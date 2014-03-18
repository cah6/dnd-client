using UnityEngine;
using WebSocketSharp;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using JsonFx.Json;

public class CreateCharView : View {

	/* Signals we want to fire to proceed */
	[Inject]
	public MainMenuSignal mainMenuSignal { get; set; }

	[Inject]
	public SendToServerSignal sendToServerSignal { get; set; }
	/*------------------------------------*/

	//GUI label for character's given name, class, and race
	public UILabel txtName, txtClass, txtRace;

	internal void init(){
	}

	//On creating character, send server character creation info and go to main menu.
	public void OnClickCreateCharacter(){
		this.gameObject.SetActive(false);
		//create and send the character info to server
		Debug.Log("Sending server info for: " + txtName.text + " " + txtClass.text + " " + txtRace.text);
		Character c = new Character(txtName.text, txtClass.text, txtRace.text);
		sendToServerSignal.Dispatch(JsonWriter.Serialize(c));
		//go back to main menu
		mainMenuSignal.Dispatch();
	}

	//On cancel, just go to main menu.
	public void OnClickCancel(){
		this.gameObject.SetActive(false);
		mainMenuSignal.Dispatch();
	}

}
