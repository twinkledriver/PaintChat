using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ChatObjects;
using System.IO;

namespace ChatInterfaces1
{
    [
        ServiceContract
    (
        Name = "ChatService",
        Namespace = "http://PaintChat/ChatService/",
        SessionMode = SessionMode.Required,
        CallbackContract = typeof(IChatServiceCallback)
    )
    ]
    public interface IChatService
    {
        [OperationContract()]
        bool Join(ChatUser chatUser);

        [OperationContract()]
        void Leave(ChatUser chatUser);

        [OperationContract]
        void SendBroadcastMessage(string strUserName, string message);

        [OperationContract()]
        bool IsUserNameTaken(string strUserName);

        [OperationContract()]
        void SendInkStrokes(MemoryStream memoryStream);

    }
}
