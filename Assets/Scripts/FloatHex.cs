using System;
using System.Collections;
using System.Collections.Generic;

public class FloatHex : IEnumerable<float>
{
    /// <summary>
    /// Defines FloatHexes for all directions.
    /// </summary>
    public static readonly Dictionary<Direction, FloatHex> Directions = new Dictionary<Direction, FloatHex>
    {
        {Direction.PosQ, new FloatHex(1f, 0f, -1f)},
        {Direction.PosR, new FloatHex(-1f, 1f, 0f)},
        {Direction.PosS, new FloatHex(0f, -1f, 1f)},
        {Direction.NegQ, new FloatHex(-1f, 0f, 1f)},
        {Direction.NegR, new FloatHex(1f, -1f, 0f)},
        {Direction.NegS, new FloatHex(0f, 1f, -1f)}
    };

    public const float Delta = .0001f;
    
    
    private float q;
    private float r;

    /// <summary>
    /// A property to get and set the q component of the FloatHex. Both the getter
    /// and setter change the value directly.
    /// </summary>
    public float Q
    {
        get => q;

        set => q = value;
    }

    /// <summary>
    /// A property to get and set the r component of the FloatHex. Both the getter
    /// and setter change the value directly.
    /// </summary>
    public float R
    {
        get => r;

        set => r = value;
    }

    /// <summary>
    /// A property to get the s component of the FloatHex. The s component is not
    /// saved in the FloatHex object, but rather calculated from q and r. Therefore
    /// it cannot be set.
    /// </summary>
    public float S => (-1 * q) - r;

    /// <summary>
    /// An indexer to get and set the components of the FloatHex. This uses the Q, R,
    /// and S properties, so only q and r can b set. Components are stored in
    /// the order [q, r, s].
    /// </summary>
    /// <param name="i">The index of the component to get. It must be in the
    /// range [0, 2].</param>
    /// <exception cref="IndexOutOfRangeException">The index was outside the
    /// range [0, 2] or attempting to set at index 2.</exception>
    public float this[uint i]
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
                    throw new ImmutableHexComponentException("The index (" + i + ") cannot be used to set " +
                                                             "the S component. Valid indices are in the range [0, 1].");

