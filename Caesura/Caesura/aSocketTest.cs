using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Sockets;
using Rhino.Mocks;
namespace Caesura
{
    [TestFixture]
    class aSocketTest
    {
        aSocket sock;
        MockRepository mock;
        [SetUp]
        public void SetUp()
        {
            mock = new MockRepository();
            sock = new aSocket();
        }

        [TearDown]
        public void TearDown()
        {
            mock = null;
            sock = null;
        }

        [Test]
        public void testaSocketConstructor()
        {
            Assert.IsNotNull(sock);
        }

        [Test]
        public void testaSocketConstructorGivenAnSocket()
        {
            Socket tempSock = sock.socket;
            aSocket tempaSock = new aSocket(tempSock);
            Assert.AreSame(tempSock, tempaSock.socket);
        }

        [Test]
        public void testaSocketConnectNullHost()
        {
            int n = sock.connect(null, 23);
            Assert.AreEqual(-1, n);
        }

        // This test causes a compiler error

        //[Test]
        //public void testaSocketConnectNullPort()
        //{
        //    int p = sock.connect("127.0.0.1", null);
        //    Assert.AreEqual(-2, null);
        //}

        [Test]
        public void testaSocketConnectHostLengthZero()
        {
            int n = sock.connect("", 23);
            Assert.AreEqual(-3, n);
        }

        [Test]
        public void testaSocketListen()
        {
            Socket tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket mockSocket = mock.StrictMock<Socket>(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mockSocket.Stub(a => a.Accept()).Return(tempSocket);
            mockSocket.Stub(a => a.Listen(23));

            sock = new aSocket(mockSocket);

            Assert.AreSame(sock.listen(23), tempSocket);
        }

        // No test for close?

        [Test]
        public void testaSocketRecieveNullBuffer()
        {
            int n = sock.receive(null, 34, 0);

            Assert.AreEqual(n, -2);
        }

        [Test]
        public void testaSocketRecieveNegativeLengthBuffer()
        {
            int n = sock.receive(new Byte[20], -2, 0);
            Assert.AreEqual(n, -3);
        }

        [Test]
        public void testaSocketRecieveLengthLongerThanBuffer()
        {
            int n = sock.receive(new Byte[20], 22, 0);
            Assert.AreEqual(n, -3);
        }

        [Test]
        public void testaSocketRecieveSocketException()
        {
            Socket mockSocket = mock.StrictMock<Socket>(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mockSocket.Stub(a => a.Receive(new Byte[32],32,0)).Throw(new SocketException());

            sock = new aSocket(mockSocket);

            Assert.AreEqual(sock.receive(new Byte[32],32,0), -1);
        }

        [Test]
        public void testaSocketRecieveObjectDisposedException()
        {
            Socket mockSocket = mock.StrictMock<Socket>(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mockSocket.Stub(a => a.Receive(new Byte[32], 32, 0)).Throw(new ObjectDisposedException("Test Element"));

            sock = new aSocket(mockSocket);

            Assert.AreEqual(sock.receive(new Byte[32], 32, 0), -4);
        }
    }
}
