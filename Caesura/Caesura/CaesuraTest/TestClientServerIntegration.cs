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

        Thread serverThread = null;

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
            server = null;
            client = null;
        }

        [Test]
        public void TestClientServerIntegrationCanLogin()
        {
            serverThread.Start();

            Assert.True(client.login("TestUser", "TestPass"));
        }

        [Test]
        public void TestClientServerIntegrationClientRequestFileGeneric()
        {
            serverThread.Start();

            if (File.Exists("generic"))
            {
                File.Delete("generic");
            }

            Assert.IsFalse(File.Exists("generic"));

            // Client will login
            client.login("TestUser", "TestPass");
            // Request the file
            client.requestFile("generic.txt");
            // Asert that the file exists
            Assert.IsTrue(File.Exists("generic"));
            // Assert that the contents are correct
            Assert.AreEqual(File.ReadAllText("generic.txt"), File.ReadAllText("generic"));
        }
    }
}
