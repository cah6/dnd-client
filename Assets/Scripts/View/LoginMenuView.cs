using UnityEngine;
using WebSocketSharp;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class LoginMenuView : View {

	/* Signals we want to fire to proceed */
	[Inject]
	public ConnectToServerSignal connectToServerSignal { get; set; }
	/*------------------------------------*/

	//Should be dragged in inspector
	public UILabel username;

	internal void init(){
	}

	//Wired through NGUI to fire on a button click.
	public void OnLoginMenuClick(){
		gameObject.SetActive(false);
		connectToServerSignal.Dispatch(username.text);
	}

}