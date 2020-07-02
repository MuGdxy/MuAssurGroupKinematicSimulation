using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    public class II_Part_Output
    {
        public II_Part_Output() { }
        public II_Part_Output(double t)
        {
            this.t = t;
        }
        public double t;

        public Vector2_t PosB;
        public Vector2_t vB;
        public Vector2_t aB;

        public Vector2_t PosD;
        public Vector2_t vD;
        public Vector2_t aD;

        public Vector2_t PosC;
        public Vector2_t vC;
        public Vector2_t aC;

        public double phii;
        public double omegai;
        public double alphai;

        public double phij;
        public double omegaj;
        public double alphaj;
    }
}
