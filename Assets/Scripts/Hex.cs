using System;
using System.Collections;
using System.Collections.Generic;

public class Hex : IEnumerable<int>
{
    /// <summary>
    /// The number of saved components in the Hex object. Currently this is 2,
    /// because only q and r are saved.
    /// </summary>
    public const uint NumSaved = 2;

    /// <summary>
    /// The total number of components in the Hex object. This should always be
    /// 3 for Cube coordinates (q, r, s).
    /// </summary>
    public const uint NumComponents = 3;

    /// <summary>
    /// Defines Hexes for all directions.
    /// </summary>
    public static readonly Dictionary<Direction, Hex> Directions = new Dictionary<Direction, Hex>
    {
        {Direction.PosS, new Hex(0, -1, 1)},
        {Direction.NegQ, new Hex(-1, 0, 1)},
        {Direction.PosR, new Hex(-1, 1, 0)},
        {Direction.NegS, new Hex(0, 1, -1)},
        {Direction.PosQ, new Hex(1, 0, -1)},
        {Direction.NegR, new Hex(1, -1, 0)}
    };
    
    
    private int q;
    private int r;

    /// <summary>
    /// A property to get and set the q component of the Hex. Both the getter
    /// and setter change the value directly.
    /// </summary>
    public int Q
    {
        get => q;

        set => q = value;
    }

    /// <summary>
    /// A property to get and set the r component of the Hex. Both the getter
    /// and setter change the value directly.
    /// </summary>
    public int R
    {
        get => r;

        set => r = value;
    }

    /// <summary>
    /// A property to get the s component of the Hex. The s component is not
    /// saved in the Hex object, but rather calculated from q and r. Therefore
    /// it cannot be set.
    /// </summary>
    public int S => -1 * q - r;

    /// <summary>
    /// An indexer to get and set the components of the Hex. This uses the Q, R,
    /// and S properties, so only q and r can b set. Components are stored in
    /// the order [q, r, s].
    /// </summary>
    /// <param name="i">The index of the component to get. It must be in the
    /// range [0, 2].</param>
    /// <exception cref="IndexOutOfRangeException">The index was outside the
    /// range [0, 2] or attempting to set at index 2.</exception>
    public int this[uint i]
    {
        get
        {
            switch (i)
            {
                case 0:
                    return q;

                case 1:
                    return r;

                case 2:
                    return S;

                default:
                    throw new IndexOutOfRangeException("The index (" + i + ") is out of range." +
                        "it must be within the range [0, 2].");
            }
        }

        set
        {
            switch (i)
            {
                case 0:
                    q = value;
                    break;

                case 1:
                    r = value;
                    break;
                
                case 2:
                    throw new IndexOutOfRangeException("The index (" + i + ") cannot be used to set the S " +
                                                       "component. Valid indices are in the range [0, 1].");

                default:
                    throw new IndexOutOfRangeException("The index (" + i + ") is out of range. " +
                                                       "It must be within the range [0, 1].");
            }
        }
    }

    /// <summary>
    /// Constructs a Hex in which both <c>q</c> and <c>r</c> are 0.
    /// </summary>
    public Hex() : this(0, 0)
    {
    }

    /// <summary>
    /// Constructs a Hex in which both <c>q</c> and <c>r</c> are set explicitly.
    /// </summary>
    /// <param name="q">The q component of the Hex.</param>
    /// <param name="r">The r component of the Hex.</param>
    public Hex(int q, int r) {
        this.q = q;
        this.r = r;
    }

