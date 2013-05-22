
using iSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Server;
using System.Threading;

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
            try
            {
                while (running)
                {
                    temp = iSocket.aSocket.bytesToString(sock.receive(iSocket.constants.MAXPACKETSIZE));
                    onRecieve(temp);
                }
            }
            catch (Exception e)
            {

                quit();
                Console.WriteLine("Exception in Connection.run, closing the connection:" + e.Message);
            }
        }

        public void quit()
        {
            sock.close();
            running = false;
        }

        public void onRecieve(string message)
        {
            string[] parsed = splitMessage(message.TrimEnd((char)0x00));
            // Message will contrain two parts
            // Type
            string type = parsed[0];
            // Params
            string param = parsed[1];
            
            if (("login".Equals(type)))
            {
                Console.WriteLine("Login " + param);
                string[] userPass = param.Split(' ');
                bool returner;
                try
                {
                    if (userPass.Length > 2)
                    {
                        throw new Exception();
                    }                    
                    returner = UserRegistration.login(userPass[0], userPass[1]);
                    username = userPass[0];
                    //Console.WriteLine("User Registration thinks : " + returner);
                }
                catch
                {
                    returner = false;
                    sock.send(iSocket.aSocket.stringToBytes(returner.ToString(), iSocket.constants.MAXPACKETSIZE));
                    //sock.close();
                    throw;
                }
                sock.send(iSocket.aSocket.stringToBytes(returner.ToString(), iSocket.constants.MAXPACKETSIZE));

            }
            else if ("register".Equals(type))
            {
                string[] userPass = param.Split(' ');
                bool returner;
                try
                {
                    if (userPass.Length > 2)
                    {
                        throw new Exception();
                    }
                    string newUsername = userPass[0];
                    string newUserpass = userPass[1];
                    Console.WriteLine("register " + newUsername + ' ' + newUserpass);
                    returner = UserRegistration.register(new User(newUsername, newUserpass));
                    //Console.WriteLine("User Registration thinks : " + returner);
                }
                catch
                {
                    returner = false;
                    sock.send(iSocket.aSocket.stringToBytes(returner.ToString(), iSocket.constants.MAXPACKETSIZE));
                    //sock.close();
                    throw;
                }
                sock.send(iSocket.aSocket.stringToBytes(returner.ToString(), iSocket.constants.MAXPACKETSIZE));

            }
            else if (username != null)
            {

                if ("requestfile".Equals(type))
                {
                    Console.WriteLine("Requestfile " + param);
                    sendFile(param);

                }
                else if ("ownedfiles".Equals(type))
                {
                    Console.WriteLine("Ownedfiles " + param);
                    ownedFiles(param);
                }

                
                else if ("searchtag".Equals(type))
                {
                    Console.WriteLine("searchtag " + param);
                    searchtag(message);
                }
                else if ("alltags".Equals(type))
                {
                    Console.WriteLine("alltags " + param);
                    alltags();
                }
                else if ("searchownedfiles".Equals(type))
                {
                    Console.WriteLine("searchownedfiles " + param);
                    searchownedfiles(message);
                }
                else if ("quit".Equals(type))
                {
                    Console.WriteLine("Quit " + param);
                    quit();
                }
                else if ("addfile".Equals(type))
                {
                    string[] userPass = param.Split(' ');
                    bool returner;
                    try
                    {
                        Console.WriteLine("addfile " + userPass[0] + ' ' + userPass[1]);
                        returner = Search.database.AddOwnership(userPass[0], userPass[1]);
                        //Console.WriteLine("User Registration thinks : " + returner);
                    }
                    catch
                    {
                        returner = true;
                        sock.send(iSocket.aSocket.stringToBytes(returner.ToString(), iSocket.constants.MAXPACKETSIZE));
                        //sock.close();
                        throw;
                    }
                    sock.send(iSocket.aSocket.stringToBytes(returner.ToString(), iSocket.constants.MAXPACKETSIZE));


                }
                else if ("message".Equals(type))
                {
                    string ret = "False";
                    try
                    {
                        string[] args = param.Split(' ');
                        String tempMessage = param.Substring(param.IndexOf(' '));
                        //Send mail requires them to be users
                        Console.WriteLine("message T:F:M" + ' ' + args[0] + ' ' + username + ' ' + tempMessage);

                        Search.database.SendMail(new User(args[0], null), new User(username, null), tempMessage);
                        ret = "True";
                    }
                    catch
                    {
                        ret = "False";
                    }
                    sock.send(iSocket.aSocket.stringToBytes(ret.ToString(), iSocket.constants.MAXPACKETSIZE));

                }
                else if ("checkmail".Equals(type))
                {
                    Console.WriteLine("checkmail " + username);
                    List<Mail> mail = Search.database.CheckMail(username);

                    // if there is no mail just say that
                    if (mail.Count == 0)
                    {
                        sock.send(iSocket.aSocket.stringToBytes(mail.Count.ToString() + ' ', iSocket.constants.MAXPACKETSIZE));
                        return;
                    }

                    int transfers = mail.Count;
                    Mail m = mail[0];
                    String sender = m.From;
                    String mailMessage = m.Message;
                    int id = m.ID;

                    sock.send(iSocket.aSocket.stringToBytes(mail.Count.ToString() + ' ' + sender + ' ' + id + ' ' + mailMessage, iSocket.constants.MAXPACKETSIZE));

                    Console.WriteLine(sender + ' ' + id + ' ' + mailMessage);

                    mail.RemoveAt(0);
                    while (mail.Count > 0)
                    {
                        m = mail[0];
                        sender = m.From;
                        mailMessage = m.Message;
                        id = m.ID;


                        sock.send(iSocket.aSocket.stringToBytes(sender + ' ' + id + ' ' + mailMessage + ' ', iSocket.constants.MAXPACKETSIZE));
                        mail.RemoveAt(0);

                        Console.WriteLine(sender + ' ' + id + ' ' + mailMessage);
                    }

                }
            }
        }

        private void searchownedfiles(string message)
        {
            var splitRecv = message.Split(' ');

            // First split is the type of message

            //Transfers is the second split
            long numberOfTags = long.Parse(splitRecv[1]);

            List<string> tags = new List<String>();
            for (int i = 2; i < splitRecv.Length; i++)
            {
                tags.Add(splitRecv[i].TrimEnd((char)(0x00)));
            }

            while (tags.Count < numberOfTags)
            {
                splitRecv = iSocket.aSocket.bytesToString(sock.receive(iSocket.constants.MAXPACKETSIZE)).Split(' ');
                // Add it to the list
                for (int i = 0; i < splitRecv.Length; i++)
                {
                    tags.Add(splitRecv[i].TrimEnd((char)(0x00)));
                }
            }
            // now we have a list of tags

            // lets get a list of files
            var files = Search.getFilesWithTagsOwnedBy(username, tags.ToArray());

            // First string to be sent needs the number of files
            long numberOfFiles = files.Count;

            String sendString = numberOfFiles.ToString();

            foreach (String s in files)
            {
                // If the next string would make the packet too long
                if (((' ' + s).Length + sendString.Length) > iSocket.constants.MAXPACKETSIZE)
                {

                    // Send it
                    sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
                    // Set the list to be the element that couldn't be added
                    sendString = s;
                }
                else
                {
                    // the next string can be added to the sendString
                    sendString = sendString + ' ' + s;
                }
            }

            // Check to make sure that everything was sent
            if (sendString.Length > 0)
            {
                // Since it wasn't all sent, send it
                sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
            }

            // And we're done?
            return;
        }

        private void alltags()
        {
            List<String> temp = Search.database.getListOfAllTags();

            // Now we have a list of tags

            // First string to be sent needs the number of files
            long numberOfFiles = temp.Count;

            String sendString = numberOfFiles.ToString();

            foreach (String s in temp)
            {
                // If the next string would make the packet too long
                if (((' ' + s).Length + sendString.Length) > iSocket.constants.MAXPACKETSIZE)
                {

                    // Send it
                    sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
                    // Set the list to be the element that couldn't be added
                    sendString = s;
                }
                else
                {
                    // the next string can be added to the sendString
                    sendString = sendString + ' ' + s;
                }
            }

            // Check to make sure that everything was sent
            if (sendString.Length > 0)
            {
                // Since it wasn't all sent, send it
                sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
            }

            // And we're done?
            return;

        }

        public List<string> getFromTag(List<string> tag)
        {
            //if (loggedIn)

            string[] searcher = new string[tag.Count];

            for (int x = 0; x < tag.Count; x++)
            {
                searcher[x] = tag.ElementAt(x);
            }

            return Search.getFilesWithTags(searcher);

        }

        private void searchtag(string message)
        {
            var splitRecv = message.Split(' ');

            // First split is the type of message

            //Transfers is the second split
            long numberOfTags = long.Parse(splitRecv[1]);

            List<string> tags = new List<String>();
            for (int i = 2; i < splitRecv.Length; i++)
            {
                tags.Add(splitRecv[i].TrimEnd((char)(0x00)));
            }

            while (tags.Count < numberOfTags)
            {
                splitRecv = iSocket.aSocket.bytesToString(sock.receive(iSocket.constants.MAXPACKETSIZE)).Split(' ');
                // Add it to the list
                for (int i = 0; i < splitRecv.Length; i++)
                {
                    tags.Add(splitRecv[i].TrimEnd((char)(0x00)));
                }
            }

            // Now we have a list of tags

            var fileList = getFromTag(tags);

            // First string to be sent needs the number of files
            long numberOfFiles = fileList.Count;

            String sendString = numberOfFiles.ToString();

            foreach (String s in fileList)
            {
                // If the next string would make the packet too long
                if (((' ' + s).Length + sendString.Length) > iSocket.constants.MAXPACKETSIZE)
                {

                    // Send it
                    sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
                    // Set the list to be the element that couldn't be added
                    sendString = s;
                }
                else
                {
                    // the next string can be added to the sendString
                    sendString = sendString + ' ' + s;
                }
            }

            // Check to make sure that everything was sent
            if (sendString.Length > 0)
            {
                // Since it wasn't all sent, send it
                sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
            }

            // And we're done?
            return;
        }

        private void ownedFiles(string param)
        {
            var listOfFiles = Search.database.GetListOfOwnedFiles(this.username);

            // First string to be sent needs the number of files
            long numberOfFiles = listOfFiles.Count;

            String sendString = numberOfFiles.ToString();

            foreach (String s in listOfFiles)
            {
                // If the next string would make the packet too long
                if (((' ' + s).Length + sendString.Length) > iSocket.constants.MAXPACKETSIZE)
                {

                    // Send it
                    sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
                    // Set the list to be the element that couldn't be added
                    sendString = s;
                }
                else
                {
                    // the next string can be added to the sendString
                    sendString = sendString + ' ' + s;
                }
            }

            // Check to make sure that everything was sent
            if (sendString.Length > 0)
            {
                // Since it wasn't all sent, send it
                sock.send(iSocket.aSocket.stringToBytes(sendString, iSocket.constants.MAXPACKETSIZE));
            }

            // And we're done?
            return;
        }


        public void sendFile(string filename)
        {

            int length;
            int sentLength = 0;

            if (System.IO.File.Exists("C://Caesura//" + filename))
            {
                length = fileSize("C://Caesura//" + filename);

                // Send filename if it exists
                // if it doesn't exist send an exception
                sock.send(iSocket.aSocket.stringToBytes(filename, iSocket.constants.MAXPACKETSIZE));
            }
            else
            {
                sock.send(iSocket.aSocket.stringToBytes("Exception " + filename + " was not found", iSocket.constants.MAXPACKETSIZE));
                return;
            }


            // Length of the file
            sock.send(iSocket.aSocket.intToByte(length));

            while (length > 8192)
            {
                // transfers
                sock.send(readFile("C://Caesura//" + filename, sentLength, iSocket.constants.MAXPACKETSIZE));
                sentLength += iSocket.constants.MAXPACKETSIZE;
                length -= iSocket.constants.MAXPACKETSIZE;

            }

            // Last transfer
            if (length > 0)
            {
                sock.send(readFile("C://Caesura//" + filename, sentLength, length));
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

            using (BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite)))
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
