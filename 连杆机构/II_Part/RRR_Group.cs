using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public class RRR_Group : II_Group
    {

        public RRR_Group()//空构造 未激活
        {
            active = false;
        }

        public RRR_Group(double li, double lj, bool clockwiseBCD)//完全构造 激活
        {
            this.Li = li;
            this.Lj = lj;
            this.ClockwiseBCD = clockwiseBCD;
            active = true;

        }

        public void Link(Bar frontPart, Bar behindPart)//连接（Link）前方组件与后方组件
        {
            base.frontPart = frontPart;
            base.behindPart = behindPart;
        }


        public override II_Part_Output GetII_Part_Output_ByTime(double t)
        {
            I_Part_Output frontPartInf = frontPart.GetI_Part_Output_ByTime(t);
            I_Part_Output behindPartInf = behindPart.GetI_Part_Output_ByTime(t);

            Vector2_t PosB = frontPartInf.Pos;
            Vector2_t PosD = behindPartInf.Pos;

            double phii;
            double phij;

            double Ao = 2 * Li * (PosD.X - PosB.X);
            double Bo = 2 * Li * (PosD.Y - PosB.Y);
            double Co = Li * Li + Vector2_t.Distance2(PosB, PosD) - Lj * Lj;
            if (ClockwiseBCD)
            {
                phii = 2 * Math.Atan((Bo + Math.Sqrt(Ao * Ao + Bo * Bo - Co * Co)) / (Ao + Co));
            }
            else
            {
                phii = 2 * Math.Atan((Bo - Math.Sqrt(Ao * Ao + Bo * Bo - Co * Co)) / (Ao + Co));
            }

            double xC = PosB.X + Li * Math.Cos(phii);
            double yC = PosB.Y + Li * Math.Sin(phii);

            Vector2_t PosC = new Vector2_t(xC, yC, t);

            double dis = Vector2_t.Distance(PosC, PosD);
            double sinPhij = (PosC.Y - PosD.Y) / dis;
            double cosPhij = (PosC.X - PosD.X) / dis;
            double tanPhij = sinPhij / cosPhij;
            if (sinPhij >= 0 & cosPhij <= 0)
            {
                phij = Math.Atan(tanPhij) + Math.PI;
            }
            else if (sinPhij <= 0 & cosPhij <= 0)
            {
                phij = Math.Atan(tanPhij) - Math.PI;
            }
            else
            {
                phij = Math.Atan(tanPhij);
            }

            Vector2_t vB = frontPartInf.v;
            Vector2_t vD = behindPartInf.v;

            double Ci = Li * Math.Cos(phii);
            double Cj = Lj * Math.Cos(phij);
            double Si = Li * Math.Sin(phii);
            double Sj = Lj * Math.Sin(phij);
            double G1 = Ci * Sj - Cj * Si;

            double omegai = (Ci * (vD.X - vB.X) + Si * (vD.Y - vB.Y)) / G1;
            double omegaj = (Ci * (vD.X - vB.X) + Si * (vD.Y - vB.Y)) / G1;

            double Vcx = vB.X - omegai * Li * Math.Sin(phii);
            double Vcy = vB.Y + omegai * Li * Math.Cos(phii);

            Vector2_t aB = frontPartInf.a;
            Vector2_t aD = behindPartInf.a;

            double G2 = aD.X - aB.X + omegai * omegai * Ci - omegaj * omegaj * Cj;
            double G3 = aD.Y - aB.Y + omegai * omegai * Si - omegaj * omegaj * Sj;

            double alphai = (G2 * Cj + G3 * Sj) / G1;
            double alphaj = (G2 * Ci + G3 * Si) / G1;

            double aCx = aB.X - alphai * Li * Math.Sin(phii) - phii * phii * Li * Math.Cos(phii);
            double aCy = aB.Y + alphai * Li * Math.Cos(phii) - phii * phii * Li * Math.Sin(phii);

            II_Part_Output RRR_Output = new II_Part_Output(t);
            RRR_Output.PosB = PosB;
            RRR_Output.vB = vB;
            RRR_Output.aB = aB;

            RRR_Output.PosD = PosD; 
            RRR_Output.vD = vD;
            RRR_Output.aD = aD;

            RRR_Output.PosC = PosC;
            RRR_Output.vC = new Vector2_t(Vcx,Vcy,t);
            RRR_Output.aC = new Vector2_t(aCx, aCy, t);


            RRR_Output.phii = phii;
            RRR_Output.phij = phij;
            RRR_Output.omegai = omegai;
            RRR_Output.omegaj = omegaj;
            RRR_Output.alphai = alphai;
            RRR_Output.alphaj = alphaj;

            return RRR_Output;
        }//给出一个类包含所有的以上的数据；

        public void DrawRRR_Part_Group_ByTime(Graphics g1,Pen pen1,int scale,Point offset,double t)
        {
            II_Part_Output oRRR1 = GetII_Part_Output_ByTime(t);
            Point PosB = Vector2_t.ExpendToIntPoint(oRRR1.PosB, scale, offset);
            Point PosC = Vector2_t.ExpendToIntPoint(oRRR1.PosC, scale, offset);
            Point PosD = Vector2_t.ExpendToIntPoint(oRRR1.PosD, scale, offset);

            g1.DrawLine(pen1, PosB, PosC);//连接BC
            g1.DrawLine(pen1, PosC, PosD);//连接CD
            PairDrawing.DrawRotationPair(g1, pen1, PosB);//B点画转动副
            PairDrawing.DrawRotationPair(g1, pen1, PosC);//C点画转动副
            PairDrawing.DrawRotationPair(g1, pen1, PosD);//D点画转动副

        }
    }
}
