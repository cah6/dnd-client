using UnityEngine;
using WebSocketSharp;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class CreateCharView : View {

	internal void init(){
	}

	public void OnClickCreateCharacter(){
		this.gameObject.SetActive(false);
	}

}
