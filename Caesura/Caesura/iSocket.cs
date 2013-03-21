using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Caesura
{
    // Interface Socket would probably be a better name
    public interface iSocket
    {
        // Needed by clientsocket to connect to the server
        void connect(string host, int port);

        // Needed by serversocket to listen for the client
        void listen(int port);
        void close();

        iSocket accept();

        // Needed by both
        int receive(byte[] buffer, int size, SocketFlags flags);
        int send(byte[] buffer, int size, SocketFlags flags);

        Boolean isConnected();
    }
}
