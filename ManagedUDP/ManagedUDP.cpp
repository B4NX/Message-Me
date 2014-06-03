// ManagedUDP.cpp : main project file.

#include "stdafx.h"
#include <stdio.h>
#include <iostream>

using namespace System;
using namespace System::Net;
using namespace System::Net::Sockets;

int main(array<System::String ^> ^args)
{
	Console::WriteLine("Essay number: 422");
	UdpClient ^client = gcnew UdpClient(777);
	Console::Write("Enter the IP address to connect to: ");
	IPAddress ^address = IPAddress::Parse(Console::ReadLine());
	client->Connect(gcnew IPEndPoint(address, 666));
	array<Byte> ^data = { 0, 1, 2, 3, 4, 5 };
	client->Send(data, data->Length);

	client->Close();
	Console::WriteLine("Done");
	Console::ReadKey();
	return 0;
}
