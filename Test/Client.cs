using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];
    private string serverIP = "127.0.0.1"; // 服务器IP地址
    private int serverPort = 8888; // 服务器端口号

    private void Start()
    {
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);
            stream = client.GetStream();

            // 开始接收服务器消息的线程
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("无法连接到服务器：" + e.Message);
        }
    }

    private void ReceiveMessage()
    {
        while (true)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                // 处理接收到的消息
                Debug.Log("收到服务器消息：" + message);
            }
            catch (Exception e)
            {
                Debug.Log("接收消息错误：" + e.Message);
                break;
            }
        }
    }

    private void SendMessage_(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        stream.Flush();
    }

    private void Update()
    {
        // 监听左右移动输入
        float moveHorizontal = Input.GetAxis("Horizontal");
        SendMessage_(moveHorizontal.ToString());
    }

    private void OnDestroy()
    {
        if (stream != null)
            stream.Close();
        if (client != null)
            client.Close();
    }
}
