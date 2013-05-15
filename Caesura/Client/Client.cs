using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client : iClient
    {
        // Needs to be public for test cases
        public iConnection connection;
        public bool loggedIn = false;

        public Client()
        {
            connection = new Connection(this);
        }

        public void connect()
        {
            connection.connect();
        }

        public bool sendMessage(string toUser, string message)
        {
            return connection.sendMessage(toUser, message);
        }

        public object checkMail()
        {
            throw new NotImplementedException();
        }



        public bool requestFile(string filename, string filepath)
        {
            if(loggedIn)
                return connection.requestFile(filename, filepath);
            return false;
        }

        public bool login(string username, string password)
        {
            
            loggedIn = connection.login(username, password);
            this.username = username;
            return loggedIn;
        }




        public void disconnect()
        {
            connection.disconnect();
        }

        public List<string> getFromTag(List<string> tag)
        {
            //if (loggedIn)

            string[] searcher = new string[tag.Count];

            for (int x = 0; x < tag.Count; x++)
            {
                searcher[x] = tag.ElementAt(x);
            }


            
            return connection.getFilesWithTags(searcher.ToList());
            
        }

        public List<string> getOwned()
        {
            //Implement here

            return connection.GetListOfOwnedFiles("Testuser");
        }

        public string username { get; set; }

        public List<string> getListOfAllTags()
        {
            return connection.getListOfAllTags();
        }

        public List<string> getOwnFilesWithTags(string username, string[] tags)
        {
            return connection.getOwnFilesWithTags(tags);
        }

        public bool AddOwnership(string username, string file)
        {
            return connection.addOwndership(username, file);
        }

        public bool register(string user, string pass)
        {
            return connection.register(user, pass);
        }

        public List<String> getFilesWithTag(List<string> tags)
        {
            return connection.getFilesWithTags(tags);
        }

        internal List<string> getFilesWithTagsOwnedBy(string username, string[] tags)
        {
            return connection.getOwnFilesWithTags(tags);
        }
    }
}
