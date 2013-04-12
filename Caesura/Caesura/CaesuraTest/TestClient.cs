using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesuraTest
{
    [TestFixture]
    public class TestClient
    {
        MockRepository mocks = null;
        iSocket.iSocket mockSocket = null;
        Client.iConnection mockConnection = null;
        Client.Client client = null;

        [SetUp]
        public void TestClientSetUp()
        {
            mocks = new MockRepository();
            mockSocket = mocks.Stub<iSocket.iSocket>();
            mockConnection = mocks.DynamicMock<Client.iConnection>();
            client = new Client.Client();
            client.connection = mockConnection;
        }

        [TearDown]
        public void TestClientTearDown()
        {
            mockConnection = null;
            mockSocket = null;
            client = null;
            mocks = null;
        }

        [Test]
        public void TestClientRequestFileRealConnectionSuccess()
        {
            Client.Connection temp = new Client.Connection(client);
            client.connection = temp;
            temp.sock = mockSocket;

            using (mocks.Record())
            {
                mockSocket.send(iSocket.aSocket.stringToBytes("RequestFile generic.txt"));

                // File Name
                mockSocket.receive(512);
                LastCall.Return(iSocket.aSocket.stringToBytes("generic.txt")).Repeat.Once();
                // Number of transfers (1)
                mockSocket.receive(512);
                LastCall.Return(iSocket.aSocket.stringToBytes("1")).Repeat.Once();
                // Length of a transfer
                mockSocket.receive(512);
                LastCall.Return(iSocket.aSocket.stringToBytes("18")).Repeat.Once();
                // Transfer
                mockSocket.receive(512);
                LastCall.Return(iSocket.aSocket.stringToBytes("This here is a text file")).Repeat.Once();
            }

            Assert.IsTrue(client.requestFile("generic.txt"));
            mocks.VerifyAll();
        }

        [Test]
        public void TestClientLoginRealConnectionSuccess()
        {
            Client.Connection temp = new Client.Connection(client);
            client.connection = temp;
            temp.sock = mockSocket;

            using (mocks.Record())
            {
                mockSocket.connect(Server.Server.host, Server.Server.defaultPort);
                // Caesura
                mockSocket.send(iSocket.aSocket.stringToBytes("Caesura"));
                // Major
                mockSocket.send(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber));
                // Minor
                mockSocket.send(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber));
                // Username
                mockSocket.send(iSocket.aSocket.stringToBytes("TestUser"));
                // Password
                mockSocket.send(iSocket.aSocket.stringToBytes("TestPass"));

                mockSocket.receive(5);
                LastCall.Return(iSocket.aSocket.stringToBytes("true"));
            }

            Assert.True(client.login("TestUser", "TestPass"));
            mocks.VerifyAll();
        }

    }
}
