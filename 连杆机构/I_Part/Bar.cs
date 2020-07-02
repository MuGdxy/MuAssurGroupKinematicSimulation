using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连杆机构
{
    public class Bar
    {
        protected bool active;//未来用于抛出错误
        public Bar()//空构造
        {

        }
   
        public virtual I_Part_Output GetI_Part_Output_ByTime(double t)
        //用于传递刚性杆上的关键点的运动和杆件的转动运动
        {
            throw new Exception();
        }

        public static void Outputfrom(II_Part_Output o)
        //打印输出Output中的所有数据
        {
            Console.Write(" t={0:f3}" , o.t);
            Console.WriteLine();
            Console.Write(" PosB=");
            o.PosB.OutputV();
            Console.Write(" vB=");
            o.vB.OutputV();
            Console.Write(" aB=");
            o.aB.OutputV();
            Console.WriteLine();

            Console.Write(" PosD=");
            o.PosD.OutputV();
            Console.Write(" vD=");
            o.vD.OutputV();
            Console.Write(" aD=");
            o.aD.OutputV();
            Console.WriteLine();

            Console.Write(" PosC=");
            o.PosC.OutputV();
            Console.Write(" vC=");
            o.vC.OutputV();
            Console.Write(" aC=");
            o.aC.OutputV();
            Console.WriteLine();

            Console.Write(" phii={0:f}", o.phii);
            Console.Write(" omegai={0:f}", o.omegai);
            Console.Write(" alphai={0:f}", o.alphai);

            Console.Write(" phij={0:f}", o.phij);
            Console.Write(" omegaj={0:f}", o.omegaj);    
            Console.Write(" alphaj={0:f}", o.alphaj);
            Console.WriteLine();
        }
        
        public static void Outputfrom(I_Part_Output o)
        {
            Console.Write(" t={0:f3}", o.t);
            Console.WriteLine();
            Console.Write(" Pos=");
            o.Pos.OutputV();
            Console.Write(" v=");
            o.v.OutputV();
            Console.Write(" a=");
            o.a.OutputV();
            Console.WriteLine();
            Console.Write(" phi={0:f}", o.phi);
            Console.Write(" omega={0:f}", o.omega);
            Console.Write(" alpha={0:f}", o.alpha);
            Console.WriteLine();

        }
        //打印输出LittleOutput中的所有数据

        public static void Outputfrom(RRP_Output o)
        {
            Console.Write(" t={0:f3}", o.t);
            Console.WriteLine();
            Console.Write(" PosB=");
            o.PosB.OutputV();
            Console.Write(" vB=");
            o.vB.OutputV();
            Console.Write(" aB=");
            o.aB.OutputV();
            Console.WriteLine();

            Console.Write(" PosD=");
            o.PosD.OutputV();
            Console.Write(" vD=");
            o.vD.OutputV();
            Console.Write(" aD=");
            o.aD.OutputV();
            Console.WriteLine();

            Console.Write(" PosC=");
            o.PosC.OutputV();
            Console.Write(" vC=");
            o.vC.OutputV();
            Console.Write(" aC=");
            o.aC.OutputV();
            Console.WriteLine();

            Console.Write(" phii={0:f}", o.phii);
            Console.Write(" omegai={0:f}", o.omegai);
            Console.Write(" alphai={0:f}", o.alphai);

            Console.Write(" phij={0:f}", o.phij);
            Console.Write(" omegaj={0:f}", o.omegaj);
            Console.Write(" alphaj={0:f}", o.alphaj);
            Console.WriteLine();

            Console.Write(" vD_OnPath={0:f}", o.vD_OnPath);
            Console.Write(" aD_OnPath={0:f}", o.aD_OnPath);
            Console.WriteLine();
        }
    }
}
