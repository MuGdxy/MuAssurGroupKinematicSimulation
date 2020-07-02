using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    public class RPR_Part_Output:II_Part_Output
    {
        public Vector2_t PosE;
        public Vector2_t vE;
        public Vector2_t aE;
        public Vector2_t PosK;
        public double sC_OnPath;
        public double vC_OnPath;
        public double aC_Onpath;
    }
}
