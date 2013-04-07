using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;

namespace Caesura
{ 
    [TestFixture()]
    class UserRegTests
    {
        private MockRepository mocks;

        [Test()]
        public void initializeTest()
        {
            User newUser = new User();
            Assert.IsNotNull(newUser);
        }

        [Test()]
        public void nameAcceptTest()
        {
            User newUser = new User();
            newUser.setName("TestName");
            Assert.AreEqual("TestName", newUser.getName());
        }

        [Test()]
        public void passAcceptTest()
        {
            User newUser = new User();
            newUser.setPass("TestPass");
            Assert.AreEqual("TestPass", newUser.getPass());
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void nullNameTest()
        {
            User newUser = new User();
            newUser.setName(null);
        }

        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void nullPassTest()
        {
            User newUser = new User();
            newUser.setPass(null);
        }

        /*[Test()]
        public void testWritten()
        {
            User newUser = new User();
            newUser.setName("TestName");
            newUser.setPass("TestPass");
            Assert.IsTrue(newUser.writeNP());
            
        }*/

        [Test()]
        public void testGetFromDatabase()
        {
            DatabaseInterface mockDatabase = mocks.Stub<DatabaseInterface>();

            User zarakavaUse = new User();
            zarakavaUse.setName("Zarakava");
            zarakavaUse.setPass("Testing");

            using (mocks.Record())
            {

                // The mock will return "Whale Rider" when the call is made with 24
                mockDatabase.getUser("Zarakava");
                LastCall.Return(zarakavaUse);
                mockDatabase.getUser("NULLMAN");
                LastCall.Return(null);
            }

            UserRegistration.Database = mockDatabase;
            Assert.IsTrue(UserRegistration.isRegistered("Zarakava"));
            Assert.IsFalse(UserRegistration.isRegistered("NULLMAN"));



        }



    }
}