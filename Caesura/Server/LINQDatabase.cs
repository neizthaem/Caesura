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

        #region Database Tables
        public Table<User> Users;
        public Table<CaesFile> Files;
        public Table<Tag> Tags;
        public Table<Mail> PendingMail;
        public Table<TagNames> TagNames;
        public Table<Owner> Owns;
        #endregion

        private const String LoginString = "Data Source=whale.cs.rose-hulman.edu;Initial Catalog=Caesura;User ID=stewarzt;Password=Chronotrigger20o";
        private const String LoginStringTest = "Data Source=whale.cs.rose-hulman.edu;Initial Catalog=CaesuraTest;User ID=stewarzt;Password=Chronotrigger20o";

        // This should be used for connections to the real database
        public LINQDatabase() : base(LoginString) { }

        // This is a dummy constructor to access the testing database
        public LINQDatabase(Boolean testDatabase) : base(LoginStringTest) { }

        #region User Functions
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
        #endregion

        #region File Functions
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
        #endregion

        #region Tag Functions
        public bool TagExists(String tag)
        {
            return ((from t in this.TagNames
                     where t.TagName == tag
                     select t).Count() > 0);
        }

        public bool TagExists(TagNames tag)
        {
            return TagExists(tag.TagName);
        }

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
        #endregion

        #region Tagging Functions
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
        #endregion

        #region Mail Functions
        public void SendMail(User to, User from, String message)
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

        public List<Mail> CheckMail(String recipient)
        {
            var messages = (from m in this.PendingMail
                            where m.To == recipient
                            select m);

            return messages.ToList<Mail>();
        }

        public List<Mail> CheckMail(User recipient)
        {
            return CheckMail(recipient.Username);
        }
        #endregion

        #region File Ownership
        public List<String> GetListOfOwnedFiles(String username)
        {
            var owns = (from o in this.Owns
                        where o.Username == username
                        select o.FilePath);
            return owns.ToList<String>();
        }

        public void AddOwnership(String username, String filePath)
        {
            try
            {
                var owner = new Owner();
                owner.Username = username;
                owner.FilePath = filePath;
                this.Owns.InsertOnSubmit(owner);
                this.SubmitChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public List<String> GetListOfOwnedFiles(User user)
        {
            return GetListOfOwnedFiles(user.Username);
        }

        public bool CheckIfOwnsFile(String username, String filePath)
        {
            var owns = ((from o in this.Owns
                         where o.Username == username && o.FilePath == filePath
                         select o).Count() > 0);
            return owns;
        }
        #endregion

    }
}
