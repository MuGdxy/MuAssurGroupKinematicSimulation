using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public class RRP_Group:II_Group
    {
        public RRP_Group()
        {
            base.active = false;
        }

        public RRP_Group(double li,double lj)
        {
            base.Li = li;
            base.Lj = lj;
            base.active = true;
        }

        public void Link(Bar frontPart, GuidePath behindPart)//连接（Link）前方组件与后方组件，要求后方必须是一个导路
        {
            base.frontPart = frontPart;
            base.behindPart = behindPart;
        }

        public override II_Part_Output GetII_Part_Output_ByTime(double t)
        {
            return GetRRP_Output_ByTime(t);
        }

        public RRP_Output GetRRP_Output_ByTime(double t)
        {
            I_Part_Output frontPartInf = frontPart.GetI_Part_Output_ByTime(t);
            I_Part_Output behindPartInf = behindPart.GetI_Part_Output_ByTime(t);
            RRP_Output o = new RRP_Output();

            o.t = t;
            Vector2_t PosB = frontPartInf.Pos;
            Vector2_t vB = frontPartInf.v;
            Vector2_t aB = frontPartInf.a;
            o.PosB = PosB;
            o.vB = vB;
            o.aB = aB;

            double phii;
            double omegai;
            double alphai;

            double vD_OnPath;
            double aD_OnPath;

            Vector2_t PosK = behindPartInf.Pos;
            o.PosK = PosK;
            Vector2_t vK = behindPartInf.v;
            Vector2_t aK = behindPartInf.a;

            double phij = behindPartInf.phi;
            double omegaj = behindPartInf.omega;
            double alphaj = behindPartInf.alpha;
            o.phij = phij + Math.PI / 2;//Lj杆件永远垂直于导路
            o.omegaj = omegaj;
            o.alphaj = alphaj;

            double Ao = (PosB.X - PosK.X) * Math.Sin(phij) - (PosB.Y - PosK.Y) * Math.Cos(phij);

            phii = Math.Asin((Ao + Lj) / Li) + phij;

            Vector2_t PosC = new Vector2_t();
            PosC.T = t;
            PosC.X = PosB.X + Li * Math.Cos(phii);
            PosC.Y = PosB.Y + Li * Math.Sin(phii);
            o.PosC = PosC;

            double s = (PosC.X - PosK.X + Lj * Math.Sin(phij)) / Math.Cos(phij);
            o.sD_OnPath = s;

            Vector2_t PosD = new Vector2_t();
            PosD.T = t;
            PosD.X = PosK.X + s * Math.Cos(phij);
            PosD.Y = PosK.Y + s * Math.Sin(phij);
            o.PosD = PosD;

            double Q1 = vK.X - vB.X - omegaj * (s * Math.Sin(phij) + Lj * Math.Cos(phij));
            double Q2 = vK.Y - vB.Y + omegaj * (s * Math.Cos(phij) - Lj * Math.Sin(phij));
            double Q3 = Li * Math.Sin(phii) * Math.Sin(phij) + Li * Math.Cos(phii) * Math.Cos(phij);

            omegai = (-Q1 * Math.Sin(phij) + Q2 * Math.Cos(phij)) / Q3;
            vD_OnPath = -(Q1 * Li * Math.Cos(phii) + Q2 * Li * Math.Sin(phii)) / Q3;
            o.vD_OnPath = vD_OnPath;
            o.omegai = omegai;

            Vector2_t vC = new Vector2_t();
            vC.T = t;
            vC.X = vB.X - omegai * Li * Math.Sin(phii);
            vC.Y = vB.Y + omegai * Li * Math.Cos(phii);
            o.vC = vC;

            Vector2_t vD = new Vector2_t();
            vD.T = t;
            vD.X = vK.X + vD_OnPath * Math.Cos(phij) - s * omegaj * Math.Sin(phij);
            vD.Y = vK.Y + vD_OnPath * Math.Sin(phij) + s * omegaj * Math.Cos(phij);
            o.vD = vD;

            double Q4 = aK.X - aB.X + omegai * omegai * Math.Cos(phii) - alphaj * (s * Math.Sin(phij) + Lj * Math.Cos(phij)) - omegaj * omegaj * (s * Math.Cos(phij) - Lj * Math.Sin(phij)) - 2 * vD_OnPath * omegaj * Math.Sin(phij);
            double Q5 = aK.Y - aB.Y + omegai * omegai * Math.Sin(phii) + alphaj * (s * Math.Cos(phij) - Lj * Math.Sin(phij)) - omegaj * omegaj * (s * Math.Sin(phij) + Lj * Math.Cos(phij)) + 2 * vD_OnPath * omegaj * Math.Cos(phij);
            alphai = (-Q4 * Math.Sin(phij) + Q5 * Math.Cos(phij)) / Q3;
            aD_OnPath = (-Q4 * Li * Math.Cos(phii) - Q5 * Li * Math.Sin(phii)) / Q3;
            o.aD_OnPath = aD_OnPath;

            Vector2_t aC = new Vector2_t();
            aC.T = t;
            aC.X = aB.X - alphai * Li * Math.Sin(phii) - omegai * omegai * Math.Cos(phii);
            aC.Y = aB.Y + alphai * Li * Math.Cos(phii) - omegai * omegai * Math.Sin(phii);
            o.aC = aC;

            Vector2_t aD = new Vector2_t();
            aD.T = t;
            aD.X = aK.X + aD_OnPath * Math.Cos(phij) - s * alphaj * Math.Sin(phij) - s * omegaj * omegaj * Math.Cos(phij) - 2 * vD_OnPath * omegaj * Math.Sin(phij);
            aD.Y = aK.Y + aD_OnPath * Math.Sin(phij) + s * alphaj * Math.Cos(phij) - s * omegaj * omegaj * Math.Sin(phij) + 2 * vD_OnPath * omegaj * Math.Cos(phij);
            o.aD = aD;
            return o;
        }

        public void DrawRRP_Group(Graphics g1, Pen pen1, Point PosB, Point PosC,Point PosD,double guidePath_Phi)
        {
            g1.DrawLine(pen1, PosB, PosC);
            SizeF sf2 = new SizeF(10f, 10f);
            RectangleF re2 = new RectangleF(new Point(PosB.X - 5, PosB.Y - 5), sf2);
            RectangleF re3 = new RectangleF(new Point(PosC.X - 5, PosC.Y - 5), sf2);
            g1.DrawEllipse(pen1, re2);
            g1.DrawEllipse(pen1, re3);
            //在B点画一个转动副

            g1.DrawLine(pen1, PosC, PosD);
            PairDrawing.DrawTranslationPair(g1, pen1, PosD, guidePath_Phi);
            //在D点画一个移动副
        }

        public void DrawRRP_Group_ByTime(Graphics g1,Pen pen1,int scale,Point offset,double t)
        {
            RRP_Output oRRP1 = GetRRP_Output_ByTime(t);
            double phi=behindPart.GetI_Part_Output_ByTime(t).phi;
            Point PosB = Vector2_t.ExpendToIntPoint(oRRP1.PosB, scale, offset);
            Point PosC = Vector2_t.ExpendToIntPoint(oRRP1.PosC, scale, offset);
            Point PosD = Vector2_t.ExpendToIntPoint(oRRP1.PosD, scale, offset);
           
            DrawRRP_Group(g1, pen1, PosB, PosC, PosD, phi);
        }
    }
}
