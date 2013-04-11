using System;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Caesura
{
    public interface IDatabase
    {

        public Table<User> Users;
        public Table<File> Files;
        public Table<Tag> Tags;
        public Table<Mail> PendingMail;

    }
}
