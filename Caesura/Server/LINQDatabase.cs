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
                return ((from t in this.Users
                         where t == toRegister
                         select t).Count() > 0);
            }

            public List<String> getListOfAllTags()
            {
                return this.TagNames.Select(p => p.TagName).ToList<String>();
            }

            /**
             * Add a file to the CMS database
             * */
            public void addFile(CaesFile file)
            {
                this.Files.InsertOnSubmit(file);
                this.SubmitChanges();
            }

            public void addFile(String FilePath, String Name)
            {
                CaesFile file = new CaesFile();
                file.Path = FilePath;
                file.Name = Name;
                addFile(file);
            }

            /**
             * Add a tag to the CMS database
             * */
            public void addTag(String TagName)
            {
                TagNames tag = new TagNames();
                tag.TagName = TagName;
                addTag(tag);
            }

            public void addTag(TagNames tag)
            {
                this.TagNames.InsertOnSubmit(tag);
                this.SubmitChanges();
            }

            /**
             * Add a tags to a file
             * NOTE: the file and TagName must already exist in the database
             * */
            public void addTagForFile(String FilePath, params String[] tags)
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

            public void addTagForFile(CaesFile file, params String[] tags)
            {
                addTagForFile(file.Path, tags);
            }

    }
}
