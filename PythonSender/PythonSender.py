import socket

UDP_IP = "127.0.0.1"
UDP_PORT = 666
MESSAGE = "Help"

print("UDP Address is: " + str(UDP_IP) + ":" + str(UDP_PORT))
print("Message to send is: " + str(MESSAGE))

sock = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)

sock.connect((UDP_IP,UDP_PORT))

sock.send(bytes(MESSAGE,"UTF-8"))