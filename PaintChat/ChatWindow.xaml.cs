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
using System.Windows.Shapes;

namespace PaintChat
{
    /// <summary>
    /// ChatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatWindow : Window
    {
        public static readonly DependencyProperty FillColorProperty=
            DependencyProperty.Register
            ("FillColor",typeof(Color),typeof(ChatWindow),new PropertyMetadata(Colors.Aquamarine));

        LoginControl loginCtrl;

        CallManager _callManager;

        public ChatWindow()
        {
            InitializeComponent();
             loginCtrl = new LoginControl(this);
            this.loginCanvas.Children.Add(loginCtrl);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            ExitChat();
            base.OnClosing(e);
        }










        private void ExitChat()
        { 
            if()
        }

        private void ChatWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
