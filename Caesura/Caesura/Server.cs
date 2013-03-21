using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesura
{
    class server
    {
        
        // I made this public because of test cases not being able to see it
        public iSocket socket;
        private Int32 standardPort = 3246; //0xcae{sura} = 3246

        public server()
        {
            socket = new aSocket();
        }

        public void setSocket(iSocket asocket)
        {
            socket = asocket;
        }

        // Will bind the current socket to whomever attempts to connect
        public void listen()
        {
            socket.listen(standardPort);
            setSocket(socket.accept());
        }

        public Boolean send(String message)
        {
            byte[] temp = Encoding.ASCII.GetBytes(message);

            return send(temp);
        }

        public Boolean send(byte[] buffer)
        {
            if (socket.isConnected())
            {
                return (socket.send(buffer, buffer.Length, 0) >= 0);
            }
            else
            {
                return false;
            }
                
        }


    }
}
