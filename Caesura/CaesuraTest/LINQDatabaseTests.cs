using System;
using Server;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CaesuraTest
{
    class LINQDatabaseTests
    {

        [Test()]
        public void TestFileDoesntExist()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile f = new CaesFile();
            f.Path = "exists.txt";
            f.Name = "I exist";

            Assert.AreEqual(false, Search.database.FileExists(f));

        }

        [Test()]
        public void TestFileExists()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile f = new CaesFile();
            f.Path = "exists.txt";
            f.Name = "I exist";
            Search.database.AddFile(f);

            Assert.AreEqual(true, Search.database.FileExists(f));

        }

        [Test()]
        public void TestTagDoesntExist()
        {
            Search.database = ObjectMother.EmptyDatabase();

            TagNames tag = new TagNames();
            tag.TagName = "audio";

            Assert.AreEqual(false, Search.database.TagExists(tag));

        }

        [Test()]
        public void TestTagExists()
        {
            Search.database = ObjectMother.EmptyDatabase();

            TagNames tag = new TagNames();
            tag.TagName = "audio";

            Search.database.AddTag(tag);

            Assert.AreEqual(true, Search.database.TagExists(tag));

        }

        [Test()]
        public void TestRegisterAndGetUser()
        {
            Search.database = ObjectMother.EmptyDatabase();

            User user = new User();
            user.Username = "山田";
            user.PasswordHash = "saikyou";
            Search.database.registerUser(user);

            var actual = Search.database.getUser("山田");

            Assert.AreEqual(user, actual);
            Assert.AreEqual(user.Username, actual.Username);
            Assert.AreEqual(user.PasswordHash, actual.PasswordHash);

        }

        [Test()]
        public void TestAddFileCaeFile()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile file = new CaesFile();
            file.Path = "test1File.txt";
            file.Name = "Add File Test";

            Search.database.AddFile(file);

            var fileActual = (from f in Search.database.Files
                              select f).First();

            Assert.AreEqual(file.Path, fileActual.Path);
            Assert.AreEqual(file.Name, fileActual.Name);
        }

        [Test()]
        public void TestAddFileCaeFileOther()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile file = new CaesFile();
            file.Path = "test1File.txt";
            file.Name = "Add File Test";

            Search.database.AddFile("test1File.txt", "Add File Test");

            var fileActual = (from f in Search.database.Files
                              select f).First();

            Assert.AreEqual(file.Path, fileActual.Path);
            Assert.AreEqual(file.Name, fileActual.Name);
        }

        [Test()]
        public void TestAddTagName()
        {
            Search.database = ObjectMother.EmptyDatabase();

            TagNames tag = new TagNames();
            tag.TagName = "Test Tag";

            Search.database.AddTag(tag);

            var tagActual = (from f in Search.database.TagNames
                              select f).First();

            Assert.AreEqual(tag.TagName, tagActual.TagName);
        }

        [Test()]
        public void TestAddTagNameOther()
        {
            Search.database = ObjectMother.EmptyDatabase();

            TagNames tag = new TagNames();
            tag.TagName = "Test Tag";

            Search.database.AddTag("Test Tag");

            var tagActual = (from f in Search.database.TagNames
                             select f).First();

            Assert.AreEqual(tag.TagName, tagActual.TagName);
        }

        [Test()]
        public void TestGetListOfAllTags()
        {
            Search.database = ObjectMother.EmptyDatabase();

            var expected = new List<string>();
            expected.Add("audio");
            expected.Add("video");
            expected.Add("text");

            foreach (String s in expected)
            {
                TagNames tag = new TagNames();
                tag.TagName = s;
                Search.database.AddTag(tag);
            }

            var actual = Search.database.getListOfAllTags();
            expected.Sort();
            actual.Sort();

            Assert.AreEqual(expected, actual);

        }

        // Test Depends on other tests (above) to be working correctly
        [Test()]
        public void TestAddTagEntryForFile()
        {
            Search.database = ObjectMother.EmptyDatabase();

            Search.database.AddFile("test.txt", "Test File");
            Search.database.AddTag("text");

            Search.database.AddTagForFile("test.txt", "text");

            var actual = (from f in Search.database.Tags
                             select f).First();

            Assert.AreEqual("test.txt", actual.FilePath);
            Assert.AreEqual("text", actual.TagName);
        }

        // Test Depends on other tests (above) to be working correctly
        [Test()]
        public void TestAddTagEntryForFileOther()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile file = new CaesFile();
            file.Path = "test.txt";
            file.Name = "Test File";

            Search.database.AddFile(file);
            Search.database.AddTag("text");

            Search.database.AddTagForFile(file, "text");

            var actual = (from f in Search.database.Tags
                          select f).First();

            Assert.AreEqual("test.txt", actual.FilePath);
            Assert.AreEqual("text", actual.TagName);
        }

        [Test()]
        public void TestSendReceiveMailMessage()
        {
            Search.database = ObjectMother.EmptyDatabase();

            var from = new User();
            from.Username = "fromUser";
            from.PasswordHash = "fromUserpwd";

            var to = new User();
            to.Username = "toUser";
            to.PasswordHash = "toUserpwd";

            Search.database.registerUser(from);
            Search.database.registerUser(to);

            String message = "hello toUser";

            Search.database.sendMail(to, from, message);

            var rec = Search.database.checkMail(to).First();

            Assert.AreEqual(message, rec.Message);
            Assert.AreEqual(to.Username, rec.To);
            Assert.AreEqual(from.Username, rec.From);

        }

        [Test()]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Invalid Recipient: toUser")]
        public void TestInvalidToUser()
        {
            Search.database = ObjectMother.EmptyDatabase();

            var from = new User();
            from.Username = "fromUser";
            from.PasswordHash = "fromUserpwd";

            var to = new User();
            to.Username = "toUser";
            to.PasswordHash = "toUserpwd";

            Search.database.registerUser(from);

            String message = "hello toUser";

            Search.database.sendMail(to, from, message);
        }

        [Test()]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Invalid Sender: fromUser")]
        public void TestInvalidFromUser()
        {
            Search.database = ObjectMother.EmptyDatabase();

            var from = new User();
            from.Username = "fromUser";
            from.PasswordHash = "fromUserpwd";

            var to = new User();
            to.Username = "toUser";
            to.PasswordHash = "toUserpwd";

            Search.database.registerUser(to);

            String message = "hello toUser";

            Search.database.sendMail(to, from, message);
        }

        [Test()]
        public void TestMultipleMessagesWithSameToFrom()
        {
            Search.database = ObjectMother.EmptyDatabase();

            var from = new User();
            from.Username = "fromUser";
            from.PasswordHash = "fromUserpwd";

            var to = new User();
            to.Username = "toUser";
            to.PasswordHash = "toUserpwd";

            Search.database.registerUser(from);
            Search.database.registerUser(to);

            String message = "hello toUser";
            String message2 = "hello toUser second";

            Search.database.sendMail(to, from, message);
            Search.database.sendMail(to, from, message2);

            var rec = Search.database.checkMail(to.Username);

            Mail m1 = new Mail();
            m1.To = to.Username;
            m1.From = from.Username;
            m1.Message = message;

            Mail m2 = new Mail();
            m2.To = to.Username;
            m2.From = from.Username;
            m2.Message = message2;

            var expected = new List<Mail>();
            expected.Add(m1);
            expected.Add(m2);

            Assert.AreEqual(expected.First().Message, rec.First().Message);
            expected.Remove(expected.First());
            rec.Remove(rec.First());

            Assert.AreEqual(expected.First().Message, rec.First().Message);

        }



    }
}
