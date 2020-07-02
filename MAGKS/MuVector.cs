using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MAGKS
{
    //public struct Vector: IEnumerator,IEnumerable
    //{
    //    private double[] vector;
    //    private int iter;
    //    public int Size => vector.Length;
    //    public double x
    //    {
    //        get => vector[0];
    //        set => vector[0] = value;
    //    }
    //    public double y
    //    {
    //        get => vector[1];
    //        set => vector[1] = value;
    //    }
    //    public double z
    //    {
    //        get => vector[2];
    //        set => vector[2] = value;
    //    }
    //    public double w
    //    {
    //        get => vector[3];
    //        set => vector[3] = value;
    //    }

    //    public Vector(params double[] vector)
    //    {
    //        this.vector = vector;
    //        iter = -1;
    //    }
    //    public Vector(Vector src)
    //    {
    //        double[] vec = new double[src.vector.Length];
    //        for (int i = 0; i < src.vector.Length; ++i)
    //        {
    //            vec[i] = src.vector[i];
    //        }
    //        vector = vec;
    //        iter = -1;
    //    }
    //    public double this[int i]
    //    {
    //        get => vector[i];
    //        set => vector[i] = value;
    //    }
    //    public void Reset(params double[] vector) => this.vector = vector;
    //    static public double Distance2(Vector l, Vector r)
    //    {
    //        double res = 0;
    //        for (int i = 0; i < l.vector.Length; ++i)
    //            res += l.vector[i] * r.vector[i];
    //        return res;
    //    }
    //    static public double Distance(Vector l, Vector r) => Math.Sqrt(Distance2(l, r));

    //    public bool MoveNext() => ++iter<vector.Length;
    //    public void Reset() => iter = -1;
    //    public object Current => vector[iter];
    //    public IEnumerator GetEnumerator() => this;

    //    static public Vector operator *(double l, Vector r)
    //    {
    //        Vector res = new Vector(r.vector);
    //        for (int i = 0; i < res.vector.Length; ++i)
    //            res.vector[i] *= l;
    //        return res;
    //    }
    //    static public Vector operator *(Vector l, double r)
    //    {
    //        Vector res = new Vector(l);
    //        for (int i = 0; i < res.vector.Length; ++i)
    //            res.vector[i] *= r;
    //        return res;
    //    }
    //    static public Vector operator +(Vector l, Vector r)
    //    {
    //        Vector res = new Vector(l);
    //        for (int i = 0; i < r.vector.Length; ++i)
    //            res.vector[i] += r.vector[i];
    //        return res;
    //    }
    //    static public Vector operator -(Vector l, Vector r)
    //    {
    //        Vector res = new Vector(l);
    //        for (int i = 0; i < r.vector.Length; ++i)
    //            res.vector[i] -= r.vector[i];
    //        return res;
    //    }

    //    static public implicit operator double[] (Vector vec)
    //    {
    //        return vec.vector;
    //    }
    //    static public implicit operator Vector (double[] vector)
    //    {
    //        return new Vector(vector);
    //    }
    //    static public Vector vector2Zero = new Vector(0, 0);
    //}

    public struct Vector2
    {
        public Vector2(double x,double y)
        {
            this.x = x;
            this.y = y;
        }
        public double x;
        public double y;

        static public double Distance2(Vector2 l, Vector2 r)
        {
            double res = 0;
            res += l.x * r.x;
            res += l.y * r.y;
            return res;
        }
        static public double Distance(Vector2 l, Vector2 r) => Math.Sqrt(Distance2(l, r));
        static public Vector2 operator *(double l, Vector2 r)
        {
            Vector2 res = r;
            res.x *= l;
            res.y *= l;
            return res;
        }
        static public Vector2 operator *(Vector2 l, double r)
        {
            Vector2 res = l;
            res.x *= r;
            res.y *= r;
            return res;
        }
        static public Vector2 operator +(Vector2 l, Vector2 r)
        {
            Vector2 res = l;
            res.x += r.x;
            res.y += r.y;
            return res;
        }
        static public Vector2 operator -(Vector2 l, Vector2 r)
        {
            Vector2 res = l;
            res.x += r.x;
            res.y += r.y;
            return res;
        }
        static public Vector2 zero = new Vector2(0, 0);
        static public Vector2 one = new Vector2(1, 1);
    }
}
