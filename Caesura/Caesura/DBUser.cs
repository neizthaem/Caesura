using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Caesura
{
    [Table(Name = "Users")]
    public class User
    {
        [Column(IsPrimaryKey = true)]
        public string Username;
        [Column]
        public string PasswordHash;
    }

}