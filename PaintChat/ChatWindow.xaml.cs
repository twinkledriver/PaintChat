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
using System.Windows.Ink;
using System.IO;




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

        protected void Window_Loaded(object sender, RoutedEvent e)
        {
            SignInMode();

            this.inkCanv.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
            this.inkCanv.DefaultDrawingAttributes.Width = 10;
            this.inkCanv.DefaultDrawingAttributes.Height = 10;
            this.inkCanv.DefaultDrawingAttributes.Color = FillColor;
        }

        private void ShowChatControls(Visibility visibility)
        {
            this.BorderEditingType.Visibility = visibility;
            this.BorderInkCanvas.Visibility = visibility;
            this.BorderUserList.Visibility = visibility;
            this.BorderInkMessage.Visibility = visibility;
        }

        private void SignInMode()
        {
            this.IvUsers.Items.Clear();
            this.inkCanv.Strokes.Clear();
            ShowChatControls(Visibility.Hidden);
            this.loginCanvas.Visibility = Visibility.Visible;
            this.btnLeave.Visibility = Visibility.Hidden;
        }

        public void ChatMode()
        {
            this.btnLeave.Visibility = Visibility.Visible;
            loginCanvas.Visibility = Visibility.Hidden;
            ShowChatControls(Visibility.Visible);
        }

        private void btnLeave_Click(object sender, RoutedEvent e)
        {
            ExitChat();
            this.SignInMode();
        }

        public void ServerDisconnected()
        {
            ChatServiceClient.Instance = null;
            this.SignInMode();
        }
        public void NotifyMessage(string message)
        {
            txtAllMessage.AppendText(message + "\r\n");
        }

        public void OnInkStrokesUpdate(byte[] bytesStroke)
        { 
            try
            {
                System.IO.MemoryStream memoryStream = new MemoryStream(bytesStroke);
                this.inkCanv.Strokes = new StrokeCollection(memoryStream);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }

        }

        private void SaveGesture()
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();

                this.inkCanv.Strokes.Save(memoryStream);

                memoryStream.Flush();

                ChatServiceClient.Instance.SendInkStrokes(memoryStream);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
            }
        }


        private void inkCanv_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            SaveGesture();
        }

        private void inkCanv_StrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
        {
            SaveGesture();
        }

        private void inkCanv_StrokeErased(object sender, RoutedEventArgs e)
        {
            SaveGesture();
        }


        private void ExitChat()
        {
            if (ChatServiceClient.Instance != null)
            {
                ChatServiceClient.Instance.Leave();
                ChatServiceClient.Instance.Close();
                ChatServiceClient.Instance = null;
            }
            if (App.s_IsServer)
            {
                App.StopServer();
            }

        }

        private void OnSetFill(object sender, RoutedEventArgs e)
        {
            Microsoft.Samples.CustomControls.ColorPickerDialog cPicker = new Microsoft.Samples.CustomControls.ColorPickerDialog();
            cPicker.StartingColor = FillColor;
            cPicker.Owner = this;

            bool? dialogResult = cPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                FillColor = cPicker.SelectedColor;
                this.inkCanv.DefaultDrawingAttributes.Color = FillColor;
            }

        }

        private Color FillColor
        {
            get
            {
                return (Color)GetValue(FillColorProperty);
            }
            set
            {
                SetValue(FillColorProperty, value);
            }
        }

        private void rbInkType_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            this.inkCanv.EditingMode = (InkCanvasEditingMode)rad.Tag;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            ChatServiceClient.Instance.SendBroadcastMessage(loginCtrl.UserName, txtMessage.Text);
            txtAllMessage.AppendText("我：" + txtMessage.Text + "\r\n");
            txtMessage.Text = "";
        }


       
    }
}
