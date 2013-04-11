using System;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Caesura
{
    public class IDatabase : DataContext
    {

        public Table<User> Users;
        public Table<File> Files;
        public Table<Tag> Tags;
        public Table<Mail> PendingMail;

        private const String LoginString = "login_string";

        public IDatabase() : base(LoginString)
        {
        }

    }
}
