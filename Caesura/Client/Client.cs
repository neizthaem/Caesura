using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Server;
using System.Threading.Tasks;

namespace Client
{
    public class Client : iClient
    {
        // Needs to be public for test cases
        public iConnection connection;
        private bool loggedIn;

        public Client()
        {
            Search.database = new LINQDatabase();
            connection = new Connection(this);
        }

        public void connect()
        {
            connection.connect();
        }


        public bool requestFile(string filename)
        {
            if(loggedIn)
                return connection.requestFile(filename);
            return false;
        }

        public bool login(string username, string password)
        {
            
            loggedIn = connection.login(username, password);
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


            
            return Search.getFilesWithTags(searcher);
            
        }
    }
}
