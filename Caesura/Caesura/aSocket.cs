using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Caesura
{
    

    class aSocket : iSocket
    {
        private Socket socket;

        public aSocket() 
        { 
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
        }

        public aSocket(Socket sock)
        {
            socket = sock;
        }

        public void connect(string host, int port)
        {
           socket.Connect(host, port);
        }

        public void listen(int port)
        {
           socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
           // I think this will cause it to block until a connection exists
           socket.Blocking = true;
           // Not sure what a good backlog is
           socket.Listen(5);
        }

        public void close()
        {
            socket.Close();
        }

        public int receive(byte[] buffer, int size, SocketFlags flags)
        {
            return socket.Receive(buffer, size, flags);
        }

        public int send(byte[] buffer, int size, SocketFlags flags)
        {
            return socket.Send(buffer, size, flags);
        }


        public iSocket accept()
        {
            return new aSocket(socket.Accept());
        }

        public Boolean isConnected()
        {
            return socket.Connected;
        }

    }
}
