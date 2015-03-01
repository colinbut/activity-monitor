using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitoring;
using Polling.Network;

namespace Polling
{
    /// <summary>
    /// Class used to monitor emails.
    /// </summary>
    public class EmailMonitor : IResourceMonitor
    {
        private static EmailMonitor instance = new EmailMonitor();

        private EmailMonitor() { }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>instance</returns>
        public static EmailMonitor GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Process the email event once got it.
        /// </summary>
        /// <param name="eventmessage">the message</param>
        public void ProcessEmailEvent(String eventmessage)
        {
            String[] spliitedMessage = eventmessage.Split('|');

            if(spliitedMessage.Length == 3)
            {
                String message = "EMAIL4" + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 spliitedMessage[1] + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 spliitedMessage[2];

                EventSender.GetInstance().ProcessMessage(message);
            }
            else
            {
                String message = "EMAIL4" + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 spliitedMessage[1] + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 spliitedMessage[2] + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 spliitedMessage[3] + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 spliitedMessage[4] + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 spliitedMessage[5];

                EventSender.GetInstance().ProcessMessage(message);
            }
                             
        }

        #region IResourceMonitor Members

        public void StartMonitoring()
        {
            // Do nothing
        }

        public void EndMonitoring()
        {
            // Do nothing
        }

        #endregion
    }
}
