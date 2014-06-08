package interweb;

import java.io.*;
import java.net.*;

public class Main {

	public static void main(String[] args) throws Exception {
		// TODO Auto-generated method stub
		System.out.println("Starting");
		DatagramSocket socket=new DatagramSocket(12142);
		byte[] receivedData = new byte[1024];
		byte[] sendData=new byte[1024];
		
		while(true){
			DatagramPacket receivedPacket =new DatagramPacket(receivedData,receivedData.length);
			socket.receive(receivedPacket);
			String sentence=new String(receivedPacket.getData());
			System.out.println("Received: "+sentence);
			
			InetAddress address=receivedPacket.getAddress();
			int port= receivedPacket.getPort();
			
			String capitalizedSentence=sentence.toUpperCase();
			sendData=capitalizedSentence.getBytes();
			
			DatagramPacket sendPacket=new DatagramPacket(sendData,sendData.length,address,port);
			socket.send(sendPacket);
			
		}
	}

}
