using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public enum AttachPoint { B,C,D};

    public class AttachBar:Bar
    {

        private double l;
        private double deltaPhi ;
        int type;//0为接在固连杆后，1为接在Li后，2为接在Lj后
        

        public II_Group II_AttachedPart;
        public AttachBar frontAttachBar;
        private Bar BehindPart;


        public AttachBar()//空构造 未激活
        {
            active = false;
        }

        public AttachBar(double l,double deltaPhi)
        {
            this.l = l;
            this.deltaPhi= deltaPhi;
            active = true;
        }

        public void Attach(II_Group bar,bool isLi)
        {
            this.II_AttachedPart = bar;
            type = (isLi == true) ? 1 : 2;
        }



        public void Attach(AttachBar bar)
        {
            this.frontAttachBar = bar;
            type = 0;
        }

        public II_Part_Output GetInput_ByTime(double t)
        {
            return II_AttachedPart.GetII_Part_Output_ByTime(t);
        }

        public I_Part_Output GetInputAttach_Bytime(double t)
        {
            return frontAttachBar.GetI_Part_Output_ByTime(t);
        }


        public override I_Part_Output GetI_Part_Output_ByTime(double t)
        {
            double phi;
            double omega;
            double alpha;
            Vector2_t PosA;
            Vector2_t vA;
            Vector2_t aA;
            if (type > 0)
            {
                II_Part_Output o = GetInput_ByTime(t);
                if (type == 1)
                {
                    phi = o.phii + deltaPhi;
                    omega = o.omegai;
                    alpha = o.alphai;
                    PosA = o.PosB;
                    vA = o.vB;
                    aA = o.aB;
                }
                else
                {
                    phi = o.phij + deltaPhi;
                    omega = o.omegaj;
                    alpha = o.alphaj;
                    PosA = o.PosD;
                    vA = o.vD;
                    aA = o.aD;
                }
            }
            else
            {
                I_Part_Output lo = GetInputAttach_Bytime(t);
                phi = lo.phi+deltaPhi;
                omega = lo.omega;
                alpha = lo.alpha;
                PosA = lo.Pos;
                vA = lo.v;
                aA = lo.a;
            }


            Vector2_t Pos = new Vector2_t(PosA.X + l * Math.Cos(phi), PosA.Y + l * Math.Sin(phi), t);
            Vector2_t v = new Vector2_t(vA.X - omega * l * Math.Sin(phi), vA.Y + omega * l * Math.Cos(phi), t);
            Vector2_t a = new Vector2_t(aA.X - omega * omega * l * Math.Cos(phi) - alpha * l * Math.Sin(phi), aA.Y - omega * omega * l * Math.Sin(phi) + alpha * l * Math.Cos(phi), t);

            I_Part_Output I_o = new I_Part_Output(t);

            I_o.PosA = PosA;
            I_o.Pos = Pos;
            I_o.v = v;
            I_o.a = a;
            I_o.phi = phi;
            I_o.omega = omega;
            I_o.alpha = alpha;

            return I_o;

        }

        public void DrawAttachBar_ByTime(Graphics g1,Pen pen1,int scale,Point offset,double t)
        {
            I_Part_Output o = GetI_Part_Output_ByTime(t);
            Point PosA = Vector2_t.ExpendToIntPoint(o.PosA, scale, offset);
            Point PosB= Vector2_t.ExpendToIntPoint(o.Pos, scale, offset);
            g1.DrawLine(pen1, PosA, PosB);
        }
    }
}
