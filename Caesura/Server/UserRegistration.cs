using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Server;
using Rhino.Mocks;

namespace Server
{
    public class UserRegistration
    {

        public static LINQDatabase database;


        public static void setDatabase(LINQDatabase toSet)
        {
            database = toSet;
        }

        public static bool register(User toRegister)
        {
            return database.registerUser(toRegister);
        }

        public static bool isRegistered(string name)
        {
            User checker = database.getUser(name);
            return (checker != null);
        }

        public static bool login(string name, string password)
        {
            if (isRegistered(name))
            {
                User checker = database.getUser(name);
                return checker.PasswordHash.Trim().Equals(password);
            }
            return false;

        }
    }
}