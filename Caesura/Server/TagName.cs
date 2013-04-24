using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Server
{
    [Table(Name = "TagNames")]
    public class TagNames
    {
        [Column(IsPrimaryKey = true)]
        public String TagName;
    }
}
