using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public interface iConnection
    {

        // returns true if successfully logged in
        Boolean login(String username, String password);

        // returns true if the file transfer begins
        Boolean requestFile(String filename);

        // returns an string[] of files that fit the given search parameters
        string[] search(String[] tags);

        void onMessage(String message);

        void sendMessage(String message);
    }
}
