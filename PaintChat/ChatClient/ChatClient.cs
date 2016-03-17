using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatInterfaces1;

namespace PaintChat
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class ChatServiceClient :System.ServiceModel.DuplexClientBase<IChatService>,IChatService
    {
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance) :
            base(callbackInstance)
        { 
        }

         public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
            base(callbackInstance, endpointConfigurationName)
        {
        }

         public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

         public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

         public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, binding, remoteAddress)
        {

        }

         public bool Join(ChatObjects.ChatUser chatUser)
         {
             return base.Channel.Join(chatUser);
         }

         public void Leave(ChatObjects.ChatUser chatUser)
         {
             base.Channel.Leave(chatUser);
         }

         public bool IsUserNameTaken(string strUserName)
         {
             return base.Channel.IsUserNameTaken(strUserName);
         }

         public void SendInkStrokes(System.IO.MemoryStream memoryStream)
         {
             base.Channel.SendInkStrokes(memoryStream);
         }

         public void SendBroadcastMessage(string strUserName, string message)
         {
             base.Channel.SendBroadcastMessage(strUserName, message);
         }

    }
    
}
