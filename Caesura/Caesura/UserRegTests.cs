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

        }



    }
}

        //[Test()]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]