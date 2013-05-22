
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Server
{
    public class Server : iServer
    {        // fields need to be public so test cases can get at them

        // Session ID, Connection
        public Dictionary<long, iConnection> connections = new Dictionary<long, iConnection>();

        public iSocket.iSocket socket = new iSocket.aSocket();

        public Boolean running = true;


        public Server()
        {
            var temp = Search.database = new LINQDatabase();
            UserRegistration.setDatabase(temp);
            Search.database = temp;
        }

        public void run()
        {

            iSocket.iSocket temp;
            Connection conn;
            
            while (true)
            {
                    temp = socket.listen(iSocket.constants.defaultPort);
                    // spawn a new connection 
                    conn = new Connection(temp, this);

                    var conThread = new Thread(new ThreadStart(conn.run));
                    conThread.Start();
                
            }


            // }
        }

        public void stop()
        {
            running = false;
        }

        public static void Main()
        {
            Console.WriteLine("Server Started");
            Server newServer = new Server();
            newServer.run();
            Console.WriteLine("Server stoped hit Control-M to quit");
            Console.ReadLine();
        }

 
    }
}
