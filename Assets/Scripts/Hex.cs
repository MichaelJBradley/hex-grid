using System;
using System.Collections;
using System.Collections.Generic;

public class Hex
{
    private int q;
    private int r;

    public int Q
    {
        get
        {
            return q;
        }

        set
        {
            q = value;
        }
    }

    public int R
    {
        get
        {
            return r;
        }

        set
        {
            r = value;
        }
    }

    public int this[int i]
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
    }

    public int S
    {
        get
        {
            return -1 * q - r;
        }
    }

    public Hex(int q, int r) {
        this.q = q;
        this.r = r;
    }

    public Hex(int q, int r, int s) : this(q, r)
    {
        if (q + r + s != 0)
        {
            throw new InvalidHexException("The coordinates do not add up to 0.");
        }
    }
}
