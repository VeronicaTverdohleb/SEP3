﻿using System.Net;
using System.Net.Sockets;

namespace BlazorApp.Services.JavaDataAccess;

public class JavaSocketConnection:IJavaSocketConnection
{
    //private string ToSend { get; set; }
    //private IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("192.168.0.6"), 4343);
    private IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2910);
    private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private bool connected = false;
    public void Connect()
    {
        if (!connected)
        {
            clientSocket.Connect(serverAddress);
            connected = true;
        }
    }

    // Sending
    public Task<string> SendMessage(string ingredientName)
    {
        if (string.IsNullOrEmpty(ingredientName))
        {
            throw new Exception($"ingredient name cannot be empty!");
        }
        
        string message = "{\"IngredientName\":\"";
        message += ingredientName;
        message += "\", \"Action\": \"get\"}";
        int toSendLen = System.Text.Encoding.ASCII.GetByteCount(message);
        byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(message);
        byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
        clientSocket.Send(toSendLenBytes);
        clientSocket.Send(toSendBytes);
        
        byte[] rcvLenBytes = new byte[4];
        clientSocket.Receive(rcvLenBytes);
        int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
        byte[] rcvBytes = new byte[rcvLen];
        clientSocket.Receive(rcvBytes);
        String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);

        Console.WriteLine("Client received: " + rcv);

        //clientSocket.Close();
        return Task.FromResult(rcv);
    }
}