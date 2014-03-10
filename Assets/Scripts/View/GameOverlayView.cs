using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

//Class that holds all main game UI components that are not GameObjects.
public class GameOverlayView : View{

	internal void init(){

	}

	void OnGUI(){
		//Create game button
		if(GUI.Button(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Create Unit")) {
			GameObject.Find("CreateCharacterView").SetActive(true);
		}
	}
}