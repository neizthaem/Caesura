using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    // Used to designate that when you requested a file you were not allowed to dl it
    public class InvalidFilePermissions : System.Exception
    {

        public InvalidFilePermissions(string message)
            : base(message)
        {
        }
    }
}
