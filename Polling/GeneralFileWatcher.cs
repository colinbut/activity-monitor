using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.Contracts;
using Microsoft.Win32;
using System.Threading;
using Polling.Network;
using ActivityMonitoring;

namespace Polling
{
    /// <summary>
    /// Generally used to watch files for changes.
    /// </summary>
    public class GeneralFileWatcher 
    {
       
        private static GeneralFileWatcher _instance = new GeneralFileWatcher();
        private static FileSystemWatcher watcher = new FileSystemWatcher();

        private GeneralFileWatcher() { }

        /// <summary>
        /// Singleton implementation
        /// </summary>
        /// <returns>this class's instance</returns>
        public static GeneralFileWatcher GetInstance()
        {
            return _instance;
        }

        public void Start()
        {
            Watch();
        }

      

        
        /// <summary>
        /// Helper to utilise the FSW (FileSystemWatcher).
        /// </summary>
        private void Watch()
        {
            watcher.Path = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()); //start from the root (Top - Down)
           
            watcher.IncludeSubdirectories = true;   //recurse watch
            watcher.EnableRaisingEvents = true;     //allow events to be notified..

            watcher.Filter = "*.*";                 //watch every type of file..

            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);

        }

        /// <summary>
        /// Event handler to handle file changed.
        /// </summary>
        /// <param name="sender">triggered source</param>
        /// <param name="e">event arguments</param>
        private static void watcher_Changed(object sender, FileSystemEventArgs e)
        {

            String message = ResourceIdentifiers.FILE_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.Name + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.FullPath + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + FileMonitor.CHANGE_ACTION + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + DateTime.Now;
            
            EventSender.GetInstance().ProcessMessage(message);
               
        }

        /// <summary>
        /// Event handler to handle file deleted.
        /// </summary>
        /// <param name="sender">triggered source</param>
        /// <param name="e">event arguments</param>
        private static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            
            String message = ResourceIdentifiers.FILE_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.Name + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.FullPath + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                             + FileMonitor.DELETE_ACTION + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + DateTime.Now;

            EventSender.GetInstance().ProcessMessage(message);
              
        }

        /// <summary>
        /// Event handler to handle file renamed.
        /// </summary>
        /// <param name="sender">triggered source</param>
        /// <param name="e">event arguments</param>
        private static void watcher_Renamed(object sender, RenamedEventArgs e)
        {
                        

            String message = ResourceIdentifiers.FILE_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.Name + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.FullPath + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                             + FileMonitor.RENAME_ACTION + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                             + DateTime.Now  // also record old path/name
                             + Constants.SPACE + Constants.SPLITTER + Constants.SPACE + e.OldFullPath 
                             + Constants.SPACE + Constants.SPLITTER + Constants.SPACE + e.OldName;
            
            
            
            EventSender.GetInstance().ProcessMessage(message);
              
        }

        /// <summary>
        /// Event handler to handle file created.
        /// </summary>
        /// <param name="sender">triggered source</param>
        /// <param name="e">event arguments</param>
        private static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            String message = ResourceIdentifiers.FILE_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.Name + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + e.FullPath + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                             + FileMonitor.CREATE_ACTION + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                             + DateTime.Now;

            EventSender.GetInstance().ProcessMessage(message);
               
        }
    }
}
