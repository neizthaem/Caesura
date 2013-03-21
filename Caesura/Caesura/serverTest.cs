using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Rhino.Mocks;

namespace Caesura
{
    [TestFixture]
    class serverTest
    {

        MockRepository mock;
        iSocket mockiSocket;
        server s;

        [SetUp]
        public void SetUp()
        {
            mock = new MockRepository();
            mockiSocket = mock.StrictMock<iSocket>();
            s = new server();
        }

        [TearDown]
        public void TearDown()
        {
            mock = null;
            mockiSocket = null;
            s = null;
        }

        [Test]
        public void testServerNoParameterConstructor()
        {
            Assert.IsNotNull(s);
        }

        [Test]
        public void testServerSetSocket()
        {
            s.setSocket(mockiSocket);
            Assert.AreSame(mockiSocket, s.socket);
        }

        [Test]
        public void testServerListen()
        {
            s.setSocket(mockiSocket);
            iSocket tempMockiSocket = mock.StrictMock<iSocket>();

            mockiSocket.Stub(a => a.accept()).Return(tempMockiSocket);

            s.listen();

            Assert.AreSame(s.socket, tempMockiSocket);
        }
    }
}
