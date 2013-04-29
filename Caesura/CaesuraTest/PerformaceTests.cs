using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaesuraTest
{
    [TestFixture]
    class PerformaceTests
    {

        public long timeToDlFileLoginLogout(String file)
        {
            var client = new Client.Client();
            client.connect();

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

            if (System.IO.File.Exists("C:\\Caesura\\" + file))
            {
                System.IO.File.Delete("C:\\Caesura\\" + file);
            }
            Assert.IsFalse(System.IO.File.Exists("C:\\Caesura\\" + file));

            Assert.True(client.login("Testuser", "Test"));

            timer.Start();
            Assert.True(client.requestFile(file));
            timer.Stop();
            client.disconnect();

            Assert.IsTrue(System.IO.File.Exists("C:\\Caesura\\" + file));
            // Assert that the contents are correct
            Assert.AreEqual(System.IO.File.ReadAllBytes(file), System.IO.File.ReadAllBytes("C:\\Caesura\\" + file));

            return timer.ElapsedMilliseconds;
        }


        #region DL performance

        #region 2013-04-27

        // Nodelay = true
        // Maxpacketsize = 8192
        // Server verbose = true
        // Client verbose = false

        // tries = 10
        // file = testpic.jpg
        // Min was initialized as long.minValue

        // File size was713088
        // Average time was 10037Milliseconds
        // Max time was 10696Milliseconds
        // Min time was -9223372036854775808Milliseconds


        // Same as above except Mini initialized correctly
        // File size was713088
        // Average time was 9384Milliseconds
        // Max time was 9980Milliseconds
        // Min time was 9299Milliseconds

        // Verbose = false

        // File size was713088
        // Average time was 14222Milliseconds
        // Max time was 14261Milliseconds
        // Min time was 14215Milliseconds

        // Lowered Server sleep to 1
        // File size was713088
        // Average time was 1470Milliseconds
        // Max time was 1774Milliseconds
        // Min time was 1417Milliseconds



        #endregion

        #endregion
        [Test]
        public void testLoginDownloadDisconnect()
        {
            long max = long.MinValue;
            long min = long.MaxValue;

            long total = 0;
            long tries = 10;

            long temp;

            string filename = "testpic.jpg";

            for (int i = 0; i < tries; i++)
            {
                temp = timeToDlFileLoginLogout(filename);

                if (temp > max)
                {
                    max = temp;
                }
                else if (temp < min)
                {
                    min = temp;
                }

                total += temp;
            }

            Console.WriteLine("File size was" + (new System.IO.FileInfo("C:\\Caesura\\" + filename)).Length.ToString());
            Console.WriteLine("Average time was " + (total / tries).ToString() + "Milliseconds");
            Console.WriteLine("Max time was " + max.ToString() + "Milliseconds");
            Console.WriteLine("Min time was " + min.ToString() + "Milliseconds");
        }

        public long timeToDLWithoutLogin(String file, Client.Client client)
        {

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

            if (System.IO.File.Exists("C:\\Caesura\\" + file))
            {
                System.IO.File.Delete("C:\\Caesura\\" + file);
            }
            Assert.IsFalse(System.IO.File.Exists("C:\\Caesura\\" + file));

            timer.Start();
            Assert.True(client.requestFile(file));
            timer.Stop();
            client.disconnect();

            Assert.IsTrue(System.IO.File.Exists("C:\\Caesura\\" + file));
            // Assert that the contents are correct
            Assert.AreEqual(System.IO.File.ReadAllBytes(file), System.IO.File.ReadAllBytes("C:\\Caesura\\" + file));

            return timer.ElapsedMilliseconds;
        }

        public void testLoginDownloadSimultaniousUsersHelper(String username, String password, String file, int index, long[] times)
        {
            var client = new Client.Client();

            client.connect();

            client.login(username, password);

            long temp = timeToDLWithoutLogin(file, client);
            
            client.disconnect();
        }

        [Test]
        public void testLoginDownloadSimultaniousUsers()
        {
            int users = 3;
            long[] times = new long[users];
            Thread[] threads = new Thread[users];

            var baseName = "PerformanceUser";
            var password = "Performance";

            var file = "testpic.jpg";

            var db = ObjectMother.EmptyDatabase();

            Server.UserRegistration.setDatabase(db);


            for (int i = 0; i < users; i++)
            {
                // Register a bunch of users
                var temp1 = baseName + i.ToString();
                Server.User addNew = new Server.User();
                addNew.PasswordHash = password;
                addNew.Username = temp1;
                Assert.True(Server.UserRegistration.register(addNew));
                // Generate a bunch of threads

                threads[i] = new Thread(new ThreadStart(delegate { testLoginDownloadSimultaniousUsersHelper(temp1, password, file, i, times); }));


            }

            for (int i = 0; i < users; i++)
            {
                // Start the threads
                threads[i].Start();

                // Put a slight delay
                Thread.Sleep(100);
            }

            for (int i = 0; i < users; i++)
            {
                // Wait for them to finish
                threads[i].Join();
            }

            // Generate statistics
            long max = long.MinValue;
            long min = long.MaxValue;

            long total = 0;
            long tries = 10;

            foreach (long temp in times)
            {

                if (temp > max)
                {
                    max = temp;
                }
                else if (temp < min)
                {
                    min = temp;
                }

                total += temp;
            }

            Console.WriteLine("File size was" + (new System.IO.FileInfo("C:\\Caesura\\" + file)).Length.ToString());
            Console.WriteLine("Average time was " + (total / tries).ToString() + "Milliseconds");
            Console.WriteLine("Max time was " + max.ToString() + "Milliseconds");
            Console.WriteLine("Min time was " + min.ToString() + "Milliseconds");


            ObjectMother.ClearDatabase(db);


        }

    }
}
