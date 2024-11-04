using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Xls;
using one.Model;

namespace one.Common
{
    public class DataTable
    {
        private string _FileName = AppDomain.CurrentDomain.BaseDirectory+"\\DataTable";

        private string[] HeadName = new string[6] {"A","B","C","D","E","F" };

        public  List<FileListModel> ReadFile()
        {

            List<FileListModel> fileListModels = new List<FileListModel>();
            //创建Workbook实例
            Workbook workbook = new Workbook();
            //加载WPS表格文档
            workbook.LoadFromFile(_FileName+"\\FileList.XLSX");
            //workbook.LoadFromFile("学生个人信息登记表.ett");

            //获取第一个工作表
            Worksheet sheet = workbook.Worksheets[0];
            for (int i = 0; i < HeadName.Length; i++)
            {
                if (sheet.Range[HeadName[i] +"1"].Text != null)
                {
                    fileListModels.Add(new FileListModel() {  company=sheet.Range[HeadName[i] + "1"].Text });
                   
                }
            }
            for (int J = 0; J < fileListModels.Count; J++)
            {
                for (int i = 1; i < 50; i++)
                {
                
                    if (fileListModels[J].company != null)
                    {
                        if (sheet.Range[HeadName[J] + $"{i}"].Text != null)
                        {

                        }
                           // fileListModels[J].Project[i] = sheet.Range[HeadName[J] + $"{i}"].Text;
                    }
                }
               
            }
            return fileListModels;
            ////向工作表的指定单元格填充数据
            //sheet.Range["C11"].Text = "广东省珠海市XX街道XX号";
            //sheet.Range["C12"].Text = "广东省珠海市XX街道XX号";
            //sheet.Range["C13"].Text = "1598523XXXX";
            //sheet.Range["C14"].Text = "daokeer@ 163.com";

            //保存文档为.et格式
            workbook.SaveToFile("WPS.et", FileFormat.ET);
            //保存文档为.ett格式
            //wb.SaveToFile("WPS.ett", FileFormat.ETT);
        }
    }
}
