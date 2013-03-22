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
        /*
            ArgumentOutOfRangeException	The port number is not valid.
            SocketException	An error occurred when attempting to access the socket. See the Remarks section for more information.
            ObjectDisposedException	The Socket has been closed.
            NotSupportedException	This method is valid for sockets in the InterNetwork or InterNetworkV6 families.
            InvalidOperationException	The Socket is Listening.
         */
        // Attempts to a given host & port.
        // Returns non-negative value upon a succesful connection
        int connect(string host, int port);

        /*
            ObjectDisposedException	The Socket has been closed.
         * 
            SocketException	An error occurred when attempting to access the socket. See the Remarks section for more information.
            ObjectDisposedException	The Socket has been closed.
            InvalidOperationException	The accepting socket is not listening for connections. You must call Bind and Listen before calling Accept.
        */
        // puts the socket into a blocking state and it will return the first socket that attempted to connect to the socket on the given port
        iSocket listen(int port);

        // disconnects the socket from whatever connection it was currently in
        void close();
        /*
            ArgumentNullException	buffer is null.
            ArgumentOutOfRangeException	size exceeds the size of buffer.
            SocketException	An error occurred when attempting to access the socket. See the Remarks section for more information.
            ObjectDisposedException	The Socket has been closed.
            SecurityException	A caller in the call stack does not have the required permissions.
         */
        // attempts to store the data recieved in the buffer
        // returns the number of bytes stored if successful
        // returns a negative number if unsuccessful
        int receive(byte[] buffer, int size, SocketFlags flags);
        /*
         *  ArgumentNullException	buffer is null.
            ArgumentOutOfRangeException	size is less than 0 or exceeds the size of the buffer.
            SocketException	socketFlags is not a valid combination of values.
            ObjectDisposedException	The Socket has been closed.
         */
        // attempts to send the data stored in the buffer along the socket
        // returns the number of bytes sent
        // returns a negative number if unsuccessful
        int send(byte[] buffer, int size, SocketFlags flags);

        // returns a true or false value if the socket was connected after the last transfer of data
        Boolean isConnected();
    }
}
