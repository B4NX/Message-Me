package interweb;

import java.io.*;
import java.net.*;

public class Main {

	public static DatagramSocket socket;
	public static void main(String[] args) throws Exception {
		System.out.println("Starting");
		socket=new DatagramSocket(777);
		
		int i=3;
		while(i!=0){
			Receive();
			--i;
		}
		socket.close();
		System.out.println("Ending");
	}
		
	private static void Receive() throws IOException{
		DatagramPacket receivedPacket=new DatagramPacket(new byte[20],20);
		
		System.out.println("Receiving");
		socket.receive(receivedPacket);
		
		String s=new String(receivedPacket.getData()).trim();
		System.out.println("Received: "+s);
		
		CheckEnd(s);
		
		Send(receivedPacket,s.toUpperCase());
	}
	private static void CheckEnd(String s){
		if (s.equalsIgnoreCase("EXIT")){
			System.out.println("Now exiting");
			socket.close();
			System.out.println("Goodbye");
			System.exit(0);
		}
	}
	private static void Send(DatagramPacket packet,String s) throws IOException {
		DatagramPacket sendpacket=new DatagramPacket(s.getBytes(),s.getBytes().length,packet.getAddress(),packet.getPort());

		System.out.println("Sending");
		socket.send(sendpacket);
	}
}
