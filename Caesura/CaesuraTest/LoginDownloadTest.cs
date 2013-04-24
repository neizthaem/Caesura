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


        public Client.Client client = null;
        public Server.Server server = null;
        

        public Thread serverThread = null;

        [SetUp]
        public void TestClientServerIntegrationSetUp()
        {
            client = new Client.Client();
            server = new Server.Server();

            serverThread = new Thread(new ThreadStart(server.run));
        }

        [TearDown]
        public void TestClientServerIntegrationTearDown()
        {
            serverThread.Abort();
            serverThread = null;
            server.socket.close();
            server = null;
            client = null;
            
        }

        [Test()]
        public void testLogin()
        {
            

            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();
            Assert.True(client.login("Testuser", "Test"));
            client.disconnect();
            serverThread.Abort();

        }

        // Jeff uses this method to prvent files from being overwritten and therefore causing a test to fail.
        // If the file exists a file transfer will append to it
        private void removeFile(String pathname)
        {

            if (System.IO.File.Exists("C:\\Caesura\\" + pathname))
            {
                System.IO.File.Delete("C:\\Caesura\\" + pathname);
            }
            Assert.IsFalse(System.IO.File.Exists("C:\\Caesura\\" + pathname));
        }

        // Since the downloads folder is C:\Caesura\ this method abuses that to check to make sure the transfer occured correctly
        private void assertFile(String pathname)
        {
            Assert.IsTrue(System.IO.File.Exists("C:\\Caesura\\" + pathname));
            // Assert that the contents are correct
            Assert.AreEqual(System.IO.File.ReadAllText(pathname), System.IO.File.ReadAllText("C:\\Caesura\\" + pathname));
        }

        [Test()]
        public void testLoginTransferHex()
        {

            serverThread.Start();
            System.Threading.Thread.Sleep(100);
            client.connect();

            var file = "testpic.jpg";


            if (System.IO.File.Exists("C:\\Caesura\\"+file))
            {
                System.IO.File.Delete("C:\\Caesura\\"+file);
            }
            Assert.IsFalse(System.IO.File.Exists("C:\\Caesura\\"+file));

            Assert.True(client.login("Testuser", "Test"));
            Assert.True(client.requestFile(file));

            client.disconnect();
            serverThread.Abort();

            Assert.IsTrue(System.IO.File.Exists("C:\\Caesura\\"+file));
            // Assert that the contents are correct
            Assert.AreEqual(System.IO.File.ReadAllBytes(file), System.IO.File.ReadAllBytes("C:\\Caesura\\"+file));


        }

        [Test()]
        public void testLoginTransfer()
        {
            
            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();

            Assert.True(client.login("Testuser", "Test"));
            Assert.True(client.requestFile("513.txt"));

            client.disconnect();
            serverThread.Abort();

        }

        [Test()]
        public void testSearching()
        {

            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();

            client.login("Testuser", "Test");
            List<String> toReturn = client.getFromTag("picture");
            List<String> checker = new List<String>();
            checker.Add("picture.jpg");
            checker.Add("picture&text.jpg");
            checker.Add("picture&video.jpg");
            checker.Add("pic&text&video.jpg");

            client.disconnect();
            serverThread.Abort();

            checker.Sort();
            toReturn.Sort();

            Assert.AreEqual(toReturn, checker);

        }

        [Test()]
        public void testSearching2()
        {

            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();

            client.login("Testuser", "Test");
            List<String> toReturn = client.getFromTag("picture","text");
            List<String> checker = new List<String>();
            
            checker.Add("picture&text.jpg");
            checker.Add("pic&text&video.jpg");

            client.disconnect();
            serverThread.Abort();

            checker.Sort();
            toReturn.Sort();

            Assert.AreEqual(toReturn, checker);

        }

        [Test()]
        public void testRegister()
        {

            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();
            User addNew = new User();
            addNew.PasswordHash = "TROLLING";
            addNew.Username = "WTFBRAH";

            Assert.True(UserRegistration.register(addNew));
            

            client.disconnect();
            serverThread.Abort();

        }



        [Test()]
        public void testFailTransfer()
        {
            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
            client.connect();

            Assert.False(client.requestFile("513.txt"));

            client.disconnect();
        }

        [Test()]
        public void testGetUser()
        {
            LINQDatabase database = new LINQDatabase();
            
            
            Assert.AreEqual(database.getUser("Testuser").Username, "Testuser                 ");

        }


    }
}
