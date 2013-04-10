using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Caesura;

namespace Caesura
{ 
    [TestFixture()]
    class UserRegTests
    {
        public MockRepository mocks = new MockRepository();

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
        public void testIsRegisteredInDatabase()
        {
            DatabaseInterface mockDatabase = mocks.Stub<DatabaseInterface>();

            User zarakavaUse = new User();
            zarakavaUse.setName("Zarakava");
            zarakavaUse.setPass("Testing");

            using (mocks.Record())
            {
                mockDatabase.getUser("Zarakava");
                LastCall.Return(zarakavaUse);
                mockDatabase.getUser("NULLMAN");
                LastCall.Return(null);
            }

            UserRegistration.setDatabase(mockDatabase);
            Assert.IsTrue(UserRegistration.isRegistered("Zarakava"));
            Assert.IsFalse(UserRegistration.isRegistered("NULLMAN"));
        }

        [Test()]
        public void testRegisteredInDatabase()
        {
            DatabaseInterface mockDatabase = mocks.Stub<DatabaseInterface>();

            User zarakavaUse = new User();
            zarakavaUse.setName("Zarakava");
            zarakavaUse.setPass("Testing");

            using (mocks.Record())
            {

                // The mock will return "Whale Rider" when the call is made with 24
                mockDatabase.registerUser(zarakavaUse);
                LastCall.Return(true);
                mockDatabase.registerUser(null);
                LastCall.Return(false);
            }

            UserRegistration.setDatabase(mockDatabase);
            Assert.IsTrue(UserRegistration.register(zarakavaUse));
            Assert.IsFalse(UserRegistration.register(null));
        }

        [Test()]
        public void testLogin()
        {
            DatabaseInterface mockDatabase = mocks.Stub<DatabaseInterface>();

            User zarakavaUse = new User();
            zarakavaUse.setName("Zarakava");
            zarakavaUse.setPass("Testing");

            using (mocks.Record())
            {

                
                mockDatabase.getUser("Zarakava");
                LastCall.Return(zarakavaUse);
                mockDatabase.getUser("NULLMAN");
                LastCall.Return(null);
            }

            UserRegistration.setDatabase(mockDatabase);
            Assert.IsTrue(UserRegistration.login("Zarakava", "Testing"));
            
            Assert.IsFalse(UserRegistration.login("Zarakava", "NotTesting"));
            Assert.IsFalse(UserRegistration.login("NULLMAN", "NotTesting"));
        }
    }
}