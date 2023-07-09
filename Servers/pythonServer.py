import socket
import threading

server_ip = "127.0.0.1"  # 服务器IP地址
server_port = 8888  # 服务器端口号
clients = []  # 存储已连接的客户端

# 数据
PlayerData = {
    'position': {
        'x': 1.23,
        'y': 2.34,
        'z': 3.45
    }
}


def handle_client(client_socket, client_address):
    while True:
        try:
            data = client_socket.recv(1024).decode("utf-8")
            if not data:
                break
            # 处理接收到的客户端消息
            print("收到来自{}的消息：{}".format(client_address, data))
            
            # 向所有客户端广播消息
            broadcast_message(data)
            
        except Exception as e:
            print("接收消息错误：", e)
            break

    print("与{}的连接已关闭".format(client_address))
    clients.remove(client_socket)
    client_socket.close()

def broadcast_message(message):
    for client in clients:
        try:
            client.sendall(message.encode("utf-8"))
        except Exception as e:
            print("发送消息错误：", e)

def start_server():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((server_ip, server_port))
    server_socket.listen(5)
    print("服务器已启动，监听 {}:{}".format(server_ip, server_port))

    while True:
        client_socket, client_address = server_socket.accept()
        print("与{}建立连接".format(client_address))
        clients.append(client_socket)
        
        client_thread = threading.Thread(target=handle_client, args=(client_socket, client_address))
        client_thread.start()

start_server()
