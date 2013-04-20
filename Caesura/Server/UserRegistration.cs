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

        private static LINQDatabase database;


        public static void mockStuff()
        {
            MockRepository mocks = new MockRepository();
            DatabaseInterface mockDatabase = mocks.Stub<DatabaseInterface>();

            User zarakavaUse = new User();
            zarakavaUse.setName("Testuser");
            zarakavaUse.setPass("Test");

            using (mocks.Record())
            {


                mockDatabase.getUser("Testuser");
                LastCall.Return(zarakavaUse);
                mockDatabase.getUser("NULLMAN");
                LastCall.Return(null);
            }

            //UserRegistration.setDatabase(mockDatabase);
        }

        public static void setDatabase(LINQDatabase toSet)
        {




            database = toSet;
        }

        public static bool register(User toRegister)
        {
            return true;
            //return database.registerUser(toRegister);
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