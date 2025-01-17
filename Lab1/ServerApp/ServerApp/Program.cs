using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ServerApp
{
    static void Main(string[] args)
    {
        int port = 8080;
        TcpListener server = null;

        try
        {
            // Create a TcpListener to listen on a specific port
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server started. Listening on port " + port);

            while (true)
            {
                Console.WriteLine("Waiting for a client...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                // Handle the connected client
                NetworkStream stream = client.GetStream();

                // Read data sent by the client
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Received from client: {clientMessage}");

                // Process the message and convert to uppercase
                string responseMessage = clientMessage.ToUpper();

                // Send the response back to the client
                byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                stream.Write(responseData, 0, responseData.Length);
                Console.WriteLine($"Sent to client: {responseMessage}");

                // Close the connection
                client.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            server?.Stop();
            Console.WriteLine("Server stopped.");
        }
    }
}
