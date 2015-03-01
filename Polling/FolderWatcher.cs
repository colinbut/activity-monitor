using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using Polling.Network;

namespace Polling
{
    /// <summary>
    /// Watches folder changes particularly.
    /// </summary>
    public class FolderWatcher
    {
        private static Timer timer = null;
        private static Timer timer2 = null;
        private List<String> folders = new List<String>();
        private Stack<String> visitedFolders = new Stack<String>();

        /*Keeps a list of all opened windows explorer*/
        private List<FolderWindow> dirs = new List<FolderWindow>();

        /*The shell window obj*/
        private SHDocVw.ShellWindows shellwindow = new SHDocVw.ShellWindowsClass();

        private String fileName = String.Empty;

        private static FolderWatcher instance = new FolderWatcher();

        /// <summary>
        /// Constructor
        /// </summary>
        private FolderWatcher()
        {
            
        }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>instance</returns>
        public static FolderWatcher GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Callback method used to check which folders were visited during the last
        /// poll.
        /// </summary>
        /// <param name="temp">required argument</param>
        public void CheckVisitedFolders(object temp)
        {
            try
            {
                foreach(FolderWindow f in dirs)
                {
               
                    try
                    {
                        f.Start();
                    }
                    catch 
                    {
                        //try remove it..
                        
                    }
                }

            }
            catch (Exception e) { Console.WriteLine("Exception 1: " + e.Message); } 
        }
        
        /// <summary>
        /// Private helper to check whether the current list of opened folders
        /// contains it.
        /// </summary>
        /// <param name="ie">the folder window</param>
        /// <returns>true if it does, false otherwise</returns>
        private Boolean Does_Folder_List_Have_It(SHDocVw.InternetExplorer ie)
        {
            foreach (FolderWindow f in dirs)
            {
                if (f.Folder.Equals(ie))
                {
                    return true;
                }
            }
            return false;//it doesnt have that folder in current list
        }

        /// <summary>
        /// Poll for folder changes.
        /// </summary>
        /// <param name="temp">required args</param>
        public void CheckDirectory(object temp)
        {
            
            /*Check folder opening*/
            foreach (SHDocVw.InternetExplorer ie in shellwindow)//for each opened window on desktop
            {
                fileName = Path.GetFileNameWithoutExtension(ie.FullName.ToLower());
                               

                if (fileName.Equals("explorer"))//if its a folder
                {
                    if (Does_Folder_List_Have_It(ie) == false)
                    {
                        dirs.Add(new FolderWindow(ie));
                    }
                  
                }
            }
        }


               
        /// <summary>
        /// Trigger the whole polling process.
        /// </summary>
        public void Start()
        {
            TimerCallback timerDelegate = new TimerCallback(CheckDirectory);
            timer = new System.Threading.Timer(timerDelegate, null, 0, 1000);
            timerDelegate.Invoke(new object());

            

            TimerCallback timerDelegate2 = new TimerCallback(CheckVisitedFolders);
            timer2 = new System.Threading.Timer(timerDelegate2, null, 0, 1000);
            timerDelegate2.Invoke(new object());
        }

        
    }
}
