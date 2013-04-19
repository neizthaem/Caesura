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
    class TestServerConnection
    {
        MockRepository mocks = null;
        iSocket.iSocket mockSocket = null;
        Server.iServer mockServer = null;
        Server.Connection connection = null;

        [SetUp]
        public void ServerConnectionSetUp()
        {
            mocks = new MockRepository();
            mockSocket = mocks.DynamicMock<iSocket.iSocket>();
            mockServer = mocks.DynamicMock<Server.iServer>();
            connection = new Server.Connection(mockSocket, mockServer);

        }

        [TearDown]
        public void ServerConnectionTearDown()
        {
            connection = null;
            mockSocket = null;
            mockServer = null;
            mocks = null;
        }

        [Test]
        public void ServerConnectionConstructor()
        {
            Assert.NotNull(connection);
        }

        [Test]
        public void MockSocketRecieve()
        {
            using (mocks.Record())
            {
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura", Server.Server.maxBytes));
            }
            Assert.AreEqual(iSocket.aSocket.stringToBytes("Caesura", Server.Server.maxBytes), mockSocket.receive(Server.Server.maxBytes));
            mocks.VerifyAll();
        }

        [Test]
        public void MockSocketMultipleRecieveAsserts()
        {
            using (mocks.Record())
            {
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura", Server.Server.maxBytes)).Repeat.Once();
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber, Server.Server.maxBytes));
            }
            Assert.AreEqual(iSocket.aSocket.stringToBytes("Caesura", Server.Server.maxBytes), mockSocket.receive(Server.Server.maxBytes));
            Assert.AreEqual(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber, Server.Server.maxBytes), mockSocket.receive(Server.Server.maxBytes));
            mocks.VerifyAll();
        }


  




        [Test]
        public void ServerConnectionReadFileEntireFile()
        {
            byte[] result = iSocket.aSocket.stringToBytes("This here is a text file");
            Assert.AreEqual(iSocket.aSocket.bytesToString(result), iSocket.aSocket.bytesToString(connection.readFile("generic.txt", 0, 0)));
        }

        [Test]
        public void ServerConnectionReadFileBeginingFour()
        {
            byte[] result = iSocket.aSocket.stringToBytes("This");
            Assert.AreEqual(iSocket.aSocket.bytesToString(result), iSocket.aSocket.bytesToString(connection.readFile("generic.txt", 0, 4)));
        }

        [Test]
        public void ServerConnectionReadFileSubsection()
        {
            byte[] result = iSocket.aSocket.stringToBytes("here");
            Assert.AreEqual(iSocket.aSocket.bytesToString(result), iSocket.aSocket.bytesToString(connection.readFile("generic.txt", 5, 4)));
        }

        [Test]
        public void ServerConnectionSendFile()
        {
            using (mocks.Record())
            {
                // File Name
                mockSocket.send(iSocket.aSocket.stringToBytes("generic", Server.Server.maxBytes));
                // Number of transfers (1)
                mockSocket.send(iSocket.aSocket.stringToBytes("1", Server.Server.maxBytes));
                // Length of a transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("24", Server.Server.maxBytes));
                // Transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("This here is a text file"));
            }

            connection.sendFile("generic.txt");

            mocks.VerifyAll();
        }

        [Test]
        public void ServerConnectionSendFile513()
        {
            using (mocks.Record())
            {
                // File Name
                mockSocket.send(iSocket.aSocket.stringToBytes("513", Server.Server.maxBytes));
                // Number of transfers (1)
                mockSocket.send(iSocket.aSocket.stringToBytes("2", Server.Server.maxBytes));
                // Length of a transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("512", Server.Server.maxBytes));
                // Transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111"));
                // Length of a transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("1", Server.Server.maxBytes));
                // Transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("2"));
            
            }

            connection.sendFile("513.txt");

            mocks.VerifyAll();
        }

        [Test]
        public void TestServerConnectionSplitMessage()
        {
            string[] target = connection.splitMessage("RequestFile generic.txt");
            Assert.AreEqual("RequestFile", target[0]);
            Assert.AreEqual("generic.txt", target[1]);
        }


        [Test]
        public void TestServerConnectionSplitMessageNoSpace()
        {
            string[] target = connection.splitMessage("NoSpace");
            Assert.AreEqual("NoSpace", target[0]);
            Assert.AreEqual("",target[1]);
        }
        [Test]
        public void ServerConnectionOnRecieveFileRequestAllowed()
        {
            using (mocks.Record())
            {

                // File Name
                mockSocket.send(iSocket.aSocket.stringToBytes("generic", Server.Server.maxBytes));
                // Number of transfers (1)
                mockSocket.send(iSocket.aSocket.stringToBytes("1", Server.Server.maxBytes));
                // Length of a transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("24", Server.Server.maxBytes));
                // Transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("This here is a text file"));
            }
            connection.username = "TestUser";
            connection.onRecieve("RequestFile generic.txt");

            mocks.VerifyAll();
        }

        [Test]
        public void ServerConnectionOnRecieveQuit()
        {
            using (mocks.Record())
            {
                mockSocket.close();
            }

            connection.onRecieve("Quit ");

            mocks.VerifyAll();
        }


        [Test]
        public void ServerConnectionRunRecieveQuit()
        {
            using (mocks.Record())
            {
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("Quit ", Server.Server.maxBytes)).Repeat.Once();

                mockSocket.close();
            }
            connection.run();

            Assert.IsFalse(connection.running);

            mocks.VerifyAll();
        }


    }
}
