using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    class Cam_My4 : Bar
    {
        public double CircleX { get; set; }
        public double CircleY { get; set; }
        public double BaseCirR { get; set; }
        public double Omega { get; set; }

        public Cam_My4()
        {

        }

        public Cam_My4(double circleX, double circleY, double BasecircleR, double omega)
        {
            this.CircleX = circleX;
            this.CircleY = circleY;
            this.BaseCirR = BasecircleR;
            this.Omega = omega;
            base.active = true;
        }

        public override I_Part_Output GetI_Part_Output_ByTime(double t)
        {
            double Pi = Math.PI;
            double phi = Omega * t;
            while (phi - 2 * Pi > 0)
            {
                phi -= 2 * Pi;//处理周期问题防止溢出
            }

            //定义各段运动区间
            double phi1 = Math.PI * 45 / 180;
            double phi2 = Math.PI * 45 / 180;
            double phi3 = Math.PI * 45 / 180;
            double phi4 = Math.PI * 45 / 180;
            double phi5 = Math.PI * 45 / 180;
            double phi6 = Math.PI * 45 / 180;
            double phi7 = Math.PI * 45 / 180;
            double phi8 = Math.PI * 45 / 180;

            //最远推程
            double h1 = 0.205;
            //中间程
            double h2 = 0.200;

            double s;
            double v;
            double a;


            //以下均为摆线运动规律
            if (phi < phi1)
            {

                double T = phi;
                s = h2 + (h1 - h2) * (1 - T / phi1 + Math.Sin(2 * Pi / phi1 * T) / (2 * Pi));
                v = -(h1 - h2) * Omega / phi1 * (1 - Math.Cos(2 * Pi / phi1 * T));
                a = -2 * Pi * (h1 - h2) * Omega * Omega / (phi1 * phi1) * Math.Sin(2 * Pi / phi1 * T);
            }
            else if (phi >= phi1 && phi <= phi1 + phi2)//远休止
            {
                s = h2;
                v = 0;
                a = 0;
            }
            else if (phi > phi1 + phi2 && phi <= phi1 + phi2 + phi3)
            {
                double T = phi - (phi1 + phi2);
                s = 0 + h2 * (1 - T / phi3 + Math.Sin(2 * Pi / phi3 * T) / (2 * Pi));
                v = -(h2) * Omega / phi3 * (1 - Math.Cos(2 * Pi / phi3 * T));
                a = -2 * Pi * (h2) * Omega * Omega / (phi3 * phi3) * Math.Sin(2 * Pi / phi3 * T);
            }
            else if (phi > phi1 + phi2 + phi3 && phi <= phi1 + phi2 + phi3 + phi4)
            {
                s = 0;
                v = 0;
                a = 0;
            }
            else if (phi > phi1 + phi2 + phi3 + phi4 && phi <= phi1 + phi2 + phi3 + phi4 + phi5)
            {
                double T = phi - (phi1 + phi2 + phi3 + phi4);
                s = h1 * (T / phi5 - Math.Sin(2 * Pi / phi5 * T) / (2 * Pi));
                v = h1 * Omega / phi5 * (1 - Math.Cos(2 * Pi / phi5 * T));
                a = 2 * Pi * h1 * Omega * Omega / (phi5 * phi5) * Math.Sin(2 * Pi / phi5 * T);
            }
            else
            {
                s = h1;
                v = 0;
                a = 0;
            }

            //等效为原动件输出运动
            I_Part_Output o = new I_Part_Output();

            //对输出类进行赋值
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
