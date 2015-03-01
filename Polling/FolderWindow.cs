using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Polling.Network;
using ActivityMonitoring;

namespace Polling
{
    /// <summary>
    /// Class that represents a Windows Explorer abstractly. 
    /// Additionally, contains a list of all opened windows.
    /// </summary>
    public class FolderWindow
    {
        private SHDocVw.InternetExplorer folder = null;
        private Stack<String> visitedFolders = new Stack<String>();

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="folder">the opened folder</param>
        public FolderWindow(SHDocVw.InternetExplorer folder)
        {
            this.folder = folder;
        }

        /// <summary>
        /// Returns the opened folder as a property.
        /// </summary>
        /// <returns>the folder opened</returns>
        public SHDocVw.InternetExplorer Folder
        {
            get { return folder; }
            
        }

        /// <summary>
        /// Start the polling process.
        /// </summary>
        public void Start()
        {
         
            if (visitedFolders.Count != 0)
            {//if it gets here - a folder is opened

                if (!visitedFolders.Peek().Equals(folder.LocationURL))
                {
                    visitedFolders.Push(folder.LocationURL);
                    
                    String message = ResourceIdentifiers.FOLDER_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                     + folder.LocationName + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                     + folder.LocationURL + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                                     + FolderMonitor.FOLDER_VIEW + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                     + DateTime.Now;

                    EventSender.GetInstance().ProcessMessage(message);

                }
               
            }
            else
            {//can get here once - at folder start up
                visitedFolders.Push(folder.LocationURL);
               
                String message = ResourceIdentifiers.FOLDER_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                 + folder.LocationName + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                 + folder.LocationURL + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                                 + FolderMonitor.FOLDER_VIEW + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                 + DateTime.Now;


                EventSender.GetInstance().ProcessMessage(message);
                
                
            }
        }
    }
}
