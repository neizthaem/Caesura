using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesuraTest
{
    [TestFixture]
    class TestASocket
    {
        iSocket.aSocket sock = null;
        byte[] caesura = null;

        [SetUp]
        public void TestASocketSetUp()
        {
            sock = new iSocket.aSocket();

            caesura = new byte[7];
            caesura[0] = (byte)'C';
            caesura[1] = (byte)'a';
            caesura[2] = (byte)'e';
            caesura[3] = (byte)'s';
            caesura[4] = (byte)'u';
            caesura[5] = (byte)'r';
            caesura[6] = (byte)'a';
        }

        [TearDown]
        public void TestASocketTearDown()
        {
            sock = null;
            caesura = null;
        }

        [Test]
        public void TestASocketStringToBytesWithLength()
        {
            byte[] bytes = new Byte[15];
            bytes[0] = (byte)'C';
            bytes[1] = (byte)'a';
            bytes[2] = (byte)'e';
            bytes[3] = (byte)'s';
            bytes[4] = (byte)'u';
            bytes[5] = (byte)'r';
            bytes[6] = (byte)'a';
            for (int i = 7; i < 15; i++)
            {
                bytes[i] = (byte)'\0';
            }
            Assert.AreEqual(bytes, iSocket.aSocket.stringToBytes("Caesura", 15));
        }

        [Test]
        public void TestASocketStringToBytesCaesura()
        {
            Assert.AreEqual(caesura, iSocket.aSocket.stringToBytes("Caesura"));
        }

        [Test]
        public void TestASocketCloseUnconnectedSocket()
        {
            (new iSocket.aSocket()).close();
        }

        [Test]
        public void TestASocketBytesToMessageManualByteArrayExcessData()
        {
            byte[] bytes = new Byte[512];
            bytes[0] = (byte)'C';
            bytes[1] = (byte)'a';
            bytes[2] = (byte)'e';
            bytes[3] = (byte)'s';
            bytes[4] = (byte)'u';
            bytes[5] = (byte)'r';
            bytes[6] = (byte)'a';
            bytes[7] = (byte)'\0';
            bytes[8] = (byte)'e';
            bytes[9] = (byte)'x';

            Assert.AreEqual("Caesura", iSocket.aSocket.bytesToMessage(bytes));
        }

        [Test]
        public void TestASocketStringToBytesManual()
        {
            byte[] bytes = new Byte[7];
            bytes[0] = (byte)'C';
            bytes[1] = (byte)'a';
            bytes[2] = (byte)'e';
            bytes[3] = (byte)'s';
            bytes[4] = (byte)'u';
            bytes[5] = (byte)'r';
            bytes[6] = (byte)'a';

            Assert.AreEqual(bytes, iSocket.aSocket.stringToBytes("Caesura"));

        }

        [Test]
        public void TestASocketBytesToStringManual()
        {
            byte[] bytes = new Byte[7];
            bytes[0] = (byte)'C';
            bytes[1] = (byte)'a';
            bytes[2] = (byte)'e';
            bytes[3] = (byte)'s';
            bytes[4] = (byte)'u';
            bytes[5] = (byte)'r';
            bytes[6] = (byte)'a';

            Assert.AreEqual("Caesura", iSocket.aSocket.bytesToString(bytes));
        }

        [Test]
        public void TestASocketBytesToMessageNull()
        {
            Assert.AreEqual("null", iSocket.aSocket.bytesToMessage(null));
        }

        [Test]
        public void TestASocketBytesToStringNull()
        {
            Assert.AreEqual("null", iSocket.aSocket.bytesToString(null));
        }

        [Test]
        public void TestASocketBytesToMessage()
        {
            Assert.AreEqual("Caesura", iSocket.aSocket.bytesToMessage(iSocket.aSocket.stringToBytes("Caesura")));
        }

        [Test]
        public void TestASocketBytesToMessageExcessDataOfNullTermination()
        {
            Assert.AreEqual("Caesura", iSocket.aSocket.bytesToMessage(iSocket.aSocket.stringToBytes("Caesura\0")));
        }

        [Test]
        public void TestASocketBytesToMessageExcessDataOfNullTerminationAndMore()
        {
            Assert.AreEqual("Caesura", iSocket.aSocket.bytesToMessage(iSocket.aSocket.stringToBytes("Caesura\0Caesuree")));
        }

        [Test]
        public void TestASocketRecieveException()
        {
            iSocket.aSocket sock = new iSocket.aSocket();
            Assert.IsNull(sock.receive(5));
        }

        [Test]
        public void TestASocketSendException()
        {
            iSocket.aSocket sock = new iSocket.aSocket();
            sock.send(null);
        }
    }
}
