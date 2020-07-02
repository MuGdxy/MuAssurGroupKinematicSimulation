using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public class PairDrawing
    {
        public static void DrawRotationPair(Graphics g1, Pen pen1, Point Pos)//在指定Point点画转动副
        {

            SizeF sf1 = new SizeF(10f, 10f);
            //用框框定一个10*10大小的正方形
            RectangleF re1 = new RectangleF(new Point(Pos.X - 5, Pos.Y - 5), sf1);
            //让圆心落到中心
            g1.DrawEllipse(pen1, re1);
        }

        public static void DrawTranslationPair(Graphics g1, Pen pen1, Point Pos, double guidePath_Phi)
        {
            double a = 10;
            double b = 5;
            double phi = -guidePath_Phi;

            double x1, y1, x2, y2, x3, y3, x4, y4;
            x1 = a * Math.Cos(phi) - b * Math.Sin(phi)+Pos.X;
            y1 = a * Math.Sin(phi) + b * Math.Cos(phi)+Pos.Y;

            x2 = -a * Math.Cos(phi) - b * Math.Sin(phi)+Pos.X;
            y2 = -a * Math.Sin(phi) + b * Math.Cos(phi)+Pos.Y;

            x3 = -a * Math.Cos(phi) + b * Math.Sin(phi) + Pos.X;
            y3 = -a * Math.Sin(phi) - b * Math.Cos(phi) + Pos.Y;

            x4 = a * Math.Cos(phi) + b * Math.Sin(phi) + Pos.X;
            y4 = a * Math.Sin(phi) - b * Math.Cos(phi) + Pos.Y;

            Point[] rectangle = new Point[5];
            rectangle[0] = new Point((int)x1, (int)y1);
            rectangle[1] = new Point((int)x2, (int)y2);
            rectangle[2] = new Point((int)x3, (int)y3);
            rectangle[3] = new Point((int)x4, (int)y4);
            rectangle[4] = new Point((int)x1, (int)y1);
            g1.DrawLines(pen1, rectangle);

        }//画移动副
    }
}
