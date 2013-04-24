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
    class TestServer
    {

        MockRepository mocks = null;
        iSocket.iSocket mockSocket = null;
        Server.Server server = null;

        [SetUp]
        public void TestServerSetUp()
        {
            mocks = new MockRepository();
            mockSocket = mocks.DynamicMock<iSocket.iSocket>();
            server = new Server.Server();

            server.socket = mockSocket;
        }

        [TearDown]
        public void TestServerTearDown()
        {
            mockSocket = null;
            mocks = null;
            server = null;
        }

        [Test]
        public void TestServerConstructor()
        {
            Assert.NotNull(server);
        }

        [Test]
        public void TestServerStop()
        {
            server.stop();
            Assert.IsFalse(server.running);
        }




        [Test]
        public void TestServerSingleConnectionFileTransfer()
        {
            iSocket.iSocket acceptedSocket = mocks.DynamicMock<iSocket.iSocket>();

            using (mocks.Record())
            {
                mockSocket.listen(Server.Server.defaultPort);
                LastCall.Return(acceptedSocket);

                acceptedSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("RequestFile generic.txt", Server.Server.maxBytes)).Repeat.Once();

                                // File Name
                acceptedSocket.send(iSocket.aSocket.stringToBytes("generic", Server.Server.maxBytes));
                // Number of transfers (1)
                acceptedSocket.send(iSocket.aSocket.stringToBytes("1", Server.Server.maxBytes));
                // Length of a transfer
                acceptedSocket.send(iSocket.aSocket.stringToBytes("24", Server.Server.maxBytes));
                // Transfer
                acceptedSocket.send(iSocket.aSocket.stringToBytes("This here is a text file", 24));

                acceptedSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("Quit ", Server.Server.maxBytes)).Repeat.Once();
            }

            server.run();

            mocks.VerifyAll();
        }



    }
}
