using UnityEngine;
using WebSocketSharp;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class MainMenuView : View {

	[Inject]
	public ConnectSignal connectSignal { get; set; }

	[Inject]
	public CreateCharMenuSignal createCharMenuSignal { get; set; }

	//Username input by user, label GameObject needs to be dragged from inspector.
	public UILabel username;

	//Gamename input by user, " ".
	public UILabel gamename;

	internal void init(){
	}

	public void OnClickMakeGameButton(){
		this.gameObject.SetActive(false);
		string address = gamename.text + "/" + username.text;
		Debug.Log(address);
		connectSignal.Dispatch(address);
	}

	public void OnClickCreateCharMenu(){
		this.gameObject.SetActive(false);
		createCharMenuSignal.Dispatch();
	}
}
