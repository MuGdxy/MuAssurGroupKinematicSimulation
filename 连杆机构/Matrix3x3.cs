using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    class Matrix3x3
    {
        double[,] a = new double[3, 3];
        public Matrix3x3()
        {
            this.a[0, 0] = 0;
            this.a[0, 1] = 0;
            this.a[0, 2] = 0;
            this.a[1, 0] = 0;
            this.a[1, 1] = 0;
            this.a[1, 2] = 0;
            this.a[2, 0] = 0;
            this.a[2, 1] = 0;
            this.a[2, 2] = 1;
            //目的在于构造一个作用于二维坐标的矩阵
        }
        public Matrix3x3(double a11, double a12, double a13, double a21, double a22, double a23, double a31, double a32, double a33)
        //完全构造
        {
            this.a[0, 0] = a11;
            this.a[0, 1] = a12;
            this.a[0, 2] = a13;
            this.a[1, 0] = a21;
            this.a[1, 1] = a22;
            this.a[1, 2] = a23;
            this.a[2, 0] = a31;
            this.a[2, 1] = a32;
            this.a[2, 2] = a33;
        }
        public Matrix3x3(double[,] a)
        {
            this.a[0, 0] = a[0, 0];
            this.a[0, 1] = a[0, 1];
            this.a[0, 2] = a[0, 2];
            this.a[1, 0] = a[1, 0];
            this.a[1, 1] = a[1, 1];
            this.a[1, 2] = a[1, 2];
            this.a[2, 0] = a[2, 0];
            this.a[2, 1] = a[2, 1];
            this.a[2, 2] = a[2, 2];
        }

        public double this[int i,int j]//添加索引
        {
            get
            {
                return a[i+1,j+1];
            }
            set
            {
                a[i + 1, j + 1] = value;
            }
        }

        public static  Matrix3x3 operator *(Matrix3x3 a,Matrix3x3 b)//定义矩阵乘
        {

            return new Matrix3x3(
                a[1, 1] * b[1, 1] + a[1, 2] * b[2, 1] + a[1, 3] * b[3, 1], a[1, 1] * b[1, 2] + a[1, 2] * b[2, 2] + a[1, 3] * b[3, 2], a[1, 1] * b[1, 3] + a[1, 2] * b[2, 3] + a[1, 3] * b[3, 3],
                a[2, 1] * b[1, 1] + a[2, 2] * b[2, 1] + a[2, 3] * b[3, 1], a[2, 1] * b[1, 2] + a[2, 2] * b[2, 2] + a[2, 3] * b[3, 2], a[2, 1] * b[1, 3] + a[2, 2] * b[2, 3] + a[2, 3] * b[3, 3],
                a[3, 1] * b[1, 1] + a[3, 2] * b[2, 1] + a[3, 3] * b[3, 1], a[3, 1] * b[1, 2] + a[3, 2] * b[2, 2] + a[3, 3] * b[3, 2], a[3, 1] * b[1, 3] + a[3, 2] * b[2, 3] + a[3, 3] * b[3, 3]
                );
        }

        public static Vector2_t operator *(Matrix3x3 m,Vector2_t v)
        {
            return new Vector2_t(
                m[1, 1] * v.X + m[1, 2] * v.Y + m[1, 3] * 1,
                m[2, 1] * v.X + m[2, 2] * v.Y + m[3, 3] * 1,
                1
                );
        }

        public static  Matrix3x3 RotationM(double phi)
        {
            return new Matrix3x3(
                Math.Cos(phi),-Math.Sin(phi),0,
                Math.Sin(phi),Math.Cos(phi),0,
                0,0,1
                );
        }
        //生成用于绕平面O点旋转phi的矩阵

        public static Vector2_t RotateV2(Vector2_t v, double phi)
        //封装方法，用于对二维向量进行旋转
        {
            return RotationM(phi) * v;
        }
    }
}
