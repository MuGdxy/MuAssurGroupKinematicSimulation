using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public class DrivingLink:Bar
    {
        public Vector2_t PosA = new Vector2_t(0, 0, 0);
        private double l;
        private double phi0;//初始角度
        private double omega0;//初始角速度
        private double alpha0;//初始角加速度
        bool active;

        public DrivingLink()//空构造 未激活
        {
            active = false;
        }

        public DrivingLink(double l,double phi0,double omega0,double alpha0)//完全构造 激活
        {
            this.l = l;
            this.phi0 = phi0;
            this.omega0 = omega0;
            this.alpha0 = alpha0;
            active = true;
        }

        public DrivingLink(Vector2_t PosA,double l, double phi0, double omega0, double alpha0)//重载完全构造 可定义原动件杆原点
        {
            this.PosA = PosA;
            this.l = l;
            this.phi0 = phi0;
            this.omega0 = omega0;
            this.alpha0 = alpha0;
            active = true;
        }

        public override I_Part_Output GetI_Part_Output_ByTime(double t)//获得1级杆组6个物理量信息；
        {
            I_Part_Output o=new I_Part_Output();
            o.PosA = PosA;
            o.vA = new Vector2_t(0, 0, t);
            o.aA = new Vector2_t(0, 0, t);
            //B点
            o.Pos = new Vector2_t(PosA.X + l * Math.Cos(phi0 + omega0 * t), PosA.Y + l * Math.Sin(phi0 + omega0 * t),t);
            o.v = new Vector2_t(-omega0 * l * Math.Sin(phi0 + omega0 * t), omega0 * l * Math.Cos(phi0 + omega0 * t), t);
            o.a = new Vector2_t(-omega0 * omega0 * l * Math.Cos(phi0 + omega0 * t), -omega0 * omega0 * l * Math.Sin(phi0 + omega0 * t), t);
            //杆件角量
            o.phi = phi0;
            o.omega = omega0;
            o.alpha = alpha0;
            return o;
        }//获得1级杆组6个物理量信息；

        static public void DrawDrivingLink(Graphics g1,Pen pen1,Point PosA,Point PosB)
        {
            g1.DrawLine(pen1, PosA, PosB);
            SizeF sf1 = new SizeF(10f, 10f);
            RectangleF re1 = new RectangleF(new Point(PosA.X - 5, PosA.Y - 5), sf1);
            g1.DrawEllipse(pen1, re1);
            //在A点画一个转动副
        }

        public void DrawDrivingLink_ByTime(Graphics g1, Pen pen1,int scale,Point offset,double t)
        {
            I_Part_Output oDriveLink1 = GetI_Part_Output_ByTime(t);
            Point PosA = Vector2_t.ExpendToIntPoint(oDriveLink1.PosA, scale, offset);
            Point PosB = Vector2_t.ExpendToIntPoint(oDriveLink1.Pos, scale, offset);
            DrawDrivingLink(g1, pen1, PosA, PosB);
        }
    }
}