                default:
                    throw new IndexOutOfRangeException("The index (" + i + ") is out of range. It must be " +
                                                       "within the range [0, 1].");
            }
        }
    }
    

    /// <summary>
    /// Constructs a FloatHex in which both <c>q</c> and <c>r</c> are 0.
    /// </summary>
    public FloatHex() : this(0f, 0f) { }

    /// <summary>
    /// Constructs a FloatHex in which both <c>q</c> and <c>r</c> are set explicitly.
    /// </summary>
    /// <param name="q">The q component of the FloatHex.</param>
    /// <param name="r">The r component of the FloatHex.</param>
    public FloatHex(float q, float r) {
        this.q = q;
        this.r = r;
    }

    /// <summary>
    /// Constructs a FloatHex in which <c>q</c>, <c>r</c>, and <c>s</c> are set
    /// explicitly. <c>s</c> must be equal to <c>-q - r</c> or, more simply,
    /// <c>q</c>, <c>r</c>, and <c>s</c> add up to 0.
    /// </summary>
    /// <param name="q">The q component of the FloatHex.</param>
    /// <param name="r">The r component of the FloatHex.</param>
    /// <param name="s">The s component of the FloatHex.</param>
    /// <exception cref="InvalidHexException"><c>q</c>, <c>r</c>, and <c>s</c>
    /// do not add up to 0.</exception>
    public FloatHex(float q, float r, float s) : this(q, r)
    {
        // Floating point addition causes fractional differences, so a straight equivalency check cannot be used.
        // When working with very big or very small floating point numbers, the Delta might be an issue. Would it be
        // better to use a larger Delta?
        if (q + r + s >= Delta)
        {
            throw new InvalidHexException("The coordinates do not add up to 0.");
        }
    }

    /// <summary>
    /// Constructs a FloatHex from a Hex. 
    /// </summary>
    /// <param name="h">The Hex from which to create the FloatHex.</param>
    public FloatHex(Hex h) : this(h.Q, h.R) { }

    
    /// <summary>
    /// Defines how the FloatHex is enumerated over.
    /// </summary>
    /// <returns>The current component's value.</returns>
    public IEnumerator<float> GetEnumerator()
    {
        for (uint i = 0; i < Hex.NumComponents; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    /// <summary>
    /// Returns a FloatHex with the absolute value of each component.
    /// </summary>
    public FloatHex Abs => new FloatHex(Math.Abs(Q), Math.Abs(R));

    /// <summary>
    /// Returns the length of the FloatHex from the origin.
    /// </summary>
    public float Length => Math.Max(Math.Abs(Q), Math.Max(Math.Abs(R), Math.Abs(S)));

    /// <summary>
    /// Rounds the FloatHex to the nearest Hex.
    /// This is the proper way to convert from FloatHex to Hex, because flooring a FloatHex will not always result in a
    /// valid Hex.
    /// 
    /// Source: https://www.redblobgames.com/grids/hexagons/#rounding
    /// </summary>
    /// <returns>The rounded FloatHex as a new Hex object.</returns>
    public Hex Round()
    {
        float roundedQ = (float)Math.Round(Q);
        float roundedR = (float)Math.Round(R);
        float roundedS = (float)Math.Round(S);

        float diffQ = Math.Abs(roundedQ - Q);
        float diffR = Math.Abs(roundedR - R);
        float diffS = Math.Abs(roundedS - S);

        if (diffQ > diffR && diffQ > diffS)
        {
            roundedQ = -roundedR - roundedS;
        } 
        else if (diffR > diffS)
        {
            roundedR = -roundedQ - roundedS;
        }
        else
        {
            roundedS = -roundedQ - roundedR;
        }
        
        return new Hex((int)roundedQ, (int)roundedR, (int)roundedS);
    } 

    /// <summary>
    /// Calculates the neighboring Hex in a given direction.
    /// The FloatHex is rounded to the nearest Hex.
    /// </summary>
    /// <param name="d">The direction in which to calculate the neighbor.</param>
    /// <returns>The neighboring Hex as a new object.</returns>
    public Hex Neighbor(Direction d)
    {
        return Round() + Hex.Directions[d];
    }
    
    public override bool Equals(object obj)
    {
        if ((obj is null) || !GetType().Equals(obj.GetType()))
        {
            return false;
        }

        FloatHex h = obj as FloatHex;
        return this == h;
    }

    /// <summary>
    /// Determines whether two components are equal within a given delta. 
    /// </summary>
    /// <param name="h">The FloatHex with which to compare equality.</param>
    /// <param name="delta">The threshold in which the two FloatHexes can be considered equal.</param>
    /// <returns></returns>
    public bool Equals(FloatHex h, float delta)
    {
        // Length returns the max component after taking the absolute value of each.
        // If the max is still less than delta then they can be considered equal.
        return (this - h).Length <= delta;
    }

    public override int GetHashCode()
    {
        return new Tuple<float,float>(Q, R).GetHashCode();
    }


    /// <summary>
    /// Determines whether two FloatHexes are equivalent. Performs a strict equality check. That is, all components in
    /// a must be equal to their respective components in b.
    /// </summary>
    /// <param name="a">The FloatHex to compare.</param>
    /// <param name="b">The FloatHex to compare.</param>
    /// <returns><c>true</c> if each component is equal to the other. That is,
    /// <c>a.Q == b.Q</c> and <c>a.R == b.R</c> or <c>false</c> otherwise.</returns>
    public static bool operator ==(FloatHex a, FloatHex b)
    {
        if (!a && !b)
        {
            return true;
        }

        if (!a || !b)
        {
            return false;
        }
        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines whether two FloatHexes are not equivalent.
    /// </summary>
    /// <param name="a">The FloatHex to compare.</param>
    /// <param name="b">The FloatHex to compare.</param>
    /// <returns><c>false</c> if each component is equal to the other. That is,
    /// <c>a.Q == b.Q</c> and <c>a.R == b.R</c> or <c>true</c> otherwise.</returns>
    public static bool operator !=(FloatHex a, FloatHex b)
    {
        return !(a == b);
    }
    
    /// <summary>
    /// Determines whether the FloatHex is null.
    /// </summary>
    /// <param name="h">The FloatHex to test for nullity.</param>
    /// <returns><c>true</c> if <c>h</c> is null or <c>false</c> otherwise.</returns>
    public static bool operator !(FloatHex h)
    {
        return h is null;
    }

    /// <summary>
    /// Performs component-wise addition on the FloatHexes. 
    /// </summary>
    /// <param name="a">The augend, or the FloatHex to add to.</param>
    /// <param name="b">The addend, or FloatHex to add.</param>
    /// <returns>A new FloatHex object with each component containing the sum of
    /// their respective additions.</returns>
    public static FloatHex operator +(FloatHex a, FloatHex b)
    {
        FloatHex ret = new FloatHex();
        
        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = a[i] + b[i];
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise addition on the Hex and FloatHex.
    /// </summary>
    /// <param name="a">The augend, or the FloatHex to add to.</param>
    /// <param name="b">The addend, or Hex to add.</param>
    /// <returns>A new FloatHex object with each component containing the sum of
    /// their respective additions.</returns>
    public static FloatHex operator +(FloatHex a, Hex b)
    {
        FloatHex ret = new FloatHex();
        
        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = a[i] + b[i];
        }

        return ret;
    }
    
    /// <summary>
    /// Performs component-wise addition on the Hex and FloatHex.
    /// </summary>
    /// <param name="a">The augend, or the Hex to add to.</param>
    /// <param name="b">The addend, or FloatHex to add.</param>
    /// <returns>A new FloatHex object with each component containing the sum of
    /// their respective additions.</returns>
    public static FloatHex operator +(Hex a, FloatHex b)
    {
        FloatHex ret = new FloatHex();
        
        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = a[i] + b[i];
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise subtraction on the FloatHexes.
    /// </summary>
    /// <param name="a">The minuend, or the FloatHex to subtract from.</param>
    /// <param name="b">The subtrahend, or the FloatHex to subtract.</param>
    /// <returns>A new FloatHex object with each component containing the difference
    /// of their respective subtractions.</returns>
    public static FloatHex operator -(FloatHex a, FloatHex b)
    {
        FloatHex ret = new FloatHex();

        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = a[i] - b[i];
        }

        return ret;
    }

    
    /// <summary>
    /// Performs component-wise subtraction on the Hex and FloatHex.
    /// </summary>
    /// <param name="a">The minuend, or the FloatHex to subtract from.</param>
    /// <param name="b">The subtrahend, or the Hex to subtract.</param>
    /// <returns>A new FloatHex object with each component containing the difference
    /// of their respective subtractions.</returns>
    public static FloatHex operator -(FloatHex a, Hex b)
    {
        FloatHex ret = new FloatHex();

        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = a[i] - b[i];
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise subtraction on the Hex and FloatHex.
    /// </summary>
    /// <param name="a">The minuend, or the Hex to subtract from.</param>
    /// <param name="b">The subtrahend, or the FloatHex to subtract.</param>
    /// <returns>A new FloatHex object with each component containing the difference
    /// of their respective subtractions.</returns>
    public static FloatHex operator -(Hex a, FloatHex b)
    {
        FloatHex ret = new FloatHex();

        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = a[i] - b[i];
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise scalar multiplication on the FloatHex.
    /// </summary>
    /// <param name="h">The FloatHex on which to perform the multiplication.</param>
    /// <param name="s">The scalar to multiply by.</param>
    /// <returns>A new FloatHex object with each component containing the product of
    /// their respective multiplications.</returns>
    public static FloatHex operator *(FloatHex h, float s)
    {
        FloatHex ret = new FloatHex();

        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = h[i] * s;
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise scalar multiplication on the FloatHex.
    /// </summary>
    /// <param name="s">The scalar to multiply by.</param>
    /// <param name="h">The FloatHex on which to perform the multiplication.</param>
    /// <returns>A new FloatHex object with each component containing the product of
    /// their respective multiplications.</returns>
    public static FloatHex operator *(float s, FloatHex h)
    {
        FloatHex ret = new FloatHex();

        for (uint i = 0; i < Hex.NumSaved; i++)
        {
            ret[i] = h[i] * s;
        }

        return ret;
    }

    /// <summary>
    /// Performs component-wise scalar division on the FloatHex.
    /// </summary>
    /// <param name="h">The FloatHex from which to divide.</param>
    /// <param name="s">The scalar to divide by.</param>
    /// <returns>A new FloatHex object with each component containing the quotient of
    /// their respective divisions.</returns>
    /// <exception cref="DivideByZeroException"><c>s</c> is 0.</exception>
    public static FloatHex operator /(FloatHex h, float s)
    {
        if (s == 0)
        {
            throw new DivideByZeroException("The parameter s cannot be zero.");
        }
        
        FloatHex ret = new FloatHex();

        for (uint i = 0; i < Hex.NumSaved; i++)
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
    public static float Distance(FloatHex a, FloatHex b)
    {
        return (a - b).Length;
    }
}
