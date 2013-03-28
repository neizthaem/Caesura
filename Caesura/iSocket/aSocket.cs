using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Caesura
{


    public class aSocket : iSocket
    {
        // Must be public for test cases
        public Socket socket;

        public aSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public aSocket(Socket sock)
        {
            socket = sock;
        }

        public int connect(string host, int port)
        {
            if (host == null)
            {
                return -1;
            }
            else if (host.Length == 0)
            {
                return -3;
            }
            socket.Connect(host, port);
            return 1;
        }

        public iSocket listen(int port)
        {

            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            // I think this will cause it to block until a connection exists
            socket.Blocking = true;

            socket.Listen(1);
            return new aSocket(socket.Accept());
        }

        public void close()
        {
            socket.Close();
        }

        public int receive(byte[] buffer)
        {
            if (buffer == null)
            {
                return -2;
            }
           
            try
            {

                return socket.Receive(buffer, buffer.Length, 0);
            }
            catch (SocketException)
            {
                return -1;
            }
            catch (ObjectDisposedException)
            {
                return -4;
            }
        }

        public int send(byte[] buffer)
        {
            if (buffer == null)
            {
                return -2;
            }

            try
            {
                return socket.Send(buffer, buffer.Length, 0);
            }
            catch (SocketException)
            {
                return -1;
            }
            catch (ObjectDisposedException)
            {
                return -4;
            }
        }


        public Boolean isConnected()
        {
            if (socket == null)
            {
                return false;
            }
            return socket.Connected;
        }

    }
}
