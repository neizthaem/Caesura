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

        [Test]
        public void testServerNoParameterConstructor()
        {
            
            Assert.IsNotNull(s);
        }

        // This Test should fail.
        [Test]
        public void testAssertAreSameWithMockedElements()
        {
            Assert.AreSame(mockiSocket, 3);
        }

        [Test]
        public void testServerSetSocket()
        {
            s.setSocket(mockiSocket);
            Assert.AreSame(mockiSocket, s.socket);
        }
    }
}
