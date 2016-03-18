using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.ServiceModel;
using ChatObjects;
using ChatInterfaces1;


namespace ChatServer
{
   delegate void EmtpyDelegate();
   [
       ServiceBehavior
       (
           ConcurrencyMode = ConcurrencyMode.Single,
           InstanceContextMode = InstanceContextMode.Single
       )
   ]

   public class ChatService : IChatService
   {
       public static Dictionary<IChatServiceCallback, ChatUser> s_dictCallbackToUser = new Dictionary<IChatServiceCallback, ChatUser>();

       public ChatService()
        {
        }

       public bool Join(ChatUser chatUser)
       {
           IChatServiceCallback client=OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

           if(s_dictCallbackToUser.ContainsValue(chatUser)==false)
           {
            s_dictCallbackToUser.Add(client,chatUser);
           }
           foreach (IChatServiceCallback callbackClient in s_dictCallbackToUser.Keys)
           {
             callbackClient.UpdateUsersList(s_dictCallbackToUser.Values.ToList());
           }
           return true;
       }

       public void Leave(ChatUser chatUser)
       {
        IChatServiceCallback client=OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();
        if (s_dictCallbackToUser.ContainsKey(client))
            {
                s_dictCallbackToUser.Remove(client);
            }
           foreach(IChatServiceCallback callbackClient in s_dictCallbackToUser.Keys)
           {
            if (chatUser.IsServer)
                {
                    if (callbackClient != client)
                    {
                        //server user logout, disconnect clients
                        callbackClient.ServerDisconnected();
                    }
                }
               else
                {
                    //normal user logout
                    callbackClient.UpdateUsersList(s_dictCallbackToUser.Values.ToList());
                }
           }
            if (chatUser.IsServer)
            {
                s_dictCallbackToUser.Clear();
            }
       }

       public bool IsUserNameTaken(string strNickName)
       {
            foreach(ChatUser chatUser in s_dictCallbackToUser.Values)
            {
                 if (chatUser.NickName.ToUpper().CompareTo(strNickName) == 0)
                {
                    return true;
                }
            }
           return false;
       }

        public void SendInkStrokes(MemoryStream memoryStream)
        {
            IChatServiceCallback client = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

             foreach (IChatServiceCallback callbackClient in s_dictCallbackToUser.Keys)
            {
                if (callbackClient != OperationContext.Current.GetCallbackChannel<IChatServiceCallback>())
                {
                    callbackClient.OnInkStrokesUpdate(s_dictCallbackToUser[client], memoryStream.GetBuffer());
                }
            }
        }
        public void SendBroadcastMessage(string clientName, string message)
        {
            IChatServiceCallback client =
                OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();
             if (client != null)
            {

                foreach (IChatServiceCallback callbackClient in s_dictCallbackToUser.Keys)
                {
                    if (callbackClient != OperationContext.Current.GetCallbackChannel<IChatServiceCallback>())
                    {
                        callbackClient.NotifyMessage(clientName + ": " + message);
                    }
                }
            }
        }
   }
}
