/// The only change in StartCommand is that we extend Command, not EventCommand

using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class StartCommand : Command {
	
	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}
	
	public override void Execute()
	{
		GameObject go = new GameObject();
		go.name = "WebSocketView";
		go.AddComponent<WebSocketView>();
		go.transform.parent = contextView.transform;
	}
}
