using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ActivityMonitoring;
using Polling.Network;

namespace Polling
{
    /// <summary>
    /// Folder monitor.
    /// </summary>
    public class FolderMonitor : IResourceMonitor
    {
        public const String FOLDER_CREATE = "CREATE";
        public const String FOLDER_RENAME = "RENAME";
        public const String FOLDER_DELETION = "DELETION";
        public const String FOLDER_VIEW = "VIEW";

        private static FolderMonitor instance = new FolderMonitor();
        private static FileSystemWatcher watcher = new FileSystemWatcher();

        private FolderMonitor() { }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>the instance</returns>
        public static FolderMonitor GetInstance()
        {
            return instance;
        }

        public void StartMonitoring()
        {
            Watch();
            FolderWatcher.GetInstance().Start();
        }

        /// <summary>
        /// Helper method to run the FSW (FileSystemWatcher) functionality.
        /// </summary>
        private void Watch()
        {
            watcher.Path = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()); //start from the root (Top - Down)

            watcher.IncludeSubdirectories = true;   //recurse watch
            watcher.EnableRaisingEvents = true;     //allow events to be notified..

            watcher.NotifyFilter = NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";                 //watch every type of file..
            

            watcher.Created += new FileSystemEventHandler(watcher_Created);
            //watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
        }

        /// <summary>
        /// Event handler to handle folder renamed.
        /// </summary>
        /// <param name="sender">triggered source</param>
        /// <param name="e">event arguments</param>
        private void watcher_Renamed(object sender, RenamedEventArgs e)
        {

            String message = ResourceIdentifiers.FOLDER_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 e.Name + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 e.FullPath + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 FOLDER_RENAME + Constants.SPACE + Constants.SPLITTER + Constants.SPACE + //still need old name/path
                                 DateTime.Now;
            EventSender.GetInstance().ProcessMessage(message);
        }

        /// <summary>
        /// Event handler to handle folder deletion.
        /// </summary>
        /// <param name="sender">triggered source</param>
        /// <param name="e">event arguments</param>
        private void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            String message = ResourceIdentifiers.FOLDER_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 e.Name + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 e.FullPath + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 FOLDER_DELETION + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 DateTime.Now;
            EventSender.GetInstance().ProcessMessage(message);
        }

        /// <summary>
        /// Event handler to handle folder created.
        /// </summary>
        /// <param name="sender">triggered source</param>
        /// <param name="e">event arguments</param>
        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (Directory.Exists(e.FullPath))
            {
                String message = ResourceIdentifiers.FOLDER_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 e.Name + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 e.FullPath + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 FOLDER_CREATE + Constants.SPACE + Constants.SPLITTER + Constants.SPACE +
                                 DateTime.Now;
                EventSender.GetInstance().ProcessMessage(message);
            }
            
        }

        #region IResourceMonitor Members


        public void EndMonitoring()
        {
          
        }

        #endregion
    }
}
