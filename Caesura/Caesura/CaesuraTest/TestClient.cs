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

        public void TestClientConstructor()
        {
            Assert.NotNull(client);
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
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("generic.txt", Server.Server.maxBytes)).Repeat.Once();
                // Number of transfers (1)
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("1", Server.Server.maxBytes)).Repeat.Once();
                // Length of a transfer
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("18", Server.Server.maxBytes)).Repeat.Once();
                // Transfer
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("This here is a text file", Server.Server.maxBytes)).Repeat.Once();
            }

            Assert.IsTrue(client.requestFile("generic.txt"));
            mocks.VerifyAll();
        }



    }
}
