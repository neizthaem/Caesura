using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesuraTest
{
    [TestFixture]
    class ClientSQLTests
    {
        Client.Client client;

        [SetUp]
        public void sqlSetUp()
        {
            client = new Client.Client();
            client.connect();
        }

        [TearDown]
        public void sqlTearDown()
        {
            client.disconnect();
        }

        [Test]
        public void sqlLogin()
        {
            Assert.IsTrue(client.login("Testuser", "Test"));
        }

        [Test]
        public void sqlgetFilesWithTags()
        {
            sqlLogin();

            // testpic.jpg
            // PictureText.txt
            var temp = client.getOwned();
            List<String> tempAnswer = new List<String>();

            tempAnswer.Add("testpic.jpg");
            tempAnswer.Add("PictureText.txt");

            foreach (String s in tempAnswer)
            {
                // I'm not too sure if order matters so I'm just going to assert that all of the stuff is in it
                Assert.IsTrue(temp.Contains(s));
            }
        }

        [Test]
        public void sqlregister()
        {
            Assert.True(client.register("Newuser", "New"));
        }

        [Test]
        public void sqlrequestfile()
        {
            sqlLogin();

            Assert.True(client.requestFile("testpic.jpg", "C:\\testpic.jpg"));

            Assert.True(System.IO.File.Exists("C:\\testpic.jpg"));
            System.IO.File.Delete("C:\\testpic.jpg");
        }

        [Test]
        public void sqlsearchtag()
        {
            sqlLogin();
            // sci-fi
            List<String> tags = new List<string>();
            tags.Add("sci-fi");

            var result = client.getFilesWithTag(tags);

            //Scifi-Text.txt

            Assert.True(result.Contains("Scifi-Text.txt"));
        }

        [Test]
        public void sqlalltags()
        {
            sqlLogin();

            // text
            // picture
            // sci-fi

            List<String> tags = new List<String>();
            tags.Add("sci-fi");
            tags.Add("text");
            tags.Add("picture");

            var result = client.getListOfAllTags();

            foreach (String s in tags)
            {
                Assert.IsTrue(result.Contains(s));
            }
        }

        [Test]
        public void sqlsearchownedfiles()
        {
            sqlLogin();

            // picture

            List<String> tags = new List<String>();
            tags.Add("testpic.jpg");
            tags.Add("PictureText.txt");

            String[] temp = new String[1];
            temp[0] = "picture";

            var result = client.getOwnFilesWithTags("Testuser", temp);

            foreach (String s in tags)
            {
                Assert.IsTrue(result.Contains(s));
            }
        }

        [Test]
        public void sqladdfile()
        {
            // use sql management to confirm that the file was added

            sqlLogin();

            Assert.True(client.AddOwnership("Testuser", "testpic.jpg"));
        }


    }
}
