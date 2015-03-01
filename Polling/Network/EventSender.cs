using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

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
    /// Class representing the sender instance. It is an entity of a Client which is primarily
    /// used for transfer data over the TCP sockets connection.
    /// </summary>
    public class EventSender : ClientConnection
    {
        private TcpClient tcpclient = null;
        private NetworkStream ns = null;
        private StreamWriter str = null;
      
        private static EventSender client = new EventSender();

        /// <summary>
        /// Constructor
        /// </summary>
        private EventSender() 
        {
            // Private Constructor to stop outside creation
        }

        /// <summary>
        /// Singleton implementation.
        /// 
        /// </summary>
        /// <returns>the instance of this class</returns>
        public static EventSender GetInstance()
        {
            return client;
        }

        /// <summary>
        /// Starts the tcp connection
        /// </summary>
        /// <param name="hostAddress">the connecting ip address</param>
        /// <param name="port">the connecting ip port</param>
        public void StartConnection(String hostAddress, int port)
        {
            try
            {
                tcpclient = new TcpClient();
                tcpclient.Connect(hostAddress, port);
                ns = tcpclient.GetStream();
                str = new StreamWriter(ns);
            }
            catch 
            {
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Process the message to send.
        /// </summary>
        /// <param name="message">the message to transmit</param>
        public void ProcessMessage(String message)
        {
            if (tcpclient != null)
            {
                if (!tcpclient.Connected)
                {
                    //re-establish connection
                    StartConnection(ServerInformation.LOCAL_HOST, ServerInformation.DEFAULT_PORT);
                }
                else
                {

                    lock (this)//synchronization: multiple threads access this
                    {
                        try
                        {
                           str.WriteLine(message);
                        }
                        catch
                        {
                           EndConnection();
                        }
                    }
                }
            }
        }

  
        /// <summary>
        /// Ends the tcp connection
        /// </summary>
        public void EndConnection()
        {
            if (str != null)
            {
                str.Close();
            }

            if (ns != null)
            {
                ns.Close();
            }

            if (tcpclient != null)
            {
                tcpclient.Close();
            }

            Environment.Exit(1);
        }
     
    }
}
