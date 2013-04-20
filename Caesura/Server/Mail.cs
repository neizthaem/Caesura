using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Server
{
    [Table(Name = "PendingMail")]
    public class Mail
    {
        [Column(IsPrimaryKey = true)]
        public int ID;
        [Column]
        public string To;
        [Column]
        public string From;
        [Column]
        public string Message;
    }
}
