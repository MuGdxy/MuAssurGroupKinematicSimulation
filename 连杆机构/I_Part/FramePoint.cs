using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    public class FramePoint:Bar
        //机架只提供一个固定点（xD，yD）
    {
        double xD;
        double yD;
        public FramePoint()//空构造
        {
            base.active = false;
        }

        public FramePoint(double xD,double yD)//完整构造
        {
            this.xD = xD;
            this.yD = yD;

            base.active = true;
        }



        public override I_Part_Output GetI_Part_Output_ByTime(double t)
        {
            I_Part_Output o = new I_Part_Output();
            o.Pos = new Vector2_t(xD, yD, t);
            o.v = new Vector2_t(0, 0, t);
            o.a = new Vector2_t(0, 0, t);
            o.phi = 0;
            o.omega = 0;
            o.alpha = 0;
            return o;
        }
    }
}
