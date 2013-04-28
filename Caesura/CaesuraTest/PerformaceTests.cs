using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesuraTest
{
    [TestFixture]
    class PerformaceTests
    {

        public long timeToDlFile(String file)
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

        

        #endregion

#endregion
        [Test]
        public void testLoginTransferHex()
        {
            long max = long.MinValue;
            long min = long.MaxValue;

            long total= 0;
            long tries = 10;

            long temp;

            string filename = "testpic.jpg";

            for (int i = 0; i < tries; i++)
            {
                temp = timeToDlFile(filename);

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

            Console.WriteLine("File size was"+(new System.IO.FileInfo("C:\\Caesura\\"+filename)).Length.ToString());
            Console.WriteLine("Average time was " + (total / tries).ToString() + "Milliseconds");
            Console.WriteLine("Max time was " + max.ToString() + "Milliseconds");
            Console.WriteLine("Min time was " + min.ToString() + "Milliseconds");
        }
    }
}
