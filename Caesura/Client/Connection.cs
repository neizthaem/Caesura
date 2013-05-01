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
            sock.send(iSocket.aSocket.stringToBytes("Login " + username + ' ' + password, Server.Server.maxBytes));


            String recv = iSocket.aSocket.bytesToMessage(sock.receive(Server.Server.maxBytes));

            bool forDebugger = "True".Equals(recv);
            return forDebugger;
        }



        //PUT IN A DESTINATION EVENTUALLY
        public bool requestFile(string filename, string filepath)
        {
            sock.send(iSocket.aSocket.stringToBytes("RequestFile " + filename, Server.Server.maxBytes));



            // need to code 512 as static (max bytes that can be transfered at once
            String name = iSocket.aSocket.bytesToMessage(sock.receive(Server.Server.maxBytes));

            if ((name.Length > 9) && ("Exception".Equals(name.Substring(0, 9))))
            {
                throw new FileNotFoundException(filename + " was not found");
            }

            if (name.Equals("Access Denied"))
            {
                return false;
            }

            // Size of the file
            Int32 fileSize = iSocket.aSocket.byteToInt(sock.receive(4));

            while (fileSize > iSocket.aSocket.MAXPACKETSIZE)
            {
                byte[] bytes = sock.receive(iSocket.aSocket.MAXPACKETSIZE);
                writeFile(filepath, bytes);
                fileSize = fileSize - iSocket.aSocket.MAXPACKETSIZE;

            }
            if (fileSize > 0)
            {
                byte[] bytes = sock.receive(fileSize);
                writeFile(filepath, bytes);
            }

            return true;

        }

        public void writeFile(string filename, byte[] bytes)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }
            //Console.WriteLine(BitConverter.ToString(bytes));
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Append)))
            {
                writer.BaseStream.Position = (long)writer.BaseStream.Length;
                writer.Write(bytes);
            }


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
