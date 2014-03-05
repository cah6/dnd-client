using UnityEngine;
using WebSocketSharp;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class WebSocketView : View {

	public Signal<string> messageReceivedSignal = new Signal<string>();

	public WebSocket ws;

	//Called on program startup, connects to server and defines OnMessage and OnErroor handling.
	internal void init () {
		ws = new WebSocket ("ws://localhost:9000/connect/cah6");

		ws.OnError += (sender, e) => {
			Debug.Log("Error " + e + " from " + sender);
		};

		ws.OnMessage += (sender, e) => 
		{
			switch (e.Type) {
				case Opcode.TEXT:
					Debug.Log ("Received: " + e);
					messageReceivedSignal.Dispatch(e.Data);
					break;
				case Opcode.BINARY:
					Debug.Log ("Message was raw data, not going to do anything.");
					break;
			}
		};

		ws.Connect();

		Debug.Log ("Connected to server!");
	}
}
