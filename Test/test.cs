using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class Json_Player
{
    public Vector3 position { get; set; }
}

public class test : MonoBehaviour
{
    Json_Player json_player;
    private const string serverIP = "127.0.0.1";
    private const int serverPort = 12345;

    private TcpClient client;
    private NetworkStream stream;

    private void Start()
    {
        // json_player.position = transform.position;
        // SetPosition(ref json_player);
        // transform.position = GetPosition(ref json_player);
    }

    void Update()
    {
        Debug.Log($"{{position: {JsonUtility.ToJson(transform.position).ToString()}}}");
        // transform.position = GetPosition(ref json_player);

        // if (Input.GetAxisRaw("Horizontal") != 0)
        // {
        //     json_player.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        // }
        // else if (Input.GetAxisRaw("Vertical") != 0)
        // {
        //     json_player.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
        // }

        // SetPosition(ref json_player);
        // transform.position = GetPosition(ref json_player);
        // // Debug.Log(json_player.position.x + ", " + json_player.position.y + ", " + json_player.position.z);
    }

    private void SetPosition(ref Json_Player json_player)
    {
        try
        {
            // 创建Socket对象
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 连接Python服务器
            socket.Connect(serverIP, serverPort);

            // 将json_player对象转换为JSON字符串
            string jsonMessage = JsonConvert.SerializeObject(json_player);

            // 将JSON字符串转换为字节数组并发送给Python服务器
            byte[] buffer = Encoding.UTF8.GetBytes(jsonMessage);
            socket.Send(buffer);

            // 关闭Socket连接
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Failed to communicate with Python server: " + e.Message);
        }
        finally
        {
            Disconnect();
        }
    }

    private Vector3 GetPosition(ref Json_Player json_player)
    {
        try
        {
            client = new TcpClient();
            client.Connect(serverIP, serverPort);
            stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 解析JSON字符串为位置信息
            json_player = JsonConvert.DeserializeObject<Json_Player>(message);
            return new Vector3(json_player.position.x, json_player.position.y, json_player.position.z);
        }
        catch (Exception e)
        {
            Debug.Log("Failed to connect to server: " + e.Message);
            return Vector3.zero;
        }
        finally
        {
            Disconnect();
        }
    }

    private void Disconnect()
    {
        if (stream != null)
            stream.Close();
        if (client != null)
            client.Close();
    }

    private void OnDestroy()
    {
        Disconnect();
    }
}
