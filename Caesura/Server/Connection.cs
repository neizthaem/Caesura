using Caesura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Connection
    {
        private iSocket sock;
        private Server server;
        private String username;

        // Default constructor is disabled because I specified a constructor
        // Do not create a default constructor
        public Connection(Caesura.iSocket temp, Server server)
        {
            this.sock = temp;
            this.server = server;

        }

        private String bytesToString(byte[] bytes)
        {
            String tempString = System.Text.UTF8Encoding.UTF8.GetString(bytes);
            int tempIndex = tempString.IndexOf('\0', 0);
            return tempString.Substring(0, tempIndex);
        }

        public Boolean validate()
        {
            byte[] tempBytes = new byte[15];
            String tempString = "";
            String tempPassword = "";
            try
            {
                // The rules for handshaking are
                // Caesura
                sock.receive(tempBytes);
                tempString = bytesToString(tempBytes);
                if(! tempString.Equals("Caesura")){
                    throw new Exception("Incorrect program name :["+tempString+"]");
                }
                // Major Number
                sock.receive(tempBytes);
                tempString = bytesToString(tempBytes);
                if (!tempString.Equals(Server.MajorNumber))
                {
                    throw new Exception("Incorrect Major Number :[" + tempString + "]");
                }
                // Minor Number
                sock.receive(tempBytes);
                tempString = bytesToString(tempBytes);
                if (!tempString.Equals(Server.MinorNumber))
                {
                    throw new Exception("Incorrect Minor Number :[" + tempString + "]");
                }
                // Username
                sock.receive(tempBytes);
                tempString = bytesToString(tempBytes);
                // Password
                sock.receive(tempBytes);
                tempPassword = bytesToString(tempBytes);

                
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
