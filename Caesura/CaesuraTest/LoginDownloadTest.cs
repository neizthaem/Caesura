using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Caesura;

namespace CaesuraTest
{

    [TestFixture()]
    class LoginDownloadTest
    {
        
        /*private static CaesuraMain cMain;

        [SetUp]
        public void TestLoginDownloadSetUp()
        {
            cMain = new CaesuraMain();
            
        }

        [TearDown]
        public void TestLoginDownloadTearDown()
        {
            cMain.end();
        }*/

        [Test()]
        public static void testLogin()
        {
            CaesuraMain cMain = new CaesuraMain();
            Assert.IsTrue(cMain.login("Zarakava","Testing"));
            cMain = new CaesuraMain();
            Assert.IsFalse(cMain.login("Zarakava", "TEsting"));
            cMain.end();
            
        }

        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public static void testAlreadyLoggedIn()
        {
            CaesuraMain cMain = new CaesuraMain();
            Assert.IsTrue(cMain.login("Zarakava", "Testing"));
            cMain.login("Zarakava", "Testing");
            

        }
        [Test()]
        public static void testGetFile()
        {
            CaesuraMain cMain = new CaesuraMain();
            Assert.IsTrue(cMain.login("Zarakava", "Testing"));
            bool tester = cMain.getFile("testpic.jpg");
            cMain.end();
            Assert.IsTrue(tester);
            
        }

        /*[Test()]
        public static void testShouldNotGetFile()
        {
            CaesuraMain cMain = new CaesuraMain();
            Assert.IsTrue(cMain.login("Zarakava", "Testing"));
            bool tester = cMain.getFile("shouldFail.txt");
            cMain.end();
            Assert.IsFalse(tester);

        }*/


        [Test()]
        [ExpectedException(typeof(InvalidOperationException))]
        public static void testNotLoggedIn()
        {
            CaesuraMain cMain = new CaesuraMain();
            cMain.getFile("ShouldFail.txt");


        }



    }
}
