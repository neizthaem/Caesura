﻿using System;
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
        private bool loggedIn;

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
    }
}
