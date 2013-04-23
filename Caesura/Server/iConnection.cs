using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface iConnection
    {

        void onRecieve(String message);

        void sendFile(String filename);

        void quit();
    }
}
