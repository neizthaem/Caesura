using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface iConnection
    {
        Boolean validation();

        void onRecieve(String message);

        void sendMessage(String message);

        void sendFile(String filename);

        void quit();
    }
}
