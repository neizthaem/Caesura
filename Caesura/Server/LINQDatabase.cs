using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Server
{
    public class LINQDatabase : DataContext
    {
        public Table<User> Users;
        public Table<CaesFile> Files;
        public Table<Tag> Tags;
        public Table<Mail> PendingMail;
        public Table<TagNames> TagNames;

        private const String LoginString = "Data Source=whale.cs.rose-hulman.edu;Initial Catalog=Caesura;User ID=stewarzt;Password=Chronotrigger20o";
        private const String LoginStringTest = "Data Source=whale.cs.rose-hulman.edu;Initial Catalog=CaesuraTest;User ID=stewarzt;Password=Chronotrigger20o";

            // This should be used for connections to the real database
            public LINQDatabase(): base(LoginString) { }

            // This is a dummy constructor to access the testing database
            public LINQDatabase(Boolean testDatabase) : base(LoginStringTest) { }


            /*
             * USER METHODS
             */
            public User getUser(string username)
            {
                return (from t in this.Users
                        where t.Username == username
                        select t).First();
            }

            public bool registerUser(User toRegister)
            {
                this.Users.InsertOnSubmit(toRegister);
                this.SubmitChanges();
                return UserExists(toRegister);
            }

            public bool UserExists(User user)
            {
                return ((from t in this.Users
                         where t == user
                         select t).Count() > 0);
            }

            /*
             * FILE METHODS
             */

            public bool FileExists(String path)
            {
                return ((from t in this.Files
                         where t.Path == path
                         select t).Count() > 0);
            }

            public bool FileExists(CaesFile file)
            {
                return FileExists(file.Path);
            }

            public void AddFile(CaesFile file)
            {
                this.Files.InsertOnSubmit(file);
                this.SubmitChanges();
            }

            public void AddFile(String FilePath, String Name)
            {
                CaesFile file = new CaesFile();
                file.Path = FilePath;
                file.Name = Name;
                AddFile(file);
            }

            /*
             * TAG METHODS
             */
            public void AddTag(String TagName)
            {
                TagNames tag = new TagNames();
                tag.TagName = TagName;
                AddTag(tag);
            }

            public void AddTag(TagNames tag)
            {
                this.TagNames.InsertOnSubmit(tag);
                this.SubmitChanges();
            }

            public List<String> getListOfAllTags()
            {
                return this.TagNames.Select(p => p.TagName).ToList<String>();
            }

            /*
             * FILE TAGGING METHODS
             */
            public void AddTagForFile(String FilePath, params String[] tags)
            {
                foreach (String tag in tags)
                {
                    Tag t = new Tag();
                    t.FilePath = FilePath;
                    t.TagName = tag;
                    this.Tags.InsertOnSubmit(t);
                }

                this.SubmitChanges();
            }

            public void AddTagForFile(CaesFile file, params String[] tags)
            {
                AddTagForFile(file.Path, tags);
            }


            /*
            * MAIL SENDING/RECEIVING METHODS
            */
            public void sendMail(User to, User from, String message)
            {
                if (!UserExists(to))
                    throw new Exception("Invalid Recipient: " + to.Username);
                if (!UserExists(from))
                    throw new Exception("Invalid Sender: " + from.Username);

                var mail = new Mail();
                mail.To = to.Username;
                mail.From = from.Username;
                mail.Message = message;

                this.PendingMail.InsertOnSubmit(mail);
                this.SubmitChanges();
            }

            public List<Mail> checkMail(String recipient)
            {
                var messages = (from m in this.PendingMail
                                where m.To == recipient
                                select m);

                return messages.ToList<Mail>();
            }

            public List<Mail> checkMail(User recipient)
            {
                return checkMail(recipient.Username);
            }

    }
}
