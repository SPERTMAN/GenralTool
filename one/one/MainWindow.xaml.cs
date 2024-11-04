using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using one.Useradd;
using one.Common;
using one.Model;

namespace one
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable _DataTable = new DataTable();
       // private FlieList _FlieList = new FlieList();
       // private TextBlock[] _textBoxes = new TextBlock[6];
        private string _ConfigPath= AppDomain.CurrentDomain.BaseDirectory;
        private double heirht, wight;
        public MainWindow()
        {
            InitializeComponent();
            heirht = this.Height;
            wight=this.Width;   
            this.SizeChanged += new System.Windows.SizeChangedEventHandler(MainWindow_Resize);

         



        }

        private void MainWindow_Resize(object sender, SizeChangedEventArgs e)
        {
            double bili =  this.ActualHeight/ heirht ;
            if (this.ActualHeight>900)
            {
                filelist.Height = bili * 340;
                filelist.Width = bili * 600;
                return;
            }
            filelist.Width = 500;
            filelist.Height= bili* 270;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
           
        }

       

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Filename = @"C:\Users\Administrator\AppData\Roaming\Microsoft\Internet Explorer\Quick Launch\Shows Desktop.lnk";
                System.Diagnostics.Process.Start(Filename);
            }
            catch (Exception ex)
            {
                showErrWin(ex.ToString());


            }
           
           
        }

        public void showErrWin( string errmes)
        {
            ErrorWindow errorWindow = new ErrorWindow(errmes);
            errorWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                string Filename = @"F:\Mobile\material\Note\新电脑笔记\COMMUNICATION\新分区 1.one";
                string Filename_E = Filename.Replace("F", "E");
                if (File.Exists(Filename))
                {
                    System.Diagnostics.Process.Start(Filename);
                }
                else if (File.Exists(Filename_E))
                {
                    System.Diagnostics.Process.Start(Filename.Replace("F", "E"));
                }


            }
            catch (Exception ex)
            {
                showErrWin(ex.ToString());


            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string Filename = AppDomain.CurrentDomain.BaseDirectory+"File\\开发日志.docx";
                System.Diagnostics.Process.Start(Filename);
            }
            catch (Exception ex)
            {
                showErrWin(ex.ToString());


            }
        }

        private void ImageButtons_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bilibili.com/");
        }

        private void ImageButtons_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://js.design/home");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(false)
            {
                Process.Start(@"c:/windows/system32/shutdown.exe", "-s");
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            List<FileListModel> FileListModel =  _DataTable.ReadFile();

            filelist.ChangeTxt(FileListModel,null);



        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string Filename = @"C:\Users\Administrator\Desktop\自制\one\one\one.sln";
                System.Diagnostics.Process.Start(Filename);
            }
            catch (Exception ex)
            {
                showErrWin(ex.ToString());


            }
        }
    }
}
