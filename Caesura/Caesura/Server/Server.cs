
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

        public static int defaultPort = 6543;
        public static string host = "localhost";

        public static int maxBytes = 512;

        // fields need to be public so test cases can get at them
        public Dictionary<String, iConnection> connections = new Dictionary<string, iConnection>();

        public iSocket.iSocket socket = new iSocket.aSocket();
        public iSQL SQL = new SQL();

        public Boolean running = true;


        public Server()
        {

        }

        public void run()
        {

            iSocket.iSocket temp;
            Connection conn;
            // enter a while loop
            //while (running)
            //{
            // listen on a port
            temp = socket.listen(defaultPort);

            // spawn a new connection 
            conn = new Connection(temp, this);

            Thread tempThread = new Thread(conn.run);
            //tempThread.Start();
            conn.run();


            // }
        }

        public void stop()
        {
            running = false;
        }

        public bool validate(string username, string password)
        {
            Boolean ret = SQL.validate(username, password);
            return ret;
        }

        public void removeConnection(string username)
        {
            throw new NotImplementedException();
        }

        public string[] checkMail(string username)
        {
            throw new NotImplementedException();
        }

        public void sendMail(string sender, string reciever, string message)
        {
            throw new NotImplementedException();
        }

        public string[] ownedFiles(string username)
        {
            throw new NotImplementedException();
        }





        public bool requestFile(string username, string filename)
        {
            Boolean ret = SQL.validateFile(username, filename);
            return ret;
        }
    }
}
