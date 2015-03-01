using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 
 * Namespace Polling.Network containing all Client - Server
 * architecture implementation using TCP sockets. Normal for all
 * network connections.
 * 
 */ 
namespace Polling.Network
{
    /// <summary>
    /// This is a class which is mostly used as a information class which it
    /// stores information about the server protoocol.
    /// </summary>
    public class ServerInformation
    {
        /// <summary>
        /// Private constructor to stop outside instantiation.
        /// </summary>
        private ServerInformation() { }

        /// <summary>
        /// String representing the local host address (which is, of course,
        /// 127.0.0.1)
        /// </summary>
        public const String LOCAL_HOST = "127.0.0.1";
        
        /// <summary>
        /// A string standing for the port number used within this 
        /// software application of Task Tracing.
        /// </summary>
        public const int DEFAULT_PORT = 6013;
    }
}
