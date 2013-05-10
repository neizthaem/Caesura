using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSocket
{
    public static class constants
    {
        // Major and Minor numbers to differentiate between different clients
        public static String MajorNumber = "0\0";
        public static String MinorNumber = "0\0";


        // Port the server will listen on
        public static int defaultPort = 5502;
        // IP range the server will listen on
        public static string host = "137.112.255.255";
        public static string serverip = "caesura.csse.rose-hulman.edu";

        // Maximum packet size to be sent over the Sockets
        public static int MAXPACKETSIZE = 8192;

        // The different types of requests the Client can make
        public enum Requests
        {
            Quit,        // Disconnects the client from the server
            Login ,       // Logs on the client to the server
            Register,    // Registers the given userpass
            RequestFile, // Attempts to download the file
            OwnedFiles,  // Gives the client a list of owned files
            SearchTag,   // Gives the client a list of files with that tag
            RequestMail, // Gives the client a list of their mail
            SendMail ,   // Stores the given mail for a certain user
            GetMemory,   // Tells the user how much memory the server is using
            GetUsers,    // Tells the user how many users are online
            AddFile,     // Sets the certain file to be owned by that user
            Exception    // Users by the server to tell the client there was a problem
        
            //AllTags
            //searchownedfiles
        };

    }
}
