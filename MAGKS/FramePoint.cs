using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAGKS
{
    public class FramePoint : Bar
    {
        private Vector2 pos;
        public FramePoint(double x,double y) => pos = new Vector2(x, y);
        public FramePoint(Vector2 pos) => this.pos = pos;

        public override I_Output GetOutput(double t)
        {
            I_Output o = new I_Output();
            o.posA = pos;
            o.posB = pos;
            o.vA = Vector2.zero;
            return o;
        }
    }
}
