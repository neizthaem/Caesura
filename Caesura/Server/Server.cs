
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Server
{
    public class Server : iServer
    {


        // it is convient if the major/minor numbers are stored as string
        public static String MajorNumber = "0\0";
        public static String MinorNumber = "0\0";

        public static int defaultPort = 6072;
        public static string host = "localhost";

        public static int maxBytes = 512;

        // fields need to be public so test cases can get at them
        public Dictionary<String, iConnection> connections = new Dictionary<string, iConnection>();

        public iSocket.iSocket socket = new iSocket.aSocket();

        public Boolean running = true;


        public Server()
        {
            UserRegistration.setDatabase(new LINQDatabase());
        }

        public void run()
        {

            iSocket.iSocket temp;
            Connection conn;

            
            while (running)
            {
                temp = socket.listen(defaultPort);
                // spawn a new connection 
                conn = new Connection(temp, this);

                conn.run();
            }


            // }
        }

        public void stop()
        {
            running = false;
        }


    }
}
