using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Server;

namespace CaesuraTest
{
    [TestFixture()]
    class RemoteServerTests
    {
        [Test()]
        public void testGetFileFromSQLAndRemote()
        {
            Client.Client client = new Client.Client();
            List<string> input = new List<string>();
            input.Add("text");
            List<String> files = client.getFromTag(input);
            client.connect();
            client.login("Testuser", "Test");
            //client.requestFile(files.First());
            client.disconnect();
            Assert.IsTrue(System.IO.File.Exists("C:\\Caesura\\" + files.First()));
        }

        // To be exact it throws a FileNotFoundException
        [Test()]
        [ExpectedException]
        public void testGetBadFileFromRemote()
        {
            Client.Client client = new Client.Client();

            client.connect();
            client.login("Testuser", "Test");
            try
            {
                //client.requestFile("failFile.txt");
            }
            finally
            {
                client.disconnect();
            }
        }


        [Test()]
        public void testNoFileFromSQL()
        {
            Client.Client client = new Client.Client();
            List<string> input = new List<string>();
            input.Add("nonTag");
            List<String> files = client.getFromTag(input);
            client.connect();
            client.login("Testuser", "Test");

            Assert.IsTrue((files.Count < 1));


            client.disconnect();
        }





    }
}
