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

        public void addFriend(string friendname)
        {
            throw new NotImplementedException();
        }

        public void removeFriend(string friendname)
        {
            throw new NotImplementedException();
        }

        public void setStatus(string status)
        {
            throw new NotImplementedException();
        }

        public string[] checkMail()
        {
            throw new NotImplementedException();
        }

        public void sendMail(string reciever, string message)
        {
            throw new NotImplementedException();
        }

        public bool requestFile(string filename)
        {
            return connection.requestFile(filename);
        }



    }
}
