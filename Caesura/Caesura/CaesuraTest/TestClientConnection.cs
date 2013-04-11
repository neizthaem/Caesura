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
    class TestClientConnection
    {
        MockRepository mocks = null;
        iSocket.iSocket mockSocket = null;
        Client.iClient mockClient = null;
        Client.Connection connection = null;

        [SetUp]
        public void TestClientConnectionSetUp()
        {
            mocks = new MockRepository();
            mockSocket = mocks.Stub<iSocket.iSocket>();
            mockClient = mocks.Stub<Client.iClient>();
            connection = new Client.Connection(mockClient);
            connection.sock = mockSocket;
        }

        [TearDown]
        public void TestClientConnectionTearDown()
        {
            mocks = null;
            mockSocket = null;
            mockClient = null;
            connection = null;
        }

        [Test]
        public void TestClientConnectionConstructorNotNull()
        {
            Assert.NotNull(connection);
        }

        [Test]
        public void TestClientConnectionLoginSuccess()
        {
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

            Assert.True(connection.login("TestUser", "TestPass"));
            mocks.VerifyAll();
        }

        [Test]
        public void TestClientRequestFileSuccess()
        {
            using (mocks.Record())
            {
                mockSocket.send(iSocket.aSocket.stringToBytes("RequestFile generic.txt"));

            }
        }
    }
}
