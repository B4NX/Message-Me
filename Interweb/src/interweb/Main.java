package interweb;

import java.io.*;
import java.net.*;

public class Main {

	public static DatagramSocket socket;
	public static void main(String[] args) throws Exception {
		System.out.println("Starting");
		socket=new DatagramSocket(777);
		
		while(true){
			Receive();
		}
	}
		
	private static void Receive() throws IOException{
		DatagramPacket receivedPacket=new DatagramPacket(new byte[1024],1024);
		
		System.out.println("Receiving");
		socket.receive(receivedPacket);
		
		String s=new String(receivedPacket.getData()).trim();
		System.out.println("Received: "+s);
		
		if (s.equalsIgnoreCase("snsd")){
			Send(receivedPacket,"GOODBYE");
			System.out.println("Now exiting");
			socket.close();
			System.out.println("Goodbye");
			System.exit(0);
		}
		
		Send(receivedPacket,s.toUpperCase());
	}
	private static void Send(DatagramPacket packet,String s) throws IOException {
		DatagramPacket sendpacket=new DatagramPacket(s.getBytes(),s.getBytes().length,packet.getAddress(),packet.getPort());

		System.out.println("Sending");
		socket.send(sendpacket);
	}
}
