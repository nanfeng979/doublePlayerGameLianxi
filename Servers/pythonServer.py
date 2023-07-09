import socket
import threading

# 服务器配置
SERVER_IP = '127.0.0.1'  # 服务器IP地址
SERVER_PORT = 8888  # 服务器端口号

# 客户端处理线程
def handle_client(client_socket, client_address):
    print(f'与客户端 {client_address} 建立连接')

    while True:
        try:
            # 接收客户端消息
            data = client_socket.recv(1024)
            if not data:
                break
            message = data.decode('utf-8')
            # 处理客户端消息
            print(f'接收到客户端 {client_address} 的消息: {message}')
        except Exception as e:
            print(f'与客户端 {client_address} 的连接异常: {str(e)}')
            break

    # 关闭客户端连接
    client_socket.close()
    print(f'与客户端 {client_address} 的连接已关闭')

def start_server():
    # 创建服务器套接字
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((SERVER_IP, SERVER_PORT))
    server_socket.listen(5)
    print('服务器已启动')

    while True:
        try:
            # 等待客户端连接
            client_socket, client_address = server_socket.accept()
            # 创建客户端处理线程
            client_thread = threading.Thread(target=handle_client, args=(client_socket, client_address))
            client_thread.start()
        except KeyboardInterrupt:
            print('服务器已停止')
            break

    # 关闭服务器套接字
    server_socket.close()

# 启动服务器
start_server()
