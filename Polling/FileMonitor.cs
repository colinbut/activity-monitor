using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polling
{
    /// <summary>
    /// Monitoring files.
    /// </summary>
    public class FileMonitor : IResourceMonitor
    {
        public const String CREATE_ACTION = "CREATE";
        public const String RENAME_ACTION = "RENAME";
        public const String CHANGE_ACTION = "CHANGE";
        public const String DELETE_ACTION = "DELETE";
        public const String VIEW_ACTION = "VIEW";

        private GeneralFileWatcher filewatcher = GeneralFileWatcher.GetInstance();
        private MSOFFICEFileWatcher msofficewatcher = MSOFFICEFileWatcher.GetInstance();


        private static FileMonitor instance = new FileMonitor();

        private FileMonitor() { }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>the instance of this class</returns>
        public static FileMonitor GetInstance()
        {
            return instance;
        }


        #region IResourceMonitor Members

        public void StartMonitoring()
        {
            COMReceiver.GetInstance().StartReceivingMessages();
            filewatcher.Start();
          
        }

        public void EndMonitoring()
        {
            
        }

        #endregion
    }
}
