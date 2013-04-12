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

            sock.send(iSocket.aSocket.stringToBytes("Caesura"+"\0"));

            sock.send(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber));
            sock.send(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber));

            sock.send(iSocket.aSocket.stringToBytes(username + "\0"));
            sock.send(iSocket.aSocket.stringToBytes(password + "\0"));

            String recv = iSocket.aSocket.bytesToMessage(sock.receive(5));

            bool forDebugger = "True".Equals(recv);
            return forDebugger;
        }

        public bool requestFile(string filename)
        {
            sock.send(iSocket.aSocket.stringToBytes("RequestFile " + filename));

            // need to code 512 as static (max bytes that can be transfered at once
            String name = iSocket.aSocket.bytesToString(sock.receive(512));

            if (name.Equals("Access Denied"))
            {
                return false;
            }

            Int32 numTransfers = Convert.ToInt32(iSocket.aSocket.bytesToString(sock.receive(512)));
            int counter = 100;
            while (numTransfers > 0 && counter > 0)
            {
                Int32 length = 0;
                String temp = "0";
                try
                {
                    temp = iSocket.aSocket.bytesToString(sock.receive(512));
                    length = Convert.ToInt32(temp);
                    counter = 100;
                }
                catch (Exception e)
                {
                    Console.WriteLine("RequestFile 000 :" + e.Message + temp);
                    length = 0;
                    counter --;
                }
                Byte[] bytes = sock.receive(length);
                if (bytes != null)
                {
                    writeFile(name, bytes);
                    numTransfers--;
                }

            }

            return true;

        }

        public void writeFile(string filename, byte[] bytes)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Write(bytes);
            }

        }

        public string[] search(string[] tags)
        {
            throw new NotImplementedException();
        }

        public void onMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void sendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
