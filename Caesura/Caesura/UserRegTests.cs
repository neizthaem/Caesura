using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Caesura
{ 
    [TestFixture()]
    class UserRegTests
    {

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

    }
}

        //[Test()]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]