using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace one
{
    /// <summary>
    /// ErrorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private string _errmes;
        public ErrorWindow(string errmes)
        {
            InitializeComponent();
            _errmes = errmes;
        }

     

      

        private void AckErrorButton_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            if (_errmes==null|| _errmes=="")
            {
                ErrMessageTxt_control.Text = "暂无错误";
                return;
            }
            ErrMessageTxt_control.Text = _errmes;

        }
    }
}
