using UnityEngine;
using WebSocketSharp;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using JsonFx.Json;

public class MainMenuView : View {

	/* Signals we want to fire to proceed */
	[Inject]
	public CreateCharMenuSignal createCharMenuSignal { get; set; }

	[Inject]
	public SendToServerSignal sendToServerSignal { get; set; }
	/*------------------------------------*/

	//Gamename input by user, label GameObject needs to be dragged from inspector.
	public UILabel gamename;

	internal void init(){
	}

	public void OnClickMakeGameButton(){
		this.gameObject.SetActive(false);
		sendToServerSignal.Dispatch(JsonWriter.Serialize(new JoinGame(gamename.text)));
	}

	public void OnClickCreateCharMenu(){
		this.gameObject.SetActive(false);
		createCharMenuSignal.Dispatch();
	}
}


