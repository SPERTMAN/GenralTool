using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        bool IsRunningAsAdmin()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        void RestartAsAdmin()
        {
            var startInfo = new ProcessStartInfo();
            //  startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas"; // 以管理员身份运行
            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // 用户取消了管理员权限提示，或者其他错误
                // 可以在此处处理错误情况
            }

            //Application.Exit();
        }
    }

}
