using System;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Server
{
    public interface IDatabase
    {
        IQueryable<Tag> Tags { set;  get; }
        IQueryable<User> Users { set; get; }
        IQueryable<Mail> PendingMail { set; get; }
        IQueryable<CaesFile> Files { set; get; }

    }
}
