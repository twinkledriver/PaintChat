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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaintChat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private ChatWindow m_mainWindow;
        public LoginControl(ChatWindow mainWindow)
        {
            m_mainWindow = mainWindow;
            InitializeComponent();
        }

        private void buttonPanel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
