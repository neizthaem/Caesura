
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Caesura;
using Caesura.iSocket;
using Caesura.aSocket;

namespace Server
{
    class Server
    {
        public static String MajorNumber = "0";
        public static String MinorNumber = "0";

        Dictionary<String, Connection> connections = new Dictionary<string,Connection>();
        Mailbox mailbox = new Mailbox();
        int defaultPort = 6543;
        iSocket socket = new aSocket();

        public Server()
        {

        }

        public void run()
        {
            iSocket temp; 
            // enter a while loop
            while (true)
            {
                // listen on a port
                temp = socket.listen(defaultPort);
                // spawn a new connection - let it add itself if neccesary
                new Connection(temp, this);

            }

        }
    }
}
