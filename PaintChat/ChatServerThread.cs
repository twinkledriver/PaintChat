using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PaintChat
{
    public class ChatServerThread
    {
        public static ServiceHost host = null;
        public static void Run()
        {
            try
            {
                host = new ServiceHost(typeof(ChatServer.ChatService));
                host.Open();
                while (true)
                {
                    if (PaintChat.App.s_bShutDownServerThread)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(200);
                }
                host.Close();
            }
            catch (System.Threading.ThreadAbortException abortEx)
            {
                System.Diagnostics.Trace.WriteLine("ChatServer ThreadAbortException: {0}", abortEx.Message.ToString());
                host.Close();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("ChatServer exception: {0}", ex.Message.ToString());
            }
            host=null;
        }
    }
}
