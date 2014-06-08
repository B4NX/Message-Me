import socket

def parseInts(data):
    """Takes a binary string and returns the ints
    contained in the string"""
    for s in data.split('\\'):
        print(s)

UDP_IP = "0.0.0.0"
UDP_PORT = 777
MESSAGE = "Help"

print("UDP Address is: " + str(UDP_IP) + ":" + str(UDP_PORT))

sock = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)

#sock.connect((UDP_IP,UDP_PORT))
sock.bind((UDP_IP,UDP_PORT))

#sock.send(bytes(MESSAGE,"UTF-8"))
while True:
    data, addr = sock.recvfrom(1024)
    print(data)
    parseInts(data)
    #print(int.from_bytes(data,"big"))
    print("lol")
    break
