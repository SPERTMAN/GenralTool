using System;
using System.Collections.Generic;
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
using one.Model;

namespace one.Useradd
{
    /// <summary>
    /// FlieList.xaml 的交互逻辑
    /// </summary>
    public partial class FlieList : UserControl
    {
        private string FileName = @"D:\Robote\Bosch";

       
        public FlieList()
        {
            InitializeComponent();
            this.DataContext = this;

            
        }

       public void ChangeTxt(List<FileListModel> _textBoxes,string[] name)
        {
            int len = 0;
            List<string> strings = new List<string>();

            if (_textBoxes!=null)
            {
                len= _textBoxes.Count;
                foreach (var item in _textBoxes)
                {
                    strings.Add(item.company);
                }
            }
            else if (name!=null)
            {
                len = name.Length;
                for (int i = 0; i < name.Length; i++)
                {
                    strings.Add (name[i].Replace(FileName+"\\",""));
                }
               
            }

            for (int i = 0; i < len; i++)
            {
                switch (i)
                {
                    case 0:File1_txt.Text = strings[i];
                        break;
                    case 1:
                        File2_txt.Text = strings[i];
                        break;
                    case 2:
                        File3_txt.Text = strings[i];
                        break;
                    case 3:
                        File4_txt.Text = strings[i];
                        break;
                    case 4:
                        File5_txt.Text = strings[i];
                        break;
                    case 5:
                        File6_txt.Text = strings[i];
                        break;

                }
                  
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File1_txt.Text=="Bosch")
            {
                //tring FileName1 = FileName+"\\" +File1_txt.Text;
                // List<string> strings = new List<string>();
                string[] folders = Directory.GetDirectories(FileName);


                ChangeTxt(null, folders);
            }
          

        }
    }
}
