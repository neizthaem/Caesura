using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    class client
    {
        

        public iSocket socket;
        private Int32 standardPort = 3246; //0xcae{sura} = 3246

        public client()
        {
            socket = new aSocket();
        }

        public void setSocket(iSocket asocket)
        {
            socket = asocket;
        }

        public void connect(string host)
        {
            socket.connect(host, standardPort);
        }

        public int recieve(byte[] buffer)
        {
            return socket.receive(buffer, buffer.Length, 0);
        }
    }
}
