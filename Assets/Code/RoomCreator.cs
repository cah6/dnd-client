using UnityEngine;
using WebSocketSharp;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class RoomCreator : View {

	[Inject]
	public ConnectSignal connectSignal { get; set; }

	private int width = Screen.width;
	private int height = Screen.height;

	string username = "testUser";

	string roomname = "testRoom";

	void OnGUI(){
		// Make a background box
		GUI.Box(new Rect(0, 0, width, height), "Game Loading");

		int unitWidth = width / 4;
		int unitHeight = height / 5;

		int padding = 5;

		//Username text label
		GUI.Label(new Rect(unitWidth*1 + padding, unitHeight*1 + padding,
			unitWidth - 2*padding, unitHeight - 2*padding), "Username");

		//field to input username text
		username = GUI.TextField (new Rect(unitWidth*2 + padding, unitHeight*1 + padding,
			unitWidth - 2*padding, unitHeight - 2*padding), username);

		//Create game button
		if(GUI.Button(new Rect(unitWidth*1 + padding, unitHeight*2 + padding,
			unitWidth - 2*padding, unitHeight - 2*padding), "Join / Create Game")) {
			this.gameObject.active = false;
			string address = "/" + roomname + "/" + username;
			connectSignal.Dispatch(address);
		}

		//field to input game name text
		roomname = GUI.TextField (new Rect(unitWidth*2 + padding, unitHeight*2f + padding,
			unitWidth - 2*padding, unitHeight - 2*padding), roomname);

	}
}
