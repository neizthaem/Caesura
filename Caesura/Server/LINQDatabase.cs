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
    }
}
