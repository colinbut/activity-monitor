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
    /// Interface containing methods for implementing class to have the functionality
    /// of a client in a Client - Server communication.
    /// </summary>
    public interface ClientConnection
    {
        /// <summary>
        /// Starts the connection of the Client - Server communication.
        /// </summary>
        /// <param name="hostAddress">the address to specify in order to connect to</param>
        /// <param name="port">the listening port</param>
        void StartConnection(String hostAddress, int port);

        /// <summary>
        /// Stops the connection brutely. 
        /// </summary>
        void EndConnection();
    }
}
