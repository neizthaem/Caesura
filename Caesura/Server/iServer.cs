using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface iServer
    {

        void run();

        void stop();

        Boolean validate(String username, String password);

        void removeConnection(String username);

        String[] checkMail(String username);

        void sendMail(String sender, String reciever, String message);

        Boolean requestFile(String username, String filename);

        String[] ownedFiles(String username);
    }
}
