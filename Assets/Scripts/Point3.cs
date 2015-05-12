using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Point3 
{

    public int x, y, z;

    public int this[int index]
    {
        get
        {
            switch (index)
            {
            case 0:
                return x;
            case 1:
                return y;
            case 2:
                return z;
            default:
                return 0;
            }
        }
        set
        {
            switch (index)
            {
            case 0:
                x = value;
                break;
            case 1:
                y = value;
                break;
            case 2:
                z = value;
                break;
            }
        }
    }

    public Point3 (int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        
    }

    public static Point3 zero = new Point3(0, 0, 0);
    public static Point3 one = new Point3(1, 1, 0);

    public static Point3 down = new Point3(0, -1, 0);
    public static Point3 up = new Point3(0, 1, 0);

    public static Point3 left = new Point3(-1, 0, 0);
    public static Point3 right = new Point3(1, 0, 0);

    public static Point3 back = new Point3(0, 0, -1);
    public static Point3 forward = new Point3(0, 0, 1);

    public static int SqrMagnitude (Point3 a)
    {
        return (a.x * a.x) + (a.y * a.y) + (a.z * a.z);
    }

    public int sqrMagnitude
    {
        get
        {
            return SqrMagnitude(this);
        }
    }

    public static Point3 Abs (Point3 a)
    {
        return new Point3(Mathf.Abs(a.x),Mathf.Abs(a.y),Mathf.Abs(a.z));
    }

    public Point3 abs
    {
        get
        {
            return Abs(this);
        }
    }

    public static Vector3 ToVector3 (Point3 a)
    {
        return new Vector3(a.x, a.y, a.z);
    }

    public Vector3 toVector3
    {
        get
        {
            return ToVector3(this);
        }
    }

    // Point3 + Point3

    public static Point3 operator + (Point3 a, Point3 b)
    {
        return new Point3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    // Point3 - Point3

    public static Point3 operator - (Point3 a, Point3 b)
    {
        return new Point3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    // Point3 * int

    public static Point3 operator * (Point3 a, int b)
    {
        return new Point3(a.x * b, a.y * b, a.z * b);
    }

    // int * Point3

    public static Point3 operator * (int b, Point3 a)
    {
        return new Point3(a.x * b, a.y * b, a.z * b);
    }

    // Point3 + int

    public static Point3 operator + (Point3 a, int b)
    {
        return new Point3(a.x + b, a.y + b, a.z + b);
    }

    // Point3 - int

    public static Point3 operator - (Point3 a, int b)
    {
        return new Point3(a.x - b, a.y - b, a.z - b);
    }

    public static bool operator == (Point3 a, Point3 b)
    {
        return (a.x == b.x) && (a.y == b.y) && (a.z == b.z);
    }

    public static bool operator != (Point3 a, Point3 b)
    {
        return (a.x != b.x) || (a.y != b.y) || (a.z != b.z);
    }

    public override bool Equals (System.Object obj)
    {
        return false;
    }

    public bool Equals (Point3 p)
    {
        // Return true if the fields match:
        return base.Equals((Point3)p) && z == p.z;
    }

    public override int GetHashCode ()
    {
        return base.GetHashCode() ^ z;
    }

    public override string ToString ()
    {
        return "(" + x + ", " + y + ", " + z + ")";
    }
}


public static class ExtendedMethods
{
    
}