    /// <summary>
    /// Constructs a Hex in which <c>q</c>, <c>r</c>, and <c>s</c> are set
    /// explicitly. <c>s</c> must be equal to <c>-q - r</c> or, more simply,
    /// <c>q</c>, <c>r</c>, and <c>s</c> add up to 0.
    /// </summary>
    /// <param name="q">The q component of the Hex.</param>
    /// <param name="r">The r component of the Hex.</param>
    /// <param name="s">The s component of the Hex.</param>
    /// <exception cref="InvalidHexException"><c>q</c>, <c>r</c>, and <c>s</c>
    /// do not add up to 0.</exception>
    public Hex(int q, int r, int s) : this(q, r)
    {
        if (q + r + s != 0)
        {
            throw new InvalidHexException("The coordinates do not add up to 0.");
        }
    }

    
    /// <summary>
    /// Defines how the Hex is enumerated over.
    /// </summary>
    /// <returns>The current component's value.</returns>
    public IEnumerator<int> GetEnumerator()
    {
        for (uint i = 0; i < NumComponents; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    /// <summary>
    /// Returns the length of the Hex from the origin.
    /// </summary>
    public int Length => Math.Max(Math.Abs(Q), Math.Max(Math.Abs(R), Math.Abs(S)));

    /// <summary>
    /// Calculates the neighboring Hex in a given direction.
    /// </summary>
    /// <param name="d">The direction in which to calculate the neighbor.</param>
    /// <returns>The neighboring Hex as a new object.</returns>
    public Hex Neighbor(Direction d)
    {
        return this + Directions[d];
    }

    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        Hex h = obj as Hex;
        return this == h;
    }

    public override int GetHashCode()
    {
        return new Tuple<int,int>(Q,R).GetHashCode();
    }

    /// <summary>
    /// Defines how two Hexes are equivalent.
    /// If both Hexes are null, then they are equivalent.
    /// </summary>
    /// <param name="a">The Hex to compare.</param>
    /// <param name="b">The Hex to compare.</param>
    /// <returns><c>true</c> if each component is equal to the other. I.e.
    /// <c>a.Q == b.Q</c> and <c>a.R == b.R</c> or <c>false</c> otherwise.</returns>
    public static bool operator ==(Hex a, Hex b)
    {
        if (a == null && b == null)
        {
            return true;
        } 
        if (a == null || b == null)
        {
            return false;
        }
        for (uint i = 0; i < NumSaved; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Defines how two Hexes are not equivalent.
    /// </summary>
    /// <param name="a">The Hex to compare.</param>
    /// <param name="b">The Hex to compare.</param>
    /// <returns><c>false</c> if each component is equal to the other. I.e.
    /// <c>a.Q == b.Q</c> and <c>a.R == b.R</c> or <c>true</c> otherwise.</returns>
    public static bool operator !=(Hex a, Hex b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Performs component-wise addition on the Hexes. 
    /// </summary>
    /// <param name="a">The augend, or the Hex to add to.</param>
    /// <param name="b">The addend, or Hex to add.</param>
    /// <returns>A new Hex object with each component containing the sum of
    /// their respective additions.</returns>
    public static Hex operator +(Hex a, Hex b)
    {
        Hex ret = new Hex();
        
        for (uint i = 0; i < NumSaved; i++)
        {
            ret[i] = a[i] + b[i];
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise subtraction on the Hexes.
    /// </summary>
    /// <param name="a">The minuend, or the Hex to subtract from.</param>
    /// <param name="b">The subtrahend, or the Hex to subtract.</param>
    /// <returns>A new Hex object with each component containing the difference
    /// of their respective subtractions.</returns>
    public static Hex operator -(Hex a, Hex b)
    {
        Hex ret = new Hex();

        for (uint i = 0; i < NumSaved; i++)
        {
            ret[i] = a[i] - b[i];
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise scalar multiplication on the Hex.
    /// </summary>
    /// <param name="h">The Hex on which to perform the multiplication.</param>
    /// <param name="s">The scalar to multiply by.</param>
    /// <returns>A new Hex object with each component containing the product of
    /// their respective multiplications.</returns>
    public static Hex operator *(Hex h, int s)
    {
        Hex ret = new Hex();

        for (uint i = 0; i < NumSaved; i++)
        {
            ret[i] = h[i] * s;
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise scalar multiplication on the Hex.
    /// </summary>
    /// <param name="s">The scalar to multiply by.</param>
    /// <param name="h">The Hex on which to perform the multiplication.</param>
    /// <returns>A new Hex object with each component containing the product of
    /// their respective multiplications.</returns>
    public static Hex operator *(int s, Hex h)
    {
        Hex ret = new Hex();

        for (uint i = 0; i < NumSaved; i++)
        {
            ret[i] = h[i] * s;
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise scalar division on the Hex.
    /// </summary>
    /// <param name="h">The Hex from which to divide.</param>
    /// <param name="s">The scalar to divide by.</param>
    /// <returns>A new Hex object with each component containing the quotient of
    /// their respective divisions.</returns>
    /// <exception cref="DivideByZeroException"><c>s</c> is 0.</exception>
    public static Hex operator /(Hex h, int s)
    {
        if (s == 0)
        {
            throw new DivideByZeroException("The parameter s cannot be zero.");
        }
        
        Hex ret = new Hex();

        for (uint i = 0; i < NumSaved; i++)
        {
            ret[i] = h[i] / s;
        }

        return ret;
    }

    /// <summary>
    /// Calculates the distance between two Hexes.
    /// </summary>
    /// <param name="a">The first Hex.</param>
    /// <param name="b">The second Hex.</param>
    /// <returns>The distance between the two hexes.</returns>
    public static int Distance(Hex a, Hex b)
    {
        return (a - b).Length;
    }
}
