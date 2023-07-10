using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class OperationSendMessage : MonoBehaviour
{
    public static OperationSendMessage Instance;
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];
    private string serverIP = "127.0.0.1"; // 服务器IP地址
    private int serverPort = 8888; // 服务器端口号

    void Start()
    {
        if(Instance == null) {
            Instance = this;
        }

        ConnectToServer();

        PlayerData playerData = new PlayerData();
        playerData.playerName = PlayerSet.Instance.playerName;
        OperationReceiveMessage.Instance.playerData = playerData;
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
                OperationReceiveData(message);
            }
            catch(ObjectDisposedException e){
                Debug.Log("断开连接：" + e.Message);
                break;
            }
            catch (Exception e)
            {
                Debug.Log("接收消息错误：" + e.Message);
            }
        }
    }

    private void OperationReceiveData(string data) {
        OperationReceiveMessage.Instance.playerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void SendMessage_(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        stream.Flush();
    }

    private void OnDestroy()
    {
        if (stream != null)
            stream.Close();
        if (client != null)
            client.Close();
    }
}
