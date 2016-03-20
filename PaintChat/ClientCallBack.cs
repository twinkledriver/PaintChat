using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ChatInterfaces1;
using System.IO;
using System.Threading;
using System.Windows;

namespace PaintChat
{
    [
        CallbackBehavior
        (
            ConcurrencyMode = ConcurrencyMode.Single,
            UseSynchronizationContext = false
        )
    ]
    public class ClientCallBack : IChatServiceCallback
    {
        public static ClientCallBack Instance;
        private SynchronizationContext m_uiSyncContext = null;
        private ChatWindow m_mainWindow;

        public ClientCallBack(SynchronizationContext uiSyncContext, ChatWindow mainWindow)
        {
            m_uiSyncContext = uiSyncContext;
            m_mainWindow = mainWindow;
        }

        public void OnInkStrokesUpdate(ChatObjects.ChatUser chatUser, byte[] bytesStroke)
        {
            SendOrPostCallback callback =
                     delegate(object state)
                     {
                         m_mainWindow.OnInkStrokesUpdate(state as byte[]);
                     };
            m_uiSyncContext.Post(callback, bytesStroke);

            SendOrPostCallback callback2 =
                     delegate(object objchatUser)
                     {
                         m_mainWindow.LastUserDraw(objchatUser as ChatObjects.ChatUser);
                     };
            m_uiSyncContext.Post(callback2, chatUser);
        }

        public void UpdateUsersList(List<ChatObjects.ChatUser> listChatUsers)
        {
            SendOrPostCallback callback =
                     delegate(object objListChatUsers)
                     {
                         m_mainWindow.UpdateUsersList(objListChatUsers as List<ChatObjects.ChatUser>);
                     };

            m_uiSyncContext.Post(callback, listChatUsers);
        }

        public void ServerDisconnected()
        {
            SendOrPostCallback callback =
                        delegate(object dummy)
                        {
                            m_mainWindow.ServerDisconnected();
                        };

            m_uiSyncContext.Post(callback, null);
        }

        public void NotifyMessage(string message)
        {
            SendOrPostCallback callback =
                              delegate(object dummy)
                              {
                                  m_mainWindow.NotifyMessage(message);
                              };

            m_uiSyncContext.Post(callback, message);
        }

        public bool AcceptCall(string username)
        {
            //调用线程必须为 STA，因为许多 UI 组件都需要。
            return System.Windows.MessageBox.Show(String.Format("Accep call from \"{0}\" ", username), "Incomming Call", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes;
        }

    }

  
}
