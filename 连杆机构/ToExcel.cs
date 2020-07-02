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
    public class ToExcel
    {

        public static double RadToDegree(double a)
        {
            return a / Math.PI * 180;
        }

        public static bool WriteXls(string filename,MakeForm form)
        {
            //启动Excel应用程序
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            _Workbook book = xls.Workbooks.Add(Missing.Value);
            //创建一张表，一张表可以包含多个sheet

            //如果表已经存在，可以用下面的命令打开
            //_Workbook book = xls.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            _Worksheet sheet;//定义sheet变量
            xls.Visible = false;//设置Excel后台运行
            xls.DisplayAlerts = false;//设置不显示确认修改提示

            for (int i = 1; i < 2; i++)//循环创建并写入数据到sheet
            {
                try
                {
                    sheet = (_Worksheet)book.Worksheets.get_Item(i);//获得第i个sheet，准备写入
                }
                catch (Exception ex)//不存在就增加一个sheet
                {
                    sheet = (_Worksheet)book.Worksheets.Add(Missing.Value, book.Worksheets[book.Sheets.Count], 1, Missing.Value);
                }
                sheet.Name = "第" + i.ToString() + "页";//设置当前sheet的Name
                for (int m = 0; m <form.M_Max; m++)//循环设置每个单元格的值
                {
                    for (int n = 0; n <form.N_Max; n++)
                        sheet.Cells[m+1, n+1] = form[m ,n];
                }
            }
            //将表另存为
            book.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //如果表已经存在，直接用下面的命令保存即可
            //book.Save();

            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
            xls.Quit();//Excel程序退出
            //sheet,book,xls设置为null，防止内存泄露
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();//系统回收资源
            return true;
        }

    }
}
