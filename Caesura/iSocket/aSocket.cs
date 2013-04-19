using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace iSocket
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


        public static byte[] stringToBytes(String message,int length)
        {
            byte[] ret = new byte[length];
            byte[] temp = System.Text.UTF8Encoding.UTF8.GetBytes(message);
            for (int i = 0; i < Math.Min(temp.Length,length); i++)
            {
                ret[i] = temp[i];   
            }
            for (int i = Math.Min(temp.Length, length); i < length; i++)
            {
                ret[i] = (byte)'\0';
            }
            return ret;
        }

        public static String bytesToMessage(byte[] bytes)
        {
            if (bytes == null)
            {
                return "null";
            }
            String tempString = System.Text.UTF8Encoding.UTF8.GetString(bytes);
            int tempIndex = tempString.IndexOf('\0', 0);
            if (tempIndex <= 0)
            {
                return tempString;
            }
            else
            {
                String ret = tempString.Substring(0, tempIndex);

                return ret;
            }
        }

        public static String bytesToString(byte[] bytes)
        {
            if (bytes == null)
            {
                return "null";
            }
            String tempString = System.Text.UTF8Encoding.UTF8.GetString(bytes);
            return tempString;

        }

        public static byte[] stringToBytes(String message)
        {
            byte[] ret = System.Text.UTF8Encoding.UTF8.GetBytes(message);

            return ret;
        }


        public void connect(string host, int port)
        {
            socket.Connect(host, port);

        }

        public iSocket listen(int port)
        {
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            // I think this will cause it to block until a connection exists
            socket.Blocking = true;


            socket.Listen(1);
            return (iSocket)(new aSocket(socket.Accept()));

            
        }

        public void close()
        {
            socket.Close();
        }

        public byte[] receive(int length)
        {

            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = (byte)'A';
            }
            socket.Blocking = true;

            try
            {
                socket.Receive(buffer, buffer.Length, 0);
                return buffer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void send(byte[] buffer)
        {
            try
            {
                socket.Send(buffer, buffer.Length, 0);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }


    }
}
