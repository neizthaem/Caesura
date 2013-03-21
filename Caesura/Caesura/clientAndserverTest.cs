using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caesura
{
    class clientAndserverTest
    {
        String storage;

        /*
         * This test does not do any mocking and so it is subject to random issues
         * said random issues do not occur and this test is mainly for my own curiousity
         * also Visual Studios could use a spellchecker.
         */
        [Test]
        public void generic001Test()
        {
            Thread sT = new Thread(new ThreadStart(serverTestGeneric001));
            Thread cT = new Thread(new ThreadStart(clientTestGeneric001));

            sT.Start();
            cT.Start();

            while (cT.IsAlive || sT.IsAlive)
            {
                // yield
                Thread.Sleep(1);
            }
            Assert.AreEqual("Test", storage);
        }

        // This test is meant to run in combination with serverTestGeneric001()
        private void clientTestGeneric001()
        {
            client c = new client();
            c.connect("127.0.0.1");
            byte[] temp = new byte[4];
            Console.WriteLine("Client: " + c.recieve(temp));
            Console.Write("Client: ");
            foreach (Byte b in temp)
            {
                Console.Write(b.ToString());
            }
            Console.WriteLine("");
            Console.WriteLine("Client: "+System.Text.UTF8Encoding.UTF8.GetString(temp));
            storage = System.Text.UTF8Encoding.UTF8.GetString(temp);
        }
        // This test is meant to run in combination with cleintTestGeneric001()
        
        private void serverTestGeneric001()
        {
            server s = new server();
            s.listen();
            s.send("Test");
        }
    }
}
