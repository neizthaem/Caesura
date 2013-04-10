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
        Server.iSQL mockSQL = null;
        Server.Server server = null;

        [SetUp]
        public void TestServerSetUp()
        {
            mocks = new MockRepository();
            mockSocket = mocks.Stub<iSocket.iSocket>();
            mockSQL = mocks.Stub<Server.iSQL>();
            server = new Server.Server();

            server.socket = mockSocket;
            server.SQL = mockSQL;
        }

        [TearDown]
        public void TestServerTearDown()
        {
            mockSQL = null;
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

        private iSocket.iSocket ReturnSocketAndCallStop(iSocket.iSocket ret, Server.iServer ser)
        {
            ser.stop();
            return ret;
        }

        [Test]
        public void TestServerRunSingleConnection()
        {
            iSocket.iSocket acceptedSocket = mocks.Stub<iSocket.iSocket>();

            using (mocks.Record())
            {
                mockSocket.listen(Server.Server.defaultPort);
                LastCall.Return(ReturnSocketAndCallStop(acceptedSocket,server));

                acceptedSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura")).Repeat.Once();
                acceptedSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber)).Repeat.Once();
                acceptedSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber)).Repeat.Once();
                acceptedSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("TestUser")).Repeat.Once().Repeat.Once();
                acceptedSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("TestPass")).Repeat.Once().Repeat.Once();

                mockSQL.validate("TestUser", "TestPass");
                LastCall.Return(true);
            }

            server.run();
            Assert.IsTrue(server.connections.ContainsKey("TestUser"));
            Assert.IsFalse(server.running);

            mocks.VerifyAll();
        }


    }
}
