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
            server = null;
            client = null;
            mocks = null;
        }

        [Test]
        public void TestMockVeryifyAllDynamicMock()
        {
            iSocket.iSocket mockSocket = mocks.DynamicMock<iSocket.iSocket>();

            using (mocks.Record())
            {
                mockSocket.send(null);
            }

            mocks.VerifyAll();
        }

        [Test]
        public void TestMockVeryifyAllStub()
        {
            iSocket.iSocket mockSocket = mocks.Stub<iSocket.iSocket>();

            using (mocks.Record())
            {
                mockSocket.send(null);
            }

            mocks.VerifyAll();
        }

        [Test]
        public void TestClientServerIntegrationCanLogin()
        {
            serverThread.Start();
            // Sleep to let the server start up
            System.Threading.Thread.Sleep(5000);

            Assert.True(client.login("TestUser", "aPass"));
        }

        [Test]
        public void TestClientServerIntegrationClientRequestFileGeneric()
        {
            serverThread.Start();

            // Sleep to let the server start up
            System.Threading.Thread.Sleep(5000);

            if (File.Exists("generic"))
            {
                File.Delete("generic");
            }

            Assert.IsFalse(File.Exists("generic"));

            // Client will login
            Assert.True(client.login("aUser", "aPass"));
            // Request the file
            client.requestFile("generic.txt");
            // Asert that the file exists
            Assert.IsTrue(File.Exists("generic"));
            // Assert that the contents are correct
            Assert.AreEqual(File.ReadAllText("generic.txt"), File.ReadAllText("generic"));
        }
    }
}
