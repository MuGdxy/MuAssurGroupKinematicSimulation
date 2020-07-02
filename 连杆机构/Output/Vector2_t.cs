using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public class Vector2_t
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double T { get; set; }
        
        public double V3 { get; set; }
        //用于建立矩阵运算基础，不用于物理运算

        public static Vector2_t zero = new Vector2_t(0, 0, 0);
        public Vector2_t()
        {
            V3 = 1;
        }//空构造
        
        public Vector2_t(double x,double y,double t)//完全构造
        {
            this.X = x;
            this.Y = y;
            this.T = t;
        }

        static public double Distance(Vector2_t vA, Vector2_t vB)//用于获得距离
        {
            return Math.Sqrt((vA.X - vB.X) * (vA.X - vB.X) + (vA.Y - vB.Y) * (vA.Y - vB.Y));              
        }

        static public double Distance2(Vector2_t vA, Vector2_t vB)//获得距离的平方
        {
            return (vA.X - vB.X) * (vA.X - vB.X) + (vA.Y - vB.Y) * (vA.Y - vB.Y);
        }

        public static Vector2_t operator*(double left,Vector2_t v)
        {
            return new Vector2_t(left * v.X, left * v.Y, v.T);
        }
        public static Vector2_t operator *(Vector2_t v, double right)
        {
            return new Vector2_t(right * v.X, right * v.Y, v.T);
        }

        public void OutputV()
        {
            Console.Write(" ({0:f3},{1:f3})", X, Y);
        }

        public static Point ExpendToIntPoint(Vector2_t v,int scale,Point offset)//将向量扩大sacle倍，强制转化为整数用于屏幕输出
        {
            Vector2_t temp = scale * v;

            return new Point((int)temp.X+offset.X, -(int)temp.Y+offset.Y);
        }
        
    }
}
