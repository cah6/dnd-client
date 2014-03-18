using WebSocketSharp;
using UnityEngine;
using System;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.signal.impl;

//Wrap websocket service we're using so it can be easily swapped out as a service.
public class WebSocketService : IServerConnectionService{

	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView { get; set; }

	//Signals we want to dispatch
	[Inject]
	public ConnectionSuccessfulSignal connectionSuccessfulSignal { get; set; }

	//Third party websocket service
	private WebSocket ws { get; set; }

	private MessageParseStrategy messageParseStrategy;

	//When we bind this service, it'll call this constructor.
	[PostConstruct]
	public void PostConstruct() {
		messageParseStrategy = contextView.AddComponent<MessageParseStrategy>();
	}

	public void Connect(string url){
		//connect
		ws = new WebSocket("ws://localhost:9000/connect/" + url);

		//If there's an error connecting, it will show up here. 
		//TODO: Probably good to notify user on GUI.
		ws.OnError += (sender, e) => {
			Debug.Log("Error " + e.Message + " from " + sender);
		};

		//When the websocket opens, dispatch a signal to tell GUI it can go to correct menu.
		ws.OnOpen += (sender, e) => {
			connectionSuccessfulSignal.Dispatch();
		};

		ws.OnMessage += (sender, e) => 
		{
			switch (e.Type) {
				case Opcode.TEXT:
					Debug.Log ("Received: " + e);
					messageParseStrategy.parseInput(e.Data);
					break;
				case Opcode.BINARY:
					Debug.Log ("Message was raw data, not going to do anything.");
					break;
			}
		};

		ws.Connect();

		Debug.Log ("Connected to server!");
	}

	public void Send(string data){
		Debug.Log ("Sending message to server: " + data);
		ws.Send(data);
	}

	public void SendAsync(string data, Action<bool> completed){
		ws.SendAsync(data, completed);
	}
}