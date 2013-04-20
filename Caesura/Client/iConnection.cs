using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public interface iConnection
    {

        Boolean login(string username, string password);
        // returns true if the file transfer begins
        Boolean requestFile(String filename);

        void connect();

        void disconnect();
    }
}
