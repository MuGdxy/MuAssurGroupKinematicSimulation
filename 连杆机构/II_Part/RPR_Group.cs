using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public class RPR_Group:II_Group
    //机械原理P339
    {
        public double Lk { get; set; }

        public RPR_Group()
        {
            base.active = false;
        }

        public RPR_Group(double li, double lj,double lk)
        {
            base.Li = li;
            base.Lj = lj;
            this.Lk = lk;
            base.active = true;
        }

        public void Link(Bar frontPart, Bar behindPart)//连接（Link）前方组件与后方组件，要求后方必须是一个导路
        {
            base.frontPart = frontPart;
            base.behindPart = behindPart;
        }

        public override II_Part_Output GetII_Part_Output_ByTime(double t)
        {
            return GetRPR_Output_ByTime(t);
        }

        public RPR_Part_Output GetRPR_Output_ByTime(double t)
        {
            I_Part_Output infB = frontPart.GetI_Part_Output_ByTime(t);
            I_Part_Output infD = behindPart.GetI_Part_Output_ByTime(t);

            RPR_Part_Output o = new RPR_Part_Output();
            o.t = t;

            Vector2_t PosB = infB.Pos;
            Vector2_t PosD = infD.Pos;

            Vector2_t vB = infB.v;
            Vector2_t vD = infD.v;
            Vector2_t aB = infB.a;
            Vector2_t aD = infD.a;
            double omegaB = infB.omega;
            o.PosB = infB.Pos;
            o.PosD = infD.Pos;
            o.vB = infB.v;
            o.vD = infD.v;
            o.aB = infB.a;
            o.aD = infD.a;

            double A0 = PosB.X - PosD.X;
            double B0 = PosB.Y - PosD.Y;
            double C0 = Li + Lk;

            double s_OnPath = Math.Sqrt(A0 * A0 + B0 * B0 - C0 * C0);
            o.sC_OnPath = s_OnPath;


            double m = B0 * s_OnPath + A0 * C0;
            double n = A0 * s_OnPath - B0 * C0;
            double tanPhij = m / n;
            double sinPhij = m  / Math.Sqrt(m * m + n * n);
            double cosPhij = n / Math.Sqrt(m * m + n * n);

            double phij;
            if(sinPhij>=0&cosPhij<=0)
            {
                phij = Math.Atan(tanPhij)+Math.PI;
            }
            else if(sinPhij<=0&cosPhij<=0)
            {
                phij = Math.Atan(tanPhij) - Math.PI;
            }
            else
            {
                phij = Math.Atan(tanPhij);
            }

            o.phij = phij;
            o.phii = phij + Math.PI / 2;

            Vector2_t PosC = new Vector2_t();
            PosC.T = t;
            PosC.X = PosB.X - Li * sinPhij;
            PosC.Y = PosB.Y + Li * cosPhij;

            o.PosC = PosC;
            Vector2_t PosE = new Vector2_t();
            PosE.T = t;
            PosE.X = PosC.X + (Lj - s_OnPath) * cosPhij;
            PosE.Y = PosC.Y + (Lj - s_OnPath) * sinPhij;
            o.PosE = PosE;

            Vector2_t PosK = new Vector2_t();
            PosK.T = t;
            PosK.X = PosE.X - Lj * cosPhij;
            PosK.Y = PosE.Y - Lj * sinPhij;

            double G4 = (PosB.X - PosD.X) * cosPhij + (PosB.Y - PosD.Y) * sinPhij;
            double omegaj = ((vB.Y - vD.Y) * cosPhij - (vB.X - vD.X) * sinPhij) / G4;
            o.omegaj = omegaj;
            o.omegai = omegaj;

            double v_Onpath = ((vB.X - vD.X) * (PosB.X - PosD.X) + (vB.Y - vD.Y) * (PosB.Y - PosD.Y)) / G4;
            o.vC_OnPath = v_Onpath;

            Vector2_t vC = new Vector2_t();
            vC.T = t;
            vC.X = vB.X - omegaj * Li * cosPhij;
            vC.Y = vB.Y - omegaj * Li * sinPhij;
            o.vC = vC;

            Vector2_t vE = new Vector2_t();
            vE.T = t;
            vE.X = vD.X - omegaj * (Lj * sinPhij - Lk * cosPhij);
            vE.Y = vD.Y + omegaj * (Lj * cosPhij + Lk * sinPhij);
            o.vE = vE;

            double G5 = aB.X - aD.X + omegaj * omegaj * (PosB.X - PosD.X) + 2 * v_Onpath * omegaj * sinPhij;
            double G6 = aB.Y - aD.Y + omegaj * omegaj * (PosB.Y - PosD.Y) - 2 * v_Onpath * omegaj * cosPhij;

            double alphaj = (G6 * cosPhij - G5 * sinPhij) / G4;
            o.alphaj = alphaj;
            o.alphai = alphaj;

            double a_OnPath = (G5 * (PosB.X - PosD.X) + G6 * (PosB.Y - PosD.Y)) / G4;
            o.aC_Onpath = a_OnPath;

            Vector2_t aC = new Vector2_t();
            aC.T = t;
            Vector2_t aE = new Vector2_t();
            aE.T = t;
            aC.X = aB.X - alphaj * Li * cosPhij + omegaj * omegaj * sinPhij;
            aC.Y = aB.Y - alphaj * Li * sinPhij - omegaj * omegaj * cosPhij;
            aE.X = aD.X - alphaj * (Lj * sinPhij - Lk * cosPhij) - omegaj * omegaj * (Lj * cosPhij + Lk * sinPhij);
            aE.Y = aD.Y + alphaj * (Lj * cosPhij + Lk * cosPhij) - omegaj * omegaj * (Lj * sinPhij - Lk * cosPhij);
            o.aC = aC;
            o.aE = aE;

            return o;
        }

        public void DrawRPR_Part_Group_ByTime(Graphics g1,Pen pen1,int scale,Point offset,double t)
        {
            RPR_Part_Output oRPR1 = GetRPR_Output_ByTime(t);
            double phij = oRPR1.phij;
            Point PosB = Vector2_t.ExpendToIntPoint(oRPR1.PosB, scale, offset);
            Point PosC = Vector2_t.ExpendToIntPoint(oRPR1.PosC, scale, offset);
            Point PosD = Vector2_t.ExpendToIntPoint(oRPR1.PosD, scale, offset);
            Point PosE = Vector2_t.ExpendToIntPoint(oRPR1.PosE, scale, offset);
   
            g1.DrawLine(pen1, PosB, PosC);//连接BC
            PairDrawing.DrawRotationPair(g1, pen1, PosB);//B点画转动副
            PairDrawing.DrawTranslationPair(g1, pen1, PosC, phij);//C点画移动副
            PairDrawing.DrawRotationPair(g1, pen1, PosD);//D点画转动副
            g1.DrawLine(pen1, PosD, PosE);//连接DE点
        }
    }
}
