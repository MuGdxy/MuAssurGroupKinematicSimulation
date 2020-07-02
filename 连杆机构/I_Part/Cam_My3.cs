using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    class Cam_My3:Bar
    {
        public double CircleX { get; set; }
        public double CircleY { get; set; }
        public double BaseCirR { get; set; }
        public double Omega { get; set; }

        public Cam_My3()
        {

        }

        public Cam_My3(double circleX,double circleY, double BasecircleR,double omega)
        {
            this.CircleX = circleX;
            this.CircleY = circleY;
            this.BaseCirR = BasecircleR;
            this.Omega = omega;
            base.active = true;
        }

        public override I_Part_Output GetI_Part_Output_ByTime(double t)
        {
            double phi = Omega * t;
            while(phi-2*Math.PI>0)
             {
                phi -= 2 * Math.PI;
             }
            double phi0 = Math.PI *150/180;
            double phiS = Math.PI *40/180;
            double phi0_ = Math.PI * 100 / 180;
            double phiS_ = Math.PI * 70 / 180;
            

            double h = 0.1;
            double Pi = Math.PI;
            double s;
            double v;
            double a;
            if(phi<phi0)//升程
            {
                s = h * (phi / phi0 - Math.Sin(2 * Pi / phi0 * phi)/(2*Pi));
                v = h * Omega / phi0 * (1 - Math.Cos(2 * Pi / phi0 * phi));
                a = 2 * Pi * h * Omega * Omega / (phi0 * phi0) * Math.Sin(2 * Pi / phi0 * phi);

            }
            else if(phi>=phi0&&phi<=phi0+phiS)//远休止
            {
                s = h;
                v = 0;
                a = 0;
            }
            else if(phi > phi0 + phiS && phi <= phi0 + phiS+phi0_/2)
            {
                s=h-2*h / (phi0_ * phi0_) * (phi - (phi0 + phiS)) * (phi - (phi0 + phiS));
                v = -4 * h * Omega / (phi0_ * phi0_) * (phi - (phi0 + phiS));
                a = -4 * h * Omega / (phi0_ * phi0_);
            }
            else if(phi > phi0 + phiS+ phi0_ / 2 && phi < phi0 + phiS + phi0_)
            {
                s = 2 * h / (phi0_ * phi0_) * ((phi0 + phiS + phi0_) - phi) * ((phi0 + phiS + phi0_) - phi);
                v= -4 * h * Omega / (phi0_ * phi0_) * ((phi0 + phiS + phi0_) - phi);
                a = 4 * h * Omega / (phi0_ * phi0_);
            }
            else
            {
                s = 0;
                v = 0;
                a = 0;
            }

            I_Part_Output o = new I_Part_Output();
            o.Pos = new Vector2_t(0, BaseCirR + s, t);
            o.v = new Vector2_t(0, v, t);
            o.a = new Vector2_t(0, a, t);

            o.phi = phi;
            o.omega = Omega;
            o.alpha = 0;

            return o;
        }
    }
}
