using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ClientSocket : MonoBehaviour
{
    TcpClient client;
    string serverIP = "127.0.0.1";
    int port = 8000;
    byte[] receivedBuffer;
    StreamReader reader;
    bool socketReady = false;
    NetworkStream stream;

    void Start()
    {
        CheckReceive();
    }

    void Update()
    {
        if (socketReady && stream.DataAvailable)
        {
            receivedBuffer = new byte[100];
            stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            string msg = Encoding.UTF8.GetString(receivedBuffer, 0, receivedBuffer.Length);
            Debug.Log(msg);
        }
    }

    void CheckReceive()
    {
        if (socketReady) return;

        try
        {
            client = new TcpClient(serverIP, port);
            if (client.Connected)
            {
                stream = client.GetStream();
                Debug.Log("CONNECT SUCCESSFUL...");
                socketReady = true;
            }
        }
        catch (Exception e)
        {
            Debug.Log("On Client Connect Exception " + e);
        }

    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }
    
    void CloseSocket()
    {
        if (!socketReady) return;

        reader.Close();
        client.Close();
        socketReady = false;
    }
}
