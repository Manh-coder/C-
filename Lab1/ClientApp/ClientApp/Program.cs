using System;
using System.Net.Sockets;
using System.Text;

class ClientApp
{
    static void Main(string[] args)
    {
        string serverAddress = "127.0.0.1";
        int port = 8080;

        try
        {
            // Connect to the server
            TcpClient client = new TcpClient();
            Console.WriteLine("Connecting to server...");
            client.Connect(serverAddress, port);
            Console.WriteLine("Connected to server.");

            // Get the network stream
            NetworkStream stream = client.GetStream();

            // Send a message to the server
            Console.Write("Enter a message to send: ");
            string message = Console.ReadLine();
            byte[] messageData = Encoding.UTF8.GetBytes(message);
            stream.Write(messageData, 0, messageData.Length);
            Console.WriteLine("Message sent to server.");

            // Receive a response from the server
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string serverResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Received from server: {serverResponse}");

            // Close the connection
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
