using WinFormsApp1.Common;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileName.SetNetworkAdapter("192.168.3.2","255.255.255.1");
        }
    }
}
