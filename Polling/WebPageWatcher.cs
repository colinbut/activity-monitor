using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using Polling.Network;
using ActivityMonitoring;

namespace Polling
{
    /// <summary>
    /// Get URL from IE
    /// </summary>
    public class WebPageWatcher : IResourceMonitor
    {
        #region fields

        private static Timer timer = null;
        private List<String> urls = new List<String>();
               
        /*The shell window obj*/
        private SHDocVw.ShellWindows shellwindow = new SHDocVw.ShellWindowsClass();

        private String fileName = String.Empty;
        private const String InternetExplorerIdentifier = "iexplore";

        private static WebPageWatcher instance = new WebPageWatcher();

        #endregion 

        /// <summary>
        /// Constructor
        /// </summary>
        private WebPageWatcher()
        {
            // do nothing
        }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>instance</returns>
        public static WebPageWatcher GetInstance()
        {
            return instance;
        }

        
        /// <summary>
        /// A private helper method to be called and what it does is that it gets the URL of
        /// the Microsoft Internet Explorer address bar. 
        /// </summary>
        /// <param name="temp">an object to be passed in</param>
        private void CheckIEURL(object temp)
        {
            foreach (SHDocVw.InternetExplorer ie in shellwindow)
            {
                fileName = Path.GetFileNameWithoutExtension(ie.FullName.ToLower());
                
                /* Its Internet Explorer */
                if (fileName.Equals(InternetExplorerIdentifier))
                {

                    if (!urls.Contains(ie.LocationURL))
                    {
                        urls.Add(ie.LocationURL);
                                  

                        String message =  ResourceIdentifiers.WEBPAGE_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                          + ie.LocationURL + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                          + ie.LocationName + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                          + DateTime.Now;

                        EventSender.GetInstance().ProcessMessage(message);

                           
                    }
                                     
                }

                
            }
        }






        #region IResourceMonitor Members

        public void StartMonitoring()
        {
            TimerCallback timerDelegate = new TimerCallback(CheckIEURL);
            timer = new System.Threading.Timer(timerDelegate, null, 0, 1000);
            timerDelegate.Invoke(new object());
        }

        public void EndMonitoring()
        {
            timer = null;
            shellwindow = null;
            urls = null;
        }

        #endregion
    }
}
