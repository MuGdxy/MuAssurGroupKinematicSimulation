using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 连杆机构
{
    public class GuidePath:Bar
    //注：导路必须以原动件末端（DrivingLink）或者固连一级杆组（AttachBar）的末端为起始点K
    //赋以导路角度phij（与x轴正方向夹角弧度制）
    //一般使用时，会将phij置零
    {

        public double Phij { get; }
        Bar attachedBar;

        public GuidePath()
        {
            base.active = false;
        }

        public GuidePath(Bar attachedBar)//构造，phij默认=0
        {
            this.attachedBar = attachedBar;
            this.Phij = 0;
            base.active = true;
        }

        public GuidePath(Bar attachedBar,double phij)//重载构造，用于指定phij
        {
            this.attachedBar = attachedBar;
            this.Phij = phij;
            base.active = true;
        }


        public override I_Part_Output GetI_Part_Output_ByTime(double t)
        {
            I_Part_Output inf= attachedBar.GetI_Part_Output_ByTime(t);
            inf.phi = inf.phi + Phij;
            //在传入杆件的关键点上附加一个与x正方向的夹角，构成导路
            //由于导路直接附在被固连的杆件上，所以角量完全一致
            //又由于直接将固连杆件关键点作为导路参考点K，所以K点的运动量与被固连杆件的关键点（B点）保持一致
            return inf;
        }

        static public void DrawGuidePath(Graphics g1,Pen pen2,Vector2_t PosK,double phi,int scale,Point offset)
        {
            Point PosK1 = Vector2_t.ExpendToIntPoint(PosK, scale, offset);
            Point PosK2 = Vector2_t.ExpendToIntPoint(new Vector2_t(PosK.X + 5 * Math.Cos(phi), PosK.Y + 5 * Math.Sin(phi), 0), scale, offset);
            g1.DrawLine(pen2, PosK1, PosK2);
        }

        public void DrawGuidePath_ByTime(Graphics g1, Pen pen2, int l,int scale, Point offset,double t)
        {
            I_Part_Output o = GetI_Part_Output_ByTime(t);
            Vector2_t PosK = o.Pos;
            double phi = o.phi;
            Point PosK1 = Vector2_t.ExpendToIntPoint(PosK, scale, offset);
            Point PosK2 = Vector2_t.ExpendToIntPoint(new Vector2_t(PosK.X + l * Math.Cos(phi), PosK.Y + l * Math.Sin(phi), 0), scale, offset);
            g1.DrawLine(pen2, PosK1, PosK2);

        }
    }
}
