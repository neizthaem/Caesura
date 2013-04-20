
using iSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Server;

namespace Server
{
    public class Connection : iConnection
    {
        private iSocket.iSocket sock;

        private iServer server;
        // needs to be public for test cases
        public string username;
        public Boolean running = true;

        // Default constructor is disabled because I specified a constructor
        // Do not create a default constructor
        public Connection(iSocket.iSocket temp, iServer server)
        {
            this.sock = temp;
            this.server = server;

        }

        public String[] splitMessage(string message)
        {

            int splitIndex = message.IndexOf(' ');
            string[] ret = new string[2];

            if (splitIndex < 0)
            {
                ret[0] = message;
                ret[1] = "";
                return ret;
            }

            ret[0] = message.Substring(0, splitIndex);
            ret[1] = message.Substring(splitIndex + 1, message.Length - splitIndex - 1);
            return ret;
        }

        public void run()
        {
            String temp;
            while (running)
            {
                temp = iSocket.aSocket.bytesToMessage(sock.receive(Server.maxBytes));
                onRecieve(temp);
            }
        }

        public void quit()
        {
            sock.close();
            running = false;
        }

        public void onRecieve(string message)
        {
            string[] parsed = splitMessage(message);
            // Message will contrain two parts
            // Type
            string type = parsed[0];
            // Params
            string param = parsed[1];

            if ("RequestFile".Equals(type))
            {

                sendFile(param);

            }
            else if ("Login".Equals(type))
            {
                string[] userPass = param.Split(' ');
                bool returner = UserRegistration.login(userPass[0], userPass[1]);
                sock.send(iSocket.aSocket.stringToBytes(returner.ToString(), Server.maxBytes));
            }
            else if ("Quit".Equals(type))
            {

                quit();
            }
        }


        public void sendFile(string filename)
        {
            int maxBytes = 512;
            int length = fileSize(filename);
            int transferLength;
            int sentLength = 0;
            // Send file name - don't send the extension for test purposes
            sock.send(iSocket.aSocket.stringToBytes(filename.Substring(0, filename.Length - 4), Server.maxBytes));
            // Number of transfers
            int transfers = (int)Math.Ceiling((double)((double)length / (double)maxBytes));
            sock.send(iSocket.aSocket.stringToBytes(transfers.ToString(), Server.maxBytes));
            while (length > 0)
            {
                // Length of a transfer
                transferLength = Math.Min(maxBytes, length);
                sock.send(iSocket.aSocket.stringToBytes(transferLength.ToString(), Server.maxBytes));
                // transfers
                sock.send(readFile(filename, sentLength, transferLength));
                sentLength += transferLength;
                length -= transferLength;

            }
            return;
        }

        public int fileSize(string filename)
        {
            FileInfo f = new FileInfo(filename);
            return (int)f.Length;
        }

        // Public for the purpose of testing
        // length of 0 means till end of file
        public byte[] readFile(string filename, int index, int length)
        {

            using (BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open,FileAccess.Read)))
            {
                int pos = index;
                int end;
                if (length == 0)
                {
                    end = (int)b.BaseStream.Length;
                }
                else
                {
                    end = length;
                }
                b.ReadBytes(pos);
                Byte[] ret = b.ReadBytes(end);
                b.Close();
                return ret;
            }
        }
    }
}
