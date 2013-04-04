using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using System.Net.Sockets;
using System.Net;

namespace iSocket
{
    [TestFixture]
    class testaSocket
    {
        aSocket defaultSocket;
        MockRepository mock;
        Socket mockSocket;
        aSocket customSocket;

        [SetUp]
        public void testaSocketSetUp()
        {
            defaultSocket = new aSocket();

            mock = new MockRepository();
            mockSocket = mock.Stub<Socket>(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            customSocket = new aSocket(mockSocket);
        }

        [TearDown]
        public void testaSocketTearDown()
        {
            defaultSocket = null;

            customSocket = null;

            mockSocket = null;
            mock = null;
        }

        private void returnVoid()
        {
        }

        [Test]
        public void testaSocketConstructorNotNull()
        {
            Assert.NotNull(defaultSocket);
        }

        [Test]
        public void testaSocketConstructorSocketNotNull()
        {
            Assert.NotNull(defaultSocket.socket);
        }

        [Test]
        public void testaSocketConstructorGivenAMockSocket()
        {
            Assert.NotNull(customSocket);
        }

        [Test]
        public void testaSocketconnectGivenNonNullHostAndPort()
        {
            using (mock.Record())
            {
                mockSocket.Connect("Host", 1337);
                LastCall.Return(null);
            }

            Assert.AreEqual(1,customSocket.connect("Host", 1337));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketconnectGivenNullHost()
        {
            Assert.AreEqual(-1,customSocket.connect(null,1337));
        }

        [Test]
        public void testaSocketconnectGivenShortHostLength()
        {
            Assert.AreEqual(-3, customSocket.connect("", 1337));
        }

        [Test]
        public void testaSocketlisten()
        {
            Socket returnSocket = mock.Stub<Socket>(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            using (mock.Record())
            {
                mockSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1337));
                LastCall.Return(null);

                mockSocket.Accept();
                LastCall.Return(returnSocket);
            }

            Assert.AreSame(returnSocket, customSocket.listen(1337));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketClose()
        {
            using (mock.Record())
            {
                mockSocket.Close();
            }

            customSocket.close();

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketrecieveWithNoExceptions()
        {
            byte[] buffer = new byte[4];
            using (mock.Record())
            {
                mockSocket.Receive(buffer, 4, 0);
                LastCall.Return(1);
            }

            Assert.AreEqual(1,customSocket.receive(buffer));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketrecieveWithNullBuffer()
        {
            Assert.AreEqual(-2,customSocket.receive(null));
        }

        [Test]
        public void testaSocketreieveWithSocketException()
        {
            byte[] buffer = new byte[4];

            using (mock.Record())
            {
                mockSocket.Receive(buffer, 4, 0);
                LastCall.Throw(new SocketException());
            }

            Assert.AreEqual(-1, customSocket.receive(buffer));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketreieveWithObjectDisposedException()
        {
            byte[] buffer = new byte[4];

            using (mock.Record())
            {
                mockSocket.Receive(buffer, 4, 0);
                LastCall.Throw(new ObjectDisposedException("mockSocket"));
            }

            Assert.AreEqual(-4, customSocket.receive(buffer));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketsendWithNoExceptions()
        {
            byte[] buffer = new byte[4];
            using (mock.Record())
            {
                mockSocket.Send(buffer, 4, 0);
                LastCall.Return(1);
            }

            Assert.AreEqual(1, customSocket.send(buffer));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketsendWithNullBuffer()
        {
            Assert.AreEqual(-2, customSocket.send(null));
        }

        [Test]
        public void testaSocketsendWithSocketException()
        {
            byte[] buffer = new byte[4];

            using (mock.Record())
            {
                mockSocket.Send(buffer, 4, 0);
                LastCall.Throw(new SocketException());
            }

            Assert.AreEqual(-1, customSocket.send(buffer));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketsendWithObjectDisposedException()
        {
            byte[] buffer = new byte[4];

            using (mock.Record())
            {
                mockSocket.Send(buffer, 4, 0);
                LastCall.Throw(new ObjectDisposedException("mockSocket"));
            }

            Assert.AreEqual(-4, customSocket.send(buffer));

            mock.VerifyAll();
        }

        [Test]
        public void testaSocketisConnected()
        {
            Assert.IsFalse(defaultSocket.isConnected());
        }

        [Test]
        public void testaSocketisConnectedNullSocket()
        {
            Assert.IsFalse((new aSocket(null)).isConnected());
        }
    }
}
