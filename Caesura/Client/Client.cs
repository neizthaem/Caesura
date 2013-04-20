using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client : iClient
    {
        // Needs to be public for test cases
        public iConnection connection;

        public Client()
        {
            connection = new Connection(this);
        }

        public void connect()
        {
            connection.connect();
        }


        public bool requestFile(string filename)
        {
            return connection.requestFile(filename);
        }

        public bool login(string username, string password)
        {
            return connection.login(username, password);
        }




        public void disconnect()
        {
            connection.disconnect();
        }
    }
}
