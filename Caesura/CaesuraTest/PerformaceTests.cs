using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace CaesuraTest
{
    [TestFixture]
    class PerformaceTests
    {
        protected long[] times;
        protected long filesize;

        [TearDown]
        public void tearDown()
        {
            times = null;
        }

        #region helperfunctions for testSimultaniousDownloadMetric

        public void removeFile(String path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public void helperSimultaniousDownload(Client.Client client, int index)
        {

            // Create a timer
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            // Start it
            timer.Start();

            // Request the file
            var file = "testpic.jpg";
            var path = "C:\\Caesura\\" + index.ToString() + file;

            removeFile(path);

            client.requestFile(file, path);

            // Stop timer
            timer.Stop();

            // Store the time
            times[index] = timer.ElapsedMilliseconds;

            // Only have the first one compute the file size
            if (index == 0)
            {
                filesize = (new System.IO.FileInfo(path)).Length;
            }
        }

        public void helperSimultaniousDownloadGenerateStatistics()
        {
            double max = double.MinValue;
            double min = double.MaxValue;
            double mean = 0;
            double std = 0;

            // Sum everything up and store it in mean
            // While summing check if its the max/min
            foreach (long l in times)
            {
                mean += l;
                if (l > max)
                {
                    max = l;
                }
                if (l < min)
                {
                    min = l;
                }
            }
            // Then divide the mean by the length
            mean /= times.LongLength;

            // Compute the variance and store it in std
            foreach (long l in times)
            {
                std += Math.Pow((double)(l - mean), 2);
            }

            // Take the square root of the variance
            std = Math.Sqrt(std);

            // Display statistics
            Console.WriteLine(String.Format("The filesize was {0} bytes", filesize));

            Console.WriteLine(String.Format("The max dl time was {0} with a rate of {1} bytes/millisecond", max, filesize / max));
            Console.WriteLine(String.Format("The min dl time was  {0} with a rate of {1} bytes/millisecond", min, filesize / min));
            Console.WriteLine(String.Format("The mean dl time was {0} with a rate of {1} bytes/millisecond", mean, filesize / mean));
            Console.WriteLine(String.Format("The std dl time was {0} millisecond", std));

        }

        #endregion
        #region Statistics

        #region 2013-05-03
        /*
         * This was with users = 1000
         * Note: Not every transfer completely before the server started throwing memory exceptions
         * Note2: A bunch of images (maybe higher than 70%) had problems
            The filesize was 713088 bytes
            The max dl time was 66416 with a rate of 10.7366899542279 bytes/millisecond
            The min dl time was  23249 with a rate of 30.6717708288529 bytes/millisecond
            The mean dl time was 51663.655 with a rate of 13.8025077784373 bytes/millisecond
            The std dl time was 361312.649961187 millisecond
         */

        /*
         * This was with users = 100
         * Note: Some of the images (around 5) had problems with how they looked
         * Note2: Did not restart the server since the 1000 users test
            The filesize was 713088 bytes
            The max dl time was 23743 with a rate of 30.0336099060776 bytes/millisecond
            The min dl time was  8587 with a rate of 83.0427390241062 bytes/millisecond
            The mean dl time was 23199.29 with a rate of 30.7374923973966 bytes/millisecond
            The std dl time was 20967.1789850232 millisecond
         */

        /*
         * Note this was with users = 100
         * Note: Some of the images (4) had problems with how they looked
         * Note2: Did restart the server
            The filesize was 713088 bytes
            The max dl time was 16129 with a rate of 44.2115444230888 bytes/millisecond
            The min dl time was  11990 with a rate of 59.4735613010842 bytes/millisecond
            The mean dl time was 15391.33 with a rate of 46.330499053688 bytes/millisecond
            The std dl time was 5890.61423877001 millisecond
         */
        #endregion

        #region 2013-05-10
        /* Users = 100
         * Maybe 4 bad pictures
         * From now on unless specified the server was restarted
            The filesize was 713088 bytes
            The max dl time was 12987 with a rate of 54.9078309078309 bytes/millisecond
            The min dl time was  6346 with a rate of 112.368105893476 bytes/millisecond
            The mean dl time was 10470.58 with a rate of 68.1039636772748 bytes/millisecond
            The std dl time was 6987.70165075756 millisecond    
        */

        /* Users = 100
         * One bad picture
            The filesize was 713088 bytes
            The max dl time was 9741 with a rate of 73.204804434863 bytes/millisecond
            The min dl time was  7035 with a rate of 101.36289978678 bytes/millisecond
            The mean dl time was 8204.13 with a rate of 86.9181741391226 bytes/millisecond
            The std dl time was 6219.19426533695 millisecond
         */

        /* Users = 1000
         * The server threw a bunch of exceptions, still doesn't like 1K users
            The filesize was 0 bytes
            The max dl time was 26801 with a rate of 0 bytes/millisecond
            The min dl time was  0 with a rate of NaN bytes/millisecond
            The mean dl time was 32.02 with a rate of 0 bytes/millisecond
            The std dl time was 27040.7126681234 millisecond
         */

        #endregion

        #region 2013-05-17

        /*
         * users =100
         *The filesize was 713088 bytes
The max dl time was 10002 with a rate of 71.2945410917816 bytes/millisecond
The min dl time was  6236 with a rate of 114.350224502886 bytes/millisecond
The mean dl time was 9194.79 with a rate of 77.553484092622 bytes/millisecond
The std dl time was 11598.08417757 millisecond

         */
        #endregion
        #endregion
        [Test]
        public void testSimultaniousDownloadMetric()
        {
            int users = 100;
            Client.Client[] clients = new Client.Client[users];
            times = new long[users];

            ArrayList dltimes = new ArrayList(users);

            // Login all the clients
            var username = "Testuser";
            var password = "Test";
            for (int i = 0; i < users; i++)
            {
                clients[i] = new Client.Client();
                clients[i].connect();
                clients[i].login(username, password);

                Thread.Sleep(600);
            }

            // Create threads for them
            System.Threading.Thread[] threads = new System.Threading.Thread[users];
            for (int i = 0; i < users; i++)
            {
                var tempClient = clients[i];
                // for some reason if i pass "i" straight to the delegate it becomes the maximum value i can be (=users)
                //  rather than what the value of i was during the loop
                var tempIndex = i;

                ThreadStart tempThreadStarter = delegate { helperSimultaniousDownload(tempClient, tempIndex); };

                threads[i] = new Thread(tempThreadStarter);
            }

            // Start the DL threads
            foreach (Thread t in threads)
            {

                t.Start();
            }

            // Wait for them to finish
            foreach (Thread t in threads)
            {
                t.Join();
            }

            // Disconnect the clients
            foreach (Client.Client client in clients)
            {
                client.disconnect();
            }

            // Generate Statistics
            helperSimultaniousDownloadGenerateStatistics();
        }

        #region helperfunctions for testSimultaniousLoginMetric
        public void helperSimultaniousLoginGenerateStatistics()
        {
            double max = double.MinValue;
            double min = double.MaxValue;
            double mean = 0;
            double std = 0;

            // Sum everything up and store it in mean
            // While summing check if its the max/min
            foreach (long l in times)
            {
                mean += l;
                if (l > max)
                {
                    max = l;
                }
                if (l < min)
                {
                    min = l;
                }
            }
            // Then divide the mean by the length
            mean /= times.LongLength;

            // Compute the variance and store it in std
            foreach (long l in times)
            {
                std += Math.Pow((double)(l - mean), 2);
            }

            // Take the square root of the variance
            std = Math.Sqrt(std);

            // Display statistics
            Console.WriteLine(String.Format("The max login time was {0} millisecond", max));
            Console.WriteLine(String.Format("The min login time was  {0}millisecond", min));
            Console.WriteLine(String.Format("The mean login time was {0} millisecond", mean));
            Console.WriteLine(String.Format("The std login time was {0} millisecond", std));
        }

        public void helperSimultaniousLogin(Client.Client client, String username, String password, int index)
        {
            // Create a timer
            Stopwatch timer = new Stopwatch();
            // Start it
            timer.Start();
            // Login
            client.login(username, password);
            // Stop timer
            timer.Stop();

            // Store time
            times[index] = timer.ElapsedMilliseconds;



        }

        #endregion

        #region 2013-05-03

        /* This was with users = 100
         * delay = 100
            The max login time was 18 millisecond
            The min login time was  4millisecond
            The mean login time was 6.04 millisecond
            The std login time was 14.4858551697855 millisecond
         */
        /* This was with users = 1000
         * delay = 100
            The max login time was 39 millisecond
            The min login time was  4millisecond
            The mean login time was 6.305 millisecond
            The std login time was 80.5976116271444 millisecond
         */
        /* This was with users = 1000
         * delay = 10
            The max login time was 58 millisecond
            The min login time was  3millisecond
            The mean login time was 5.349 millisecond
            The std login time was 139.4030092932 millisecond
         */
        #endregion

        #region 2013-05-10
        /* Users = 100
         * Delay = 1
         * The server threw many exceptions
            The max login time was 0 millisecond
            The min login time was  0millisecond
            The mean login time was 0 millisecond
            The std login time was 0 millisecond
         */

        /*Users = 100, delay = 10 also caused exceptions
         */

        /* Users = 100
         * delay = 100
            The max login time was 378 millisecond
            The min login time was  5millisecond
            The mean login time was 14.58 millisecond
            The std login time was 433.848314506349 millisecond
         */

        #endregion

        #region 2013-5-17

        /* users = 100
         *The max login time was 329 millisecond
The min login time was  4millisecond
The mean login time was 20.7 millisecond
The std login time was 358.043293471613 millisecond
         */
        #endregion
        [Test]
        public void testSimultaniousLoginMetric()
        {
            int users = 100;
            Client.Client[] clients = new Client.Client[users];
            int delay = 100;

            times = new long[users];
            // Generate the clients & connect them
            for (int i = 0; i < users; i++)
            {
                clients[i] = new Client.Client();
                clients[i].connect();
                Thread.Sleep(1);
            }
            // Create the threads
            System.Threading.Thread[] threads = new System.Threading.Thread[users];
            var tempName = "Testuser";
            var tempPassword = "Test";

            for (int i = 0; i < users; i++)
            {
                var tempClient = clients[i];
                var tempIndex = i;
                ThreadStart tempThreadStarter = delegate { helperSimultaniousLogin(tempClient, tempName, tempPassword, tempIndex); };

                threads[i] = new Thread(tempThreadStarter);
            }

            // start the threads
            foreach (Thread t in threads)
            {
                t.Start();
                Thread.Sleep(delay);
            }

            // wait on the threads
            foreach (Thread t in threads)
            {
                t.Join();
            }

            // Disconnect the clients
            for (int i = 0; i < users; i++)
            {
                clients[i].disconnect();
            }

            // generate statistics
            helperSimultaniousLoginGenerateStatistics();
        }
    }
}
