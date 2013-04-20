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
    }
}
