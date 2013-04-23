using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Caesura
{
    [Table(Name = "Files")]
    public class CaesFile
    {
        [Column(IsPrimaryKey = true)]
        public string Path;
        [Column]
        public string Name;
    }
}
