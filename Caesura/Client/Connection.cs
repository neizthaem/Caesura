using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Connection : iConnection
    {
        // needs to be public for the test cases
        public iSocket.iSocket sock = null;
        public iClient client = null;

        public Connection(iClient client)
        {
            this.client = client;
            sock = new iSocket.aSocket();
        }

        public bool login(string username, string password)
        {
            sock.connect(Server.Server.host, Server.Server.defaultPort);

            sock.send(iSocket.aSocket.stringToBytes("Caesura" + "\0", Server.Server.maxBytes));

            sock.send(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber, Server.Server.maxBytes));
            sock.send(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber, Server.Server.maxBytes));

            sock.send(iSocket.aSocket.stringToBytes(username + "\0", Server.Server.maxBytes));
            sock.send(iSocket.aSocket.stringToBytes(password + "\0", Server.Server.maxBytes));

            String recv = iSocket.aSocket.bytesToMessage(sock.receive(Server.Server.maxBytes));

            bool forDebugger = "True".Equals(recv);
            return forDebugger;
        }

        //PUT IN A DESTINATION EVENTUALLY
        public bool requestFile(string filename)
        {
            sock.send(iSocket.aSocket.stringToBytes("RequestFile " + filename, Server.Server.maxBytes));

            // need to code 512 as static (max bytes that can be transfered at once
            String name = iSocket.aSocket.bytesToMessage(sock.receive(Server.Server.maxBytes));

            if (name.Equals("Access Denied"))
            {
                return false;
            }

            Int32 numTransfers = Convert.ToInt32(iSocket.aSocket.bytesToMessage(sock.receive(Server.Server.maxBytes)));
            int counter = 100;
            while (numTransfers > 0 && counter > 0)
            {
                Int32 length = 0;
                String temp = "0";
                try
                {
                    temp = iSocket.aSocket.bytesToMessage(sock.receive(Server.Server.maxBytes));
                    length = Convert.ToInt32(temp);
                    counter = 100;
                }
                catch (Exception e)
                {
                    Console.WriteLine("RequestFile 000 :" + e.Message + temp);
                    length = 0;
                    counter--;
                }
                Byte[] bytes = sock.receive(length);
                if (bytes != null)
                {
                    writeFile("C:\\Caesura\\"+ name + ".txt", bytes);
                    numTransfers--;
                }

            }

            return true;

        }

        public void writeFile(string filename, byte[] bytes)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }


            File.AppendAllText(filename, iSocket.aSocket.bytesToString(bytes));

        }

        public void connect()
        {
            sock.connect(Server.Server.host, Server.Server.defaultPort);
        }


        public void disconnect()
        {
            sock.send(iSocket.aSocket.stringToBytes("Quit ", Server.Server.maxBytes));
        }
    }
}
