using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    public class II_Group:Bar
    {
        protected Bar frontPart;
        protected Bar behindPart;

        public bool ClockwiseBCD { get; set; }
        //BCD顺时针方向，则为true，逆时针方向，则为false；

        public double Li { get; set; }
        public double Lj { get; set; }

        public virtual II_Part_Output GetII_Part_Output_ByTime(double t)
        //用于所有二级杆组输出的虚方法（需要到具体杆组进行重写）
        {
            return new II_Part_Output(t);
        }

    }
}
