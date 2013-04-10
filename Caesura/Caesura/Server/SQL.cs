using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class SQL : iSQL
    {
        public bool validate(string username, string password)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void addFile(string username, string filename)
        {
            throw new NotImplementedException();
        }

        public void addFriend(string username, string friendname)
        {
            throw new NotImplementedException();
        }

        public void removeFriend(string username, string friendname)
        {
            throw new NotImplementedException();
        }

        public string[] checkMail(string username)
        {
            throw new NotImplementedException();
        }

        public void storeMail(string sender, string reciever, string message)
        {
            throw new NotImplementedException();
        }


        public bool validateFile(string username, string filename)
        {
            //throw new NotImplementedException();r
            return true;
        }
    }
}
