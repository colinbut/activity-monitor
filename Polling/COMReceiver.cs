using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using Polling.Network;
using MSMQ;

namespace Polling
{
    /// <summary>
    /// The reciever used to get messages from the MSMQ which is put there
    /// by the COM add-ins.
    /// </summary>
    public class COMReceiver
    {
        private MessageQueue queue = MSMQ_Queue.GetInstance().GetQueue();
        private const String queuename = MSMQ_Queue_Details.QUEUE_NAME;
        private static COMReceiver instance = new COMReceiver();

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>the instance</returns>
        public static COMReceiver GetInstance()
        {
            return instance;
        }

        private COMReceiver() 
        {
            Setup();
        }

        /// <summary>
        /// Setups the class.
        /// </summary>
        private void Setup()
        {
            queue.ReceiveCompleted += new ReceiveCompletedEventHandler(queue_ReceiveCompleted);
        }

      
        /// <summary>
        /// Event handler used to constantly retrieve messages from the MSMQ.
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">the event arguments</param>
        private void queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            //recieve the messages..
            Message m = queue.EndReceive(e.AsyncResult);
            m.Formatter = new XmlMessageFormatter(new string[] { "System.String,mscorlib" });

            String receivedMessage = (string)m.Body;

            String[] splittedMessage = receivedMessage.Split('|');
            if (splittedMessage[0].Equals("OUTLOOK"))
            {
                EmailMonitor.GetInstance().ProcessEmailEvent(receivedMessage);
            }
            else
            {
                MSOFFICEFileWatcher.GetInstance().FileViewed(receivedMessage);
            }

            //once get them - send to them to consuming client
            EventSender.GetInstance().ProcessMessage((string)m.Body);

            //recieve again
            StartReceivingMessages();

        }

        /// <summary>
        /// Removes all messages from the MSMQ. 
        /// </summary>
        public void DeleteAllMessages()
        {
            queue.Purge();
        }

        /// <summary>
        /// Start getting the messages from the MSMQ.
        /// </summary>
        public void StartReceivingMessages()
        {
            queue.BeginReceive();
        }
    }
}
