// ManagedUdp_Received.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace System::Net;
using namespace System::Net::Sockets;

IPAddress^ getLocalAddress();

int main(array<System::String ^> ^args)
{
	Console::WriteLine("Your IP is: " + getLocalAddress()->ToString());
	UdpClient ^sender = gcnew UdpClient();
	IPEndPoint ^endPoint = gcnew IPEndPoint(IPAddress::Any, 666);
	sender->Client->Bind(endPoint);
	
	array<unsigned char> ^data = sender->Receive(endPoint);

	for (int i = 0; i < data->Length; ++i){
		Console::WriteLine(data[i]);
	}
	sender->Close();
	Console::WriteLine("Done");

	Console::ReadKey();
	return 0;
}

IPAddress^ getLocalAddress(){
	IPHostEntry ^host;
	String ^localIP = "";
	host = Dns::GetHostEntry(Dns::GetHostName());
	for each (IPAddress ^ip in host->AddressList){
		if (ip->AddressFamily == AddressFamily::InterNetwork){
			localIP = ip->ToString();
			break;
		}
	}
	return IPAddress::Parse(localIP);
}
