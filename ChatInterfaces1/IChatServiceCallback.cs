using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ChatObjects;

namespace ChatInterfaces1
{
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyMessage(string message);

        [OperationContract(IsOneWay = true)]
        void UpdateUsersList(List<ChatUser> listChatUsers);

        [OperationContract(IsOneWay = true)]
        void OnInkStrokesUpdate(ChatUser chatUser, byte[] bytesStroke);

        [OperationContract(IsOneWay = true)]
        void ServerDisconnected();
    }
}
