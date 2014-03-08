using WebSocketSharp;
using System;

//Helper class to allow programmer to set the websocket's URL before the websocket instance is actually instantiated.
public class WebSocketWrapper {

	public WebSocket ws { get; set; }

	public string url { get; set; }

	public WebSocketWrapper(){
		this.url = "overwriteMe!";
	}

	public void CreateInstance(){
		ws = new WebSocket(url);
	}

	public void Send(string data){
		ws.Send(data);
	}

	public void SendAsync(string data, Action<bool> completed){
		ws.SendAsync(data, completed);
	}

}