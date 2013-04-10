using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface iSQL
    {
        Boolean validate(String username, String password);

        Boolean validateFile(String username, String filename);

        void addFile(String username, String filename);

        void addFriend(String username, String friendname);

        void removeFriend(String username, String friendname);

        String[] checkMail(String username);

        void storeMail(String sender, String reciever, String message);
    }
}
