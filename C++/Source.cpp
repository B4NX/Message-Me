#include <WinSock2.h>
#include <ws2tcpip.h>
#include <stdio.h>
#include <iostream>

#pragma comment(lib, "Ws2_32.lib")

int main(){
	WSADATA wsaData;

	int iResult;
	
	//Initialize winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0){
		std::cout << "WSAStartup failed: " << iResult << std::endl;
		return 1;
	}
	return 0;
}