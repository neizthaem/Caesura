using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Server
{
    [Table(Name = "Tags")]
    public class Tag
    {
        [Column(IsPrimaryKey = true)]
        public String FilePath;
        [Column(IsPrimaryKey = true)]
        public String TagName;
    }
}
