using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaesuraTest
{
    [TestFixture]
    class TestClientServerIntegration
    {
        Client.Client client = null;
        Server.Server server = null;
        MockRepository mocks = null;

        Thread serverThread = null;

        [SetUp]
        public void TestClientServerIntegrationSetUp()
        {
            client = new Client.Client();
            server = new Server.Server();
            mocks = new MockRepository();
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
            mocks = null;
        }

        [Test]
        public void TestClientServerIntegrationClientRequestFileGeneric()
        {
            serverThread.Start();

            // Sleep to let the server start up
            System.Threading.Thread.Sleep(5000);

            if (File.Exists("C:\\Caesura\\generic.txt"))
            {
                File.Delete("C:\\Caesura\\generic.txt");
            }

            Assert.IsFalse(File.Exists("C:\\Caesura\\generic.txt"));
            client.connect();
            // Request the file
            //client.requestFile("generic.txt");
            client.disconnect();
            // Asert that the file exists
            Assert.IsTrue(File.Exists("C:\\Caesura\\generic.txt"));
            // Assert that the contents are correct
            Assert.AreEqual(File.ReadAllText("generic.txt"), File.ReadAllText("C:\\Caesura\\generic.txt"));
        }

        [Test]
        public void TestClientServerIntegrationClientRequestFileOver512()
        {
            serverThread.Start();

            // Sleep to let the server start up
            System.Threading.Thread.Sleep(5000);

            if (File.Exists("513"))
            {
                File.Delete("513");
            }

            Assert.IsFalse(File.Exists("513"));
            client.connect();
            // Request the file
            //client.requestFile("513.txt");
            client.disconnect();
            // Asert that the file exists
            Assert.IsTrue(File.Exists("513"));
            // Assert that the contents are correct
            Assert.AreEqual(File.ReadAllText("513.txt"), File.ReadAllText("513"));
        }
    }
}
