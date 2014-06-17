package interweb;

import java.net.InetAddress;

public class RemoteHost{

	InetAddress addr;
	int port;
	
	public RemoteHost(InetAddress _addr,int _port){
		this.addr=_addr;
		this.port=_port;
	}
	
	@Override
	public String toString() {
		return "RemoteHost [addr=" + addr + ", port=" + port + "]";
	}
	
	public InetAddress getAddr() {
		return addr;
	}
	public void setAddr(InetAddress addr) {
		this.addr = addr;
	}
	public int getPort() {
		return port;
	}
	public void setPort(int port) {
		this.port = port;
	}
}