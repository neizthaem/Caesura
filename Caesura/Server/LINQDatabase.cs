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

        private const String LoginString = "Data Source=whale.cs.rose-hulman.edu;Initial Catalog=Caesura;User ID=stewarzt;Password=Chronotrigger20o";

            public LINQDatabase(): base(LoginString)
            {
            }

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

            /**
             * Add a file to the CMS database
             * */
            public static void addFile(CaesFile file)
            {
                //TODO: Write test cases and implement
            }

            public static void addFile(String FilePath, String Name)
            {
                //TODO: Write test cases and implement
            }

            /**
             * Add a tag to the CMS database
             * */
            public static void addTag(String TagName)
            {
                //TODO: Write test cases and implement
            }

            public static void addTag(TagNames tag)
            {
                //TODO: Write test cases and implement
            }

            /**
             * Add a tags to a file
             * */
            public static void addTagForFile(String FilePath, params String[] tags)
            {
                //TODO: Write test cases and implement
            }

            // Insert tags for a file
            public static void addTagForFile(CaesFile file, params String[] tags)
            {
                //TODO: Write test cases and implement
            }
    }
}
