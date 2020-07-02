using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;


namespace 连杆机构
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Q27_Example();
            //Cam_Example1();
            //Cam_Example2();
            // Cam_Example3();
            //Cam_Example4();
            RRP_Example1();
        }

        public static void RRR_Example()//RRR杆组示例
        {
            DrivingLink drivingLink = new DrivingLink(0.15, 0, 10, 0);//定义原动件
            RRR_Group RRR_Part1 = new RRR_Group(0.5, 0.35, true);//定义RRR杆组
            FramePoint frame1 = new FramePoint(0.3, 0);//定义机架端
            RRR_Part1.Link(drivingLink, frame1);//连接：原动件——RRR——机架
            AttachBar attachBar1 = new AttachBar(0.2, 0.2);//在距离B点0.2.角度0.2弧度的位置有一个固连点
            attachBar1.Attach(RRR_Part1, true);
            AttachBar attachBar2 = new AttachBar(0.2, 0.2);
            attachBar2.Attach(attachBar1);



            double dt = 0.01;
            int j = 1000;
            MakeForm form = new MakeForm();

            for (int i = 0; i < j; i++)
            {

                double t = i * dt;
                Console.WriteLine(attachBar1 + "固连点1");
                Bar.Outputfrom(attachBar1.GetI_Part_Output_ByTime(t));
                Console.WriteLine(attachBar2 + "固连点2");
                Bar.Outputfrom(attachBar2.GetI_Part_Output_ByTime(t));
                Console.WriteLine(RRR_Part1 + "杆件关键点");
                Bar.Outputfrom(RRR_Part1.GetII_Part_Output_ByTime(t));
                I_Part_Output o = attachBar1.GetI_Part_Output_ByTime(t);
                form[i, 0] = o.t;
                form[i, 1] = o.Pos.X;
                form[i, 2] = o.Pos.Y;
                Console.WriteLine();
                t += dt;
            }
            Console.ReadLine();
            MakeExcel(form);
        }

        public static void RRP_Example()//RRP杆组示例
        {
            DrivingLink drivingLink = new DrivingLink(1, 0, 10, 0);//定义原动件
            RRP_Group RRP_Part1 = new RRP_Group(5, 1);//定义RRP杆组
            FramePoint frame1 = new FramePoint(0, 0);//定义机架端
            GuidePath guidePath1 = new GuidePath(frame1);

            RRP_Part1.Link(drivingLink, guidePath1);//连接：原动件——RRP——导路
            AttachBar attachBar1 = new AttachBar(0.2, 0.2);//在距离B点0.2.角度0.2弧度的位置有一个固连点
            attachBar1.Attach(RRP_Part1, true);
            AttachBar attachBar2 = new AttachBar(0.2, 0.2);
            attachBar2.Attach(attachBar1);


            double dt = 0.01;
            int j = 100;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)    
            {
                double t = i * dt;
                I_Part_Output oB2 = attachBar2.GetI_Part_Output_ByTime(t);
                RRP_Output oRRP1 = RRP_Part1.GetRRP_Output_ByTime(t);

                Console.WriteLine(attachBar1 + "固连点1");
                Bar.Outputfrom(attachBar1.GetI_Part_Output_ByTime(t));
                Console.WriteLine(attachBar2 + "固连点2");
                Bar.Outputfrom(attachBar2.GetI_Part_Output_ByTime(t));
                Console.WriteLine(RRP_Part1 + "杆件关键点");
                Bar.Outputfrom(RRP_Part1.GetRRP_Output_ByTime(t));
          
                form[i, 0] = t;
                form[i, 1] = oRRP1.sD_OnPath;
                form[i, 2] = oB2.Pos.X;
                form[i, 3] = oB2.Pos.Y;

                Console.WriteLine();
             }

            MakeExcel(form);
        }

        public static void RRP_Example1()//RRP杆组示例
        {
            DrivingLink drivingLink = new DrivingLink(0.120, 0, 10, 0);//定义原动件
            RRP_Group RRP_Part1 = new RRP_Group(0.130, 0);//定义RRP杆组
            FramePoint frame1 = new FramePoint(0, 0);//定义机架端
            GuidePath guidePath1 = new GuidePath(frame1, 0);
            RRP_Part1.Link(drivingLink, guidePath1);

            double dt = 0.01;
            int j = 100;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                RRP_Output oRRP1 = RRP_Part1.GetRRP_Output_ByTime(t);

                form[i, 0] = t;
                form[i, 1] = oRRP1.sD_OnPath;
                Console.WriteLine();
            }

            MakeExcel(form);
        }
        
        public static void RPR_Example24()
        {
            DrivingLink drivingLinkAB = new DrivingLink(0.17, 0, 10, 0);
            RPR_Group RPR_PartBC = new RPR_Group(0, 0.25, 0);
            FramePoint framePointC = new FramePoint(-0.11, 0);
            RPR_PartBC.Link(drivingLinkAB, framePointC);

            AttachBar attachBarCD = new AttachBar(0.2, Math.PI / 2);
            attachBarCD.Attach(RPR_PartBC, false);

            RPR_Group RPR_PartDE = new RPR_Group(0, 0.56, 0);
            FramePoint framePointE = new FramePoint(-0.11, -0.32);
            RPR_PartDE.Link(attachBarCD, framePointE);

            AttachBar attachBarEF = new AttachBar(0.65, 0);
            attachBarEF.Attach(RPR_PartDE, false);

            FramePoint framePointX = new FramePoint(-0.5, 0.38);
            GuidePath guidePathX = new GuidePath(framePointX, 0);
            RRP_Group RRP_PartFG = new RRP_Group(0.5, 0);
            RRP_PartFG.Link(attachBarEF, guidePathX);

            double dt = 0.01;
            int j = 100;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                RRP_Output o = RRP_PartFG.GetRRP_Output_ByTime(t);            
                double sG_OnPath = o.sD_OnPath;
                double vG_OnPath = o.vD_OnPath;
                double aG_OnPath = o.aD_OnPath;
                form[i, 0] = t;
                form[i, 1] = sG_OnPath;
                form[i, 2] = vG_OnPath;
                form[i, 3] = aG_OnPath;

                Console.WriteLine();
            }

            MakeExcel(form);
        }//24题

        public static void RRR_Example26()//26
        {
            DrivingLink drivingLinkAB = new DrivingLink(0.108, 0, 10, 0);//定义原动件AB
            RRR_Group RRR_PartBCD = new RRR_Group(0.2, 0.2, false);//定义RRR BCD杆件
            AttachBar attachBarBC = new AttachBar(0.2, 0);//在C点产生一个点
            AttachBar attachBarCE = new AttachBar(0.2, Math.PI * 5 / 9);
            RRR_Group RRR_PartEFG = new RRR_Group(0.32, 0.162, false);
            FramePoint frameA = new FramePoint(0, 0);
            FramePoint frameD = new FramePoint(-0.178, 0.187);
            FramePoint frameG = new FramePoint(-0.514, 0);

            RRR_PartBCD.Link(drivingLinkAB, frameD);
            attachBarBC.Attach(RRR_PartBCD, true);
            attachBarCE.Attach(attachBarBC);
            RRR_PartEFG.Link(attachBarCE, frameG);

            double dt = 0.01;
            int j = 100;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                I_Part_Output oCE = attachBarCE.GetI_Part_Output_ByTime(t);
                II_Part_Output oGF = RRR_PartEFG.GetII_Part_Output_ByTime(t);
                double Xe = oCE.Pos.X;
                double Ye = oCE.Pos.Y;
                double phi5 = oGF.phij;
                double omega5 = oGF.omegaj;
                double alpha5 = oGF.alphaj;
                form[i, 0] = t;
                form[i, 1] =Xe;
                form[i, 2] =Ye;
                form[i, 3] =phi5;
                form[i, 4] = omega5;
                form[i, 5] = alpha5;


                Console.WriteLine();
            }

            MakeExcel(form);

        }

        public static  void Q27_Example()
        {
            DrivingLink drivingLinkAB = new DrivingLink(0.28, 0, 10, 0);//定义原动件AB
            RRR_Group RRR_PartBCD = new RRR_Group(0.35, 0.32, false);//定义RRR BCD杆件
            FramePoint frameD = new FramePoint(0, 0.16);
            RRR_PartBCD.Link(drivingLinkAB, frameD);

            AttachBar attachBarBE = new AttachBar(0.175, 0);//在BC上产生一个固连的E点
            attachBarBE.Attach(RRR_PartBCD, true);

            AttachBar attachBarEF = new AttachBar(0.22, Math.PI / 2);//C点上固连上EF
            attachBarEF.Attach(attachBarBE);
            RPR_Group RPR_PartFFG = new RPR_Group(0, 0.2, 0);
            FramePoint frameG = new FramePoint(0.025, 0.08);
            RPR_PartFFG.Link(attachBarEF, frameG);

            double dt = 0.01;
            int j = 100;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                I_Part_Output oEF = attachBarEF.GetI_Part_Output_ByTime(t);
                II_Part_Output oFFG = RPR_PartFFG.GetII_Part_Output_ByTime(t);
                double Xe = oEF.Pos.X;
                double Ye = oEF.Pos.Y;
                double phi5 = oFFG.phij;
                double omega5 = oFFG.omegaj;
                double alpha5 = oFFG.alphaj;

                form[i, 0] = t;
                form[i, 1] = Xe;
                form[i, 2] = Ye;
                form[i, 3] = phi5;
                form[i, 4] = omega5;
                form[i, 5] = alpha5;


                Console.WriteLine();
            }

            MakeExcel(form);

        }

        public static void Cam_Example1()
        {
            Cam_My1 cam_My1 = new Cam_My1(0, 0, 0, 10);
            double dt = 0.002;
            int j = 500;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                I_Part_Output o = cam_My1.GetI_Part_Output_ByTime(t);

                double PosY = o.Pos.Y;
                double vY = o.v.Y;
                double aY = o.a.Y;
                double phi = o.phi;
                

                form[i, 0] = t;
                form[i, 1] = ToExcel.RadToDegree(phi);
                form[i, 2] = PosY;
                form[i, 3] = vY;
                form[i, 4] = aY;

                
                Console.WriteLine(aY);
            }

            MakeExcel(form);
        }

        public static void Cam_Example2()
        {
            Cam_My2 cam_My2 = new Cam_My2(0, 0, 0, 10);
            double dt = 0.002;
            int j = 500;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                I_Part_Output o = cam_My2.GetI_Part_Output_ByTime(t);

                double PosY = o.Pos.Y;
                double vY = o.v.Y;
                double aY = o.a.Y;
                double phi = o.phi;
                

                form[i, 0] = t;
                form[i, 1] = ToExcel.RadToDegree(phi);
                form[i, 2] = PosY;
                form[i, 3] = vY;
                form[i, 4] = aY;

                
                Console.WriteLine(aY);
            }

            MakeExcel(form);
        }

        public static void Cam_Example3()
        {
            Cam_My3 cam_My2 = new Cam_My3(0, 0, 0, 10);
            double dt = 0.002;
            int j = 500;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                I_Part_Output o = cam_My2.GetI_Part_Output_ByTime(t);

                double PosY = o.Pos.Y;
                double vY = o.v.Y;
                double aY = o.a.Y;
                double phi = o.phi;


                form[i, 0] = t;
                form[i, 1] = ToExcel.RadToDegree(phi);
                form[i, 2] = PosY;
                form[i, 3] = vY;
                form[i, 4] = aY;


                Console.WriteLine(aY);
            }

            MakeExcel(form);
        }

        public static void Cam_Example4()
        {
            Cam_My4 cam_My4 = new Cam_My4(0, 0, 0, 10);
            double dt = 0.002;
            int j = 500;
            MakeForm form = new MakeForm();
            for (int i = 0; i < j; i++)
            {
                double t = i * dt;
                I_Part_Output o = cam_My4.GetI_Part_Output_ByTime(t);

                double PosY = o.Pos.Y;
                double vY = o.v.Y;
                double aY = o.a.Y;
                double phi = o.phi;


                form[i, 0] = t;
                form[i, 1] = ToExcel.RadToDegree(phi);
                form[i, 2] = PosY;
                form[i, 3] = vY;
                form[i, 4] = aY;


                Console.WriteLine(PosY);
            }

            MakeExcel(form);
        }

        static void MakeExcel(MakeForm form)
        {
            Console.WriteLine("按Enter继续，Esc退出");
            if(Console.ReadKey().Key==ConsoleKey.Escape)
            {
                return;
            }
            Console.WriteLine("输入文件名");
            string fName = Console.ReadLine();
            Console.WriteLine("正在生成表格，时间与导入量成正相关");
            string Current;
            Current = Directory.GetCurrentDirectory();//获取当前根目录
            ToExcel.WriteXls(Current + "\\" + fName + ".xls", form);

            Console.WriteLine("保存位置:" + Current + "\\{0}.xls", fName);
            Console.ReadLine();
        }//挂载输出Excel；
    }
}
