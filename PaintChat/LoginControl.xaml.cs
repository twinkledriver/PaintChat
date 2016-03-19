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
using System.ServiceModel;

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

        public string UserName
        {
            get;
            set;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.chatTypeServer.IsChecked = true;
        }

        private void chatTypeServer_Checked(object sender, RoutedEventArgs e)
        {
            this.serverPanel.IsEnabled = false;
        }

        private void chatTypeClient_Checked(object sender, RoutedEventArgs e)
        {
            this.serverPanel.IsEnabled = true;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            EndpointAddress serverAddress;
            if (this.chatTypeServer.IsChecked == true)
            {
                PaintChat.App.s_IsServer = true;
                serverAddress = new EndpointAddress("net.tcp://localhost:8000/ChatService/service");
            }
            else 
            {
                PaintChat.App.StopServer();
                PaintChat.App.s_IsServer = false;
                if (txtServer.Text.Length == 0)
                {
                    MessageBox.Show("Please enter server name");
                    return;
                }
                serverAddress = new EndpointAddress(string.Format("net.tcp://{0}:8000/ChatService/service", txtServer.Text));
            }
            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("Please enter username");
                return;
            }
            if (ChatServiceClient.Instance == null)
            {
                UserName = txtUserName.Text;
                if (App.s_IsServer)
                {
                    PaintChat.App.StartServer();
                 }
                try 
                {
                    ClientCallBack
                }
            }

        }






        private void buttonPanel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
