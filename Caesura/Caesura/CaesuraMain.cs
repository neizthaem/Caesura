using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Rhino.Mocks;

namespace Caesura
{
    public class CaesuraMain
    {
        public MockRepository mocks = new MockRepository();
        Client.Client client = null;
        Server.Server server = null;
        Thread serverThread = null;
        User currentUser = null;


        public CaesuraMain()
        {
            client = new Client.Client();
            server = new Server.Server();
            serverThread = new Thread(new ThreadStart(server.run));
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

            serverThread.Start();
            System.Threading.Thread.Sleep(5000);
        }


        public bool login(string userName, string pass)
        {
            if (currentUser == null)
            {
                if (UserRegistration.login(userName, pass))
                {
                    currentUser = new User();
                    currentUser.setName(userName);
                    currentUser.setPass(pass);
                    return true;
                }
                return false;
            }
            throw new InvalidOperationException("Already Logged in");

        }


        public bool getFile(string fileName)
        {
            if (currentUser != null)
            {

                //check permissions later

            if (File.Exists(fileName))
                {
                    client.connect();
                    bool returner = client.requestFile(fileName);
                    client.disconnect();
                    return returner;
                }
                return false;
            }
            throw new InvalidOperationException("Not Logged In");
        }

        public void end()
        {
            serverThread.Abort();
            serverThread = null;
            server.socket.close();
            server = null;
            client = null;
            mocks = null;
        }
    }
}
