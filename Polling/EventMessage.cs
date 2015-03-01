using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polling
{
    public enum MESSAGETYPE{ FILE, FOLDER, WEBPAGE, APPLICATION, EMAIL };

    public struct EventMessage
    {
        private String _name;
        private String _message;
        private MESSAGETYPE _type;
        private int _time;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventMessage(String name, String message)
        {
            _name = name;
            _message = message;
            _time = 0;
            _type = MESSAGETYPE.EMAIL;
        }

        /// <summary>
        /// Gets the name of the message. 
        /// </summary>
        /// <return>the name of the message as a string</return>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets the message stored.
        /// </summary>
        /// <return>the message body</return>
        public String Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public MESSAGETYPE MESSAGETYPE
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
