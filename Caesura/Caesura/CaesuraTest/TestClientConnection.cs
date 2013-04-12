using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.IO;
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
            mockSocket = mocks.DynamicMock<iSocket.iSocket>();
            mockClient = mocks.DynamicMock<Client.iClient>();
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
                LastCall.On(mockSocket).Repeat.Once();
                // Caesura
                mockSocket.send(iSocket.aSocket.stringToBytes("Caesura" + "\0", Server.Server.maxBytes));
                LastCall.On(mockSocket).Repeat.Once();
                // Major
                mockSocket.send(iSocket.aSocket.stringToBytes(Server.Server.MajorNumber, Server.Server.maxBytes));
                LastCall.On(mockSocket).Repeat.Once();
                // Minor
                mockSocket.send(iSocket.aSocket.stringToBytes(Server.Server.MinorNumber, Server.Server.maxBytes));
                LastCall.On(mockSocket).Repeat.Once();
                // Username
                mockSocket.send(iSocket.aSocket.stringToBytes("TestUser" + "\0", Server.Server.maxBytes));
                LastCall.On(mockSocket).Repeat.Once();
                // Password
                mockSocket.send(iSocket.aSocket.stringToBytes("TestPass" + "\0", Server.Server.maxBytes));
                LastCall.On(mockSocket).Repeat.Once();

                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("True", Server.Server.maxBytes));
            }

            Assert.True(connection.login("TestUser", "TestPass"));
            mocks.VerifyAll();
        }

        [Test]
        public void TestClientRequestFileSuccess()
        {
            using (mocks.Record())
            {
                mockSocket.send(iSocket.aSocket.stringToBytes("RequestFile TestClientRequestFileSuccess", Server.Server.maxBytes));
                LastCall.On(mockSocket).Repeat.Once();
                // File Name
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("TestClientRequestFileSuccess", Server.Server.maxBytes)).Repeat.Once();
                // Number of transfers (1)
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("1", Server.Server.maxBytes)).Repeat.Once();
                // Length of a transfer
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("24", Server.Server.maxBytes)).Repeat.Once();
                // Transfer
                mockSocket.receive(24);
                LastCall.Return(iSocket.aSocket.stringToBytes("This here is a text file")).Repeat.Once();
            }

            Assert.IsTrue(connection.requestFile("TestClientRequestFileSuccess"));

            //Assert.IsTrue(File.Exists("TestClientRequestFileSuccess"));
            //Assert.AreEqual(File.ReadAllText("TestClientRequestFileSuccess"), "This here is a text file");
            //File.Delete("TestClientRequestFileSuccess");

            //Assert.IsFalse(File.Exists("TestClientRequestFileSuccess"));
            mocks.VerifyAll();
        }

        [Test]
        public void TestClientRequestFileFailure()
        {
            using (mocks.Record())
            {
                mockSocket.send(iSocket.aSocket.stringToBytes("RequestFile TestClientRequestFileSuccess", Server.Server.maxBytes));
                LastCall.On(mockSocket).Repeat.Once();
                // File Name
                mockSocket.receive(Server.Server.maxBytes);
                LastCall.Return(iSocket.aSocket.stringToBytes("Access Denied", Server.Server.maxBytes)).Repeat.Once();
            }

            Assert.IsFalse(connection.requestFile("TestClientRequestFileSuccess"));

            //Assert.IsTrue(File.Exists("TestClientRequestFileSuccess"));
            //Assert.AreEqual(File.ReadAllText("TestClientRequestFileSuccess"), "This here is a text file");
            //File.Delete("TestClientRequestFileSuccess");

            //Assert.IsFalse(File.Exists("TestClientRequestFileSuccess"));
            mocks.VerifyAll();
        }

        [Test]
        public void TestClientWriteFileFileDoesNotExist()
        {
            String filename = "TestClientWriteFile";

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            Assert.IsFalse(File.Exists(filename));

            connection.writeFile(filename, iSocket.aSocket.stringToBytes("TestClientWriteFile"));

            //Assert.AreEqual(File.ReadAllText(filename), "TestClientWriteFile");

            //File.Delete(filename);
        }

        [Test]
        public void TestClientWriteFileFileDoesExist()
        {
            String filename = "TestClientWriteFile";

            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            Assert.IsTrue(File.Exists(filename));

            connection.writeFile(filename, iSocket.aSocket.stringToBytes("TestClientWriteFile"));

            Assert.AreEqual(File.ReadAllText(filename), "TestClientWriteFile");

            //File.Delete(filename);
        }

    }
}
