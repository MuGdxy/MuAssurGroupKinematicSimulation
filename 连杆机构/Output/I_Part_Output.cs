using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    public class I_Part_Output
    {
        public I_Part_Output() { }
        public I_Part_Output(double t)
        {
            this.t = t;
        }
        public double t;
        //杆始端（备用关键点输出）
        public Vector2_t PosA;
        public Vector2_t vA;
        public Vector2_t aA;
        //杆末端（常用关键点输出）
        public Vector2_t Pos;
        public Vector2_t v;
        public Vector2_t a;
        //杆角量（固定输出）
        public double phi;
        public double omega;
        public double alpha;
    }
}
