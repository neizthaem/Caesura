using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Caesura;

namespace Caesura
{
    class UserRegistration
    {

        private static DatabaseInterface Database;



        public static void setDatabase(DatabaseInterface toSet)
        {
            Database = toSet;
        }

        internal static bool register(User toRegister)
        {
            return Database.registerUser(toRegister);
        }

        internal static bool isRegistered(string name)
        {
            User checker = Database.getUser(name);
            return (checker != null);
        }

        internal static bool login(string name, string password)
        {
            if (isRegistered(name))
            {
                User checker = Database.getUser(name);
                return checker.getPass().Equals(password);
            }
            return false;
            
        }
    }
}