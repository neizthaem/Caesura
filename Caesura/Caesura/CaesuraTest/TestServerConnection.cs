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
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura"));
            }
            Assert.AreEqual(iSocket.aSocket.stringToBytes("Caesura"), mockSocket.receive(15));
            mocks.VerifyAll();
        }

        [Test]
        public void MockSocketMultipleRecieveAsserts()
        {
            using (mocks.Record())
            {
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura")).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber));
            }
            Assert.AreEqual(iSocket.aSocket.stringToBytes("Caesura"), mockSocket.receive(15));
            Assert.AreEqual(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber), mockSocket.receive(15));
            mocks.VerifyAll();
        }


        [Test]
        public void ServerConnectionValidation()
        {
            using (mocks.Record())
            {
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura")).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber)).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber)).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("TestUser")).Repeat.Once().Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("TestPass")).Repeat.Once().Repeat.Once();

                mockServer.validate("TestUser", "TestPass");
                LastCall.Return(true);
            }
            Boolean temp = connection.validation();

            Assert.AreEqual("TestUser", connection.username);
            Assert.True(temp);
            mocks.VerifyAll();

        }

        [Test]
        public void ServerConnectionValidationBadClientName()
        {
            using (mocks.Record())
            {
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura" + "1")).Repeat.Once();


            }
            Boolean temp = connection.validation();

            Assert.False(temp);
            mocks.VerifyAll();

        }

        [Test]
        public void ServerConnectionValidationBadMajorNumber()
        {
            using (mocks.Record())
            {
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura")).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("1" + Server.Server.MajorNumber)).Repeat.Once();


            }
            Boolean temp = connection.validation();

            Assert.False(temp);
            mocks.VerifyAll();

        }

        [Test]
        public void ServerConnectionValidationBadMinorNumber()
        {
            using (mocks.Record())
            {
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura")).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber)).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("1" + Server.Server.MinorNumber)).Repeat.Once();

            }
            Boolean temp = connection.validation();

            Assert.False(temp);
            mocks.VerifyAll();

        }


        [Test]
        public void ServerConnectionValidationWithExcessData()
        {
            using (mocks.Record())
            {
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("Caesura" + "\0ex")).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber + "\0ex")).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber + "\0ex")).Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("TestUser" + "\0ex")).Repeat.Once().Repeat.Once();
                mockSocket.receive(15);
                LastCall.Return(iSocket.aSocket.stringToBytes("TestPass" + "\0ex")).Repeat.Once().Repeat.Once();

                mockServer.validate("TestUser", "TestPass");
                LastCall.Return(true);
            }
            Boolean temp = connection.validation();

            Assert.AreEqual("TestUser", connection.username);
            Assert.True(temp);
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
                mockSocket.send(iSocket.aSocket.stringToBytes("generic"));
                // Number of transfers (1)
                mockSocket.send(iSocket.aSocket.stringToBytes("1"));
                // Length of a transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("24"));
                // Transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("This here is a text file"));
            }

            connection.sendFile("generic.txt");

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
        public void ServerConnectionOnRecieveFileRequestAllowed()
        {
            using (mocks.Record())
            {
                mockServer.requestFile("TestUser", "generic.txt");
                LastCall.Return(true);

                // File Name
                mockSocket.send(iSocket.aSocket.stringToBytes("generic"));
                // Number of transfers (1)
                mockSocket.send(iSocket.aSocket.stringToBytes("1"));
                // Length of a transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("24"));
                // Transfer
                mockSocket.send(iSocket.aSocket.stringToBytes("This here is a text file"));
            }
            connection.username = "TestUser";
            connection.onRecieve("RequestFile generic.txt");

            mocks.VerifyAll();
        }


        [Test]
        public void ServerConnectionOnRecieveFileRequestDenied()
        {
            using (mocks.Record())
            {
                mockServer.requestFile("TestUser", "generic.txt");
                LastCall.Return(false);

                // File Name
                mockSocket.send(iSocket.aSocket.stringToBytes("Access Denied"));
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
                mockSocket.receive(30);
                LastCall.Return(iSocket.aSocket.stringToBytes("Quit ")).Repeat.Once();

                mockSocket.close();
            }
            connection.run();

            Assert.IsFalse(connection.running);

            mocks.VerifyAll();
        }


    }
}
