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
            //sock.send(iSocket.aSocket.stringToBytes(iSocket.constants.Requests.Login + ' ' + username + ' ' + password, iSocket.constants.MAXPACKETSIZE));
            sock.send(iSocket.aSocket.stringToBytes("login" + ' ' + username + ' ' + password, iSocket.constants.MAXPACKETSIZE));


            String recv = iSocket.aSocket.bytesToMessage(sock.receive(iSocket.constants.MAXPACKETSIZE));

            bool forDebugger = "True".Equals(recv);
            return forDebugger;
        }

        public bool requestFile(string filename, string filepath)
        {
            // sock.send(iSocket.aSocket.stringToBytes(iSocket.constants.Requests.RequestFile + ' ' + filename, iSocket.constants.MAXPACKETSIZE));
            sock.send(iSocket.aSocket.stringToBytes("requestfile" + ' ' + filename, iSocket.constants.MAXPACKETSIZE));

            // need to code 512 as static (max bytes that can be transfered at once
            String name = iSocket.aSocket.bytesToMessage(sock.receive(iSocket.constants.MAXPACKETSIZE));

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
            sock.connect(iSocket.constants.serverip, iSocket.constants.defaultPort);
        }


        public void disconnect()
        {
            sock.send(iSocket.aSocket.stringToBytes("quit" + ' ', iSocket.constants.MAXPACKETSIZE));
        }


        public List<string> getOwnFilesWithTags(string[] searcher)
        {
            // -- Send part
            String sendString = "searchownedfiles" + ' ' + searcher.Length;

            foreach (String s in searcher)
            {
                // Basically the same protocal as everywhere else that I send a list of data
                if (((' ' + s).Length + sendString.Length) > iSocket.constants.MAXPACKETSIZE)
                {
                    sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));

                    sendString = s;
                }
                else
                {
                    sendString = sendString + ' ' + s;
                }
            }
            if (sendString.Length > 0)
            {
                sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
            }


            /// -- Recv part

            // First thing back has number of transfers
            var firstRecv = sock.receive(iSocket.constants.MAXPACKETSIZE);

            // Need to split it based upond ' '
            var splitRecv = iSocket.aSocket.bytesToString(firstRecv).Split(' ');
            // First split is transfers

            long numberOfFiles = long.Parse(splitRecv[0]);
            // Rest of actual tags
            List<string> returnValue = new List<string>();
            for (int i = 1; i < splitRecv.Length; i++)
            {
                returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
            }

            // Then sequential transfers
            while (returnValue.Count < numberOfFiles)
            {
                // Listen for a new transfer
                var recv = sock.receive(iSocket.constants.MAXPACKETSIZE);

                splitRecv = iSocket.aSocket.bytesToString(recv).Split(' ');

                // Add it to the list
                for (int i = 0; i < recv.Length; i++)
                {
                    returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
                }
            }

            return returnValue;
        }

        public List<string> GetListOfOwnedFiles(string p)
        {
            // Why do we send the username?

            //sock.send(iSocket.aSocket.stringToBytes(iSocket.constants.Requests.OwnedFiles.ToString() + ' ', iSocket.constants.MAXPACKETSIZE));
            sock.send(iSocket.aSocket.stringToBytes("ownedfiles" + ' ', iSocket.constants.MAXPACKETSIZE));

            // First thing back has number of transfers
            var firstRecv = sock.receive(iSocket.constants.MAXPACKETSIZE);

            // Need to split it based upond ' '
            var splitRecv = iSocket.aSocket.bytesToString(firstRecv).Split(' ');
            // First split is transfers

            long numberOfFiles = long.Parse(splitRecv[0]);
            // Rest of actual tags
            List<string> returnValue = new List<string>();
            for (int i = 1; i < splitRecv.Length; i++)
            {
                returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
            }

            // Then sequential transfers
            while (returnValue.Count < numberOfFiles)
            {
                // Listen for a new transfer
                var recv = sock.receive(iSocket.constants.MAXPACKETSIZE);

                splitRecv = iSocket.aSocket.bytesToString(recv).Split(' ');

                // Add it to the list
                for (int i = 0; i < recv.Length; i++)
                {
                    returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
                }
            }

            return returnValue;
        }


        public bool register(string user, string pass)
        {
            //sock.send(iSocket.aSocket.stringToBytes(iSocket.constants.Requests.Login + ' ' + username + ' ' + password, iSocket.constants.MAXPACKETSIZE));
            sock.send(iSocket.aSocket.stringToBytes("register" + ' ' + user + ' ' + pass, iSocket.constants.MAXPACKETSIZE));


            String recv = iSocket.aSocket.bytesToMessage(sock.receive(iSocket.constants.MAXPACKETSIZE));

            bool forDebugger = "True".Equals(recv);
            return forDebugger;
        }



        public List<string> getFilesWithTags(List<string> tags)
        {
            // -- Send part
            String sendString = "searchtag" + ' ' + tags.Count;

            foreach (String s in tags)
            {
                // Basically the same protocal as everywhere else that I send a list of data
                if (((' ' + s).Length + sendString.Length) > iSocket.constants.MAXPACKETSIZE)
                {
                    sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));

                    sendString = s;
                }
                else
                {
                    sendString = sendString + ' ' + s;
                }
            }
            if (sendString.Length > 0)
            {
                sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
            }


            /// -- Recv part

            // First thing back has number of transfers
            var firstRecv = sock.receive(iSocket.constants.MAXPACKETSIZE);

            // Need to split it based upond ' '
            var splitRecv = iSocket.aSocket.bytesToString(firstRecv).Split(' ');
            // First split is transfers

            long numberOfFiles = long.Parse(splitRecv[0]);
            // Rest of actual tags
            List<string> returnValue = new List<string>();
            for (int i = 1; i < splitRecv.Length; i++)
            {
                returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
            }

            // Then sequential transfers
            while (returnValue.Count < numberOfFiles)
            {
                // Listen for a new transfer
                var recv = sock.receive(iSocket.constants.MAXPACKETSIZE);

                splitRecv = iSocket.aSocket.bytesToString(recv).Split(' ');

                // Add it to the list
                for (int i = 0; i < recv.Length; i++)
                {
                    returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
                }
            }

            return returnValue;
        }


        public List<String> getListOfAllTags()
        {
            // Why do we send the username?

            //sock.send(iSocket.aSocket.stringToBytes(iSocket.constants.Requests.OwnedFiles.ToString() + ' ', iSocket.constants.MAXPACKETSIZE));
            sock.send(iSocket.aSocket.stringToBytes("alltags" + ' ', iSocket.constants.MAXPACKETSIZE));

            // First thing back has number of transfers
            var firstRecv = sock.receive(iSocket.constants.MAXPACKETSIZE);

            // Need to split it based upond ' '
            var splitRecv = iSocket.aSocket.bytesToString(firstRecv).Split(' ');
            // First split is transfers

            long numberOfFiles = long.Parse(splitRecv[0]);
            // Rest of actual tags
            List<string> returnValue = new List<string>();
            for (int i = 1; i < splitRecv.Length; i++)
            {
                returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
            }

            // Then sequential transfers
            while (returnValue.Count < numberOfFiles)
            {
                // Listen for a new transfer
                var recv = sock.receive(iSocket.constants.MAXPACKETSIZE);

                splitRecv = iSocket.aSocket.bytesToString(recv).Split(' ');

                // Add it to the list
                for (int i = 0; i < recv.Length; i++)
                {
                    returnValue.Add(splitRecv[i].TrimEnd((char)(0x00)));
                }
            }

            return returnValue;
        }



        public bool addOwndership(string username, string file)
        {
            //sock.send(iSocket.aSocket.stringToBytes(iSocket.constants.Requests.Login + ' ' + username + ' ' + password, iSocket.constants.MAXPACKETSIZE));
            sock.send(iSocket.aSocket.stringToBytes("addfile" + ' ' + username + ' ' + file, iSocket.constants.MAXPACKETSIZE));


            String recv = iSocket.aSocket.bytesToMessage(sock.receive(iSocket.constants.MAXPACKETSIZE));

            bool forDebugger = "True".Equals(recv);
            return forDebugger;
        }
    }
}
