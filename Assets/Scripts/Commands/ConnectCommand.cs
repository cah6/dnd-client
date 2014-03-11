using System;
using strange.extensions.signal.impl;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using UnityEngine;

public class ConnectSignal : Signal<String> { }

public class ConnectCommand : Command {
	
	[Inject]
	public String address { get; set; }

	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}

	public override void Execute() {
		GameObject go = new GameObject();
		go.name = address;
		//go.name = "WebSocketView";
		go.AddComponent<WebSocketView>();
		go.transform.parent = contextView.transform;
	}
}
