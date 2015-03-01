using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using Polling.Network;
using ActivityMonitoring;

namespace Polling
{
    /// <summary>
    /// Class for watching the opening of programs.
    /// </summary>
    public class ProgramWatcher : IResourceMonitor
    {
        private List<String> runningProgramList = new List<String>();
        private List<Process> procList;
        private Timer timer = null;

        private static ProgramWatcher instance = new ProgramWatcher();
        
        /// <summary>
        /// Constructor
        /// </summary>
        private ProgramWatcher()
        {
            // do nothing
        }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>instance</returns>
        public static ProgramWatcher GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Helper callback method for use of the timer object in order to poll.
        /// </summary>
        /// <param name="temp">needed argument</param>
        private void WatchProgram(object temp)
        {
            Console.WriteLine("Start");
            //Check new process opening
            foreach (Process P in Process.GetProcesses())
            {
         
                if (P.MainWindowTitle.Length > 0)
                {
                    try
                    {
                        if (!runningProgramList.Contains(P.ProcessName))
                        {
                           

                            try
                            {
                                runningProgramList.Add(P.ProcessName);
                                procList.Add(P);
                            }
                            catch (Exception e)
                            {
                                
                            }//swallow

                            String message = ResourceIdentifiers.PROGRAM_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + P.ProcessName + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                                             + P.MainWindowTitle + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + P.ToString() + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + P.MainModule.FileName + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + DateTime.Now;

                            EventSender.GetInstance().ProcessMessage(message);
                                                           
                           
                        }
                    }
                    catch (Exception e)
                    {
                       
                    }//swallow
                   
                }
            }
                  
           
        }



        #region IResourceMonitor Members

        public void StartMonitoring()
        {
            procList = new List<Process>();

            TimerCallback timerDelegate = new TimerCallback(WatchProgram);
            timer = new Timer(timerDelegate, null, 0, 1000);
            timerDelegate.Invoke(new object());
        }

        public void EndMonitoring()
        {
            timer = null;
            runningProgramList = null;
            procList = null;
        }

        #endregion

       
    }
}
