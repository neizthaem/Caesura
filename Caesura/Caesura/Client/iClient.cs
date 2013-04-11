using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public interface iClient
    {
        void addFriend(String friendname);

        void removeFriend(String friendname);

        void setStatus(String status);

        String[] checkMail();

        void sendMail(String reciever, String message);
    }
}
