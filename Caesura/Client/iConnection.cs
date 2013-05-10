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
        Boolean requestFile(String filename, string filepath);

        void connect();

        void disconnect();

        List<string> GetListOfOwnedFiles(string p);

        bool register(string user, string pass);

        List<string> getFilesWithTags(List<string> tags);

        List<String> getListOfAllTags();

        List<String> getOwnFilesWithTags(string[] tags);

        bool addOwndership(string username, string file);
    }
}
