
using iSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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



        public Boolean validation()
        {
            byte[] tempBytes = new byte[15];
            String tempString = "";
            String tempPassword = "";
            String fullString = "";
            try
            {
                // The rules for handshaking are
                // Caesura
                tempBytes = sock.receive(15);
                fullString = iSocket.aSocket.bytesToString(tempBytes);
                tempString = iSocket.aSocket.bytesToMessage(tempBytes);
                if (!"Caesura".Equals(tempString))
                {
                    throw new Exception("Incorrect program name :[" + tempString + "]");
                }

                tempBytes = new byte[15];

                // Major Number
                tempBytes = sock.receive(15);
                tempString = iSocket.aSocket.bytesToMessage(tempBytes);
                fullString = iSocket.aSocket.bytesToString(tempBytes);
                if (!Server.MajorNumber.Substring(0, Server.MajorNumber.Length - 1).Equals(tempString))
                {
                    throw new Exception("Incorrect Major Number :[" + tempString + "]");
                }

                tempBytes = new byte[15];

                // Minor Number
                tempBytes = sock.receive(15);
                tempString = iSocket.aSocket.bytesToMessage(tempBytes);
                fullString = iSocket.aSocket.bytesToString(tempBytes);
                if (!Server.MinorNumber.Substring(0, Server.MajorNumber.Length - 1).Equals(tempString))
                {
                    throw new Exception("Incorrect Minor Number :[" + tempString + "]");
                }

                tempBytes = new byte[15];

                // Username
                tempBytes = sock.receive(15);
                fullString = iSocket.aSocket.bytesToString(tempBytes);
                tempString = iSocket.aSocket.bytesToMessage(tempBytes);

                tempBytes = new byte[15];

                // Password
                tempBytes = sock.receive(15);
                fullString += ":" + iSocket.aSocket.bytesToString(tempBytes);
                tempPassword = iSocket.aSocket.bytesToMessage(tempBytes);

                username = tempString;

                Boolean ret = server.validate(tempString, tempPassword);

                sock.send(iSocket.aSocket.stringToBytes(ret.ToString()));
                return ret;
            }

            catch (Exception e)
            {
                Console.WriteLine("Server.Connection.Validation():" + e.Message + " | " + e.Source + "|" + fullString);
                return false;
            }
        }

        public String[] splitMessage(string message)
        {
            int splitIndex = message.IndexOf(' ');

            string[] ret = new string[2];
            ret[0] = message.Substring(0, splitIndex);
            ret[1] = message.Substring(splitIndex + 1, message.Length - splitIndex - 1);
            return ret;
        }

        public void run()
        {
            String temp;
            while (running)
            {
                temp = iSocket.aSocket.bytesToMessage(sock.receive(30));
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
            else if ("Quit".Equals(type))
            {

                quit();
            }
        }

        public void sendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void sendFile(string filename)
        {
            int maxBytes = 512;
            int length = fileSize(filename);
            int transferLength;
            int sentLength = 0;
            // Send file name - don't send the extension for test purposes
            sock.send(iSocket.aSocket.stringToBytes(filename.Substring(0, filename.Length - 4)));
            // Number of transfers
            int transfers = (int)Math.Ceiling((double)((double)length / (double)maxBytes));
            sock.send(iSocket.aSocket.stringToBytes(transfers.ToString()));
            while (length > 0)
            {
                // Length of a transfer
                transferLength = Math.Min(maxBytes, length);
                sock.send(iSocket.aSocket.stringToBytes(transferLength.ToString()));
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

            using (BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open)))
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
                return b.ReadBytes(end);
            }
        }
    }
}
