using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Server;

namespace CaesuraTest
{

    [TestFixture()]
    class LoginDownloadTest
    {

        [Test()]
        public static void testLogin()
        {
            
            Thread serverThread = null;
            Server.Server server = new Server.Server();
            Client.Client client = new Client.Client();
            serverThread = new Thread(new ThreadStart(server.run));
            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();
            Assert.True(client.login("Testuser", "Test"));
            client.disconnect();

        }

        [Test()]
        public static void testTransfer()
        {
            Thread serverThread = null;
            Server.Server server = new Server.Server();
            Client.Client client = new Client.Client();
            serverThread = new Thread(new ThreadStart(server.run));
            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();

            client.requestFile("513.txt");

            client.disconnect();
        }

        [Test()]
        public static void testGetUser()
        {
            LINQDatabase database = new LINQDatabase();
            
            
            Assert.AreEqual(database.getUser("Testuser").Username, "Testuser                 ");

        }


    }
}
