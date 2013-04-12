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

            sock.send(iSocket.aSocket.stringToBytes("Caesura"));

            sock.send(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber));
            sock.send(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber));

            sock.send(iSocket.aSocket.stringToBytes(username));
            sock.send(iSocket.aSocket.stringToBytes(password));

            String recv = iSocket.aSocket.bytesToString(sock.receive(5));

            return "true".Equals(recv);
        }

        public bool requestFile(string filename)
        {
            sock.send(iSocket.aSocket.stringToBytes("RequestFile " + filename));

            // need to code 512 as static (max bytes that can be transfered at once
            String name = iSocket.aSocket.bytesToString(sock.receive(512));

            Int32 numTransfers = Convert.ToInt32(iSocket.aSocket.bytesToString(sock.receive(512)));

            while (numTransfers > 0)
            {

                numTransfers--;
            }

            return true;
            
        }

        public void writeFile(string filename, byte[] bytes)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
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
