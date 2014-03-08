using System;
using UnityEngine;
using strange.extensions.signal.impl;
using strange.extensions.context.api;
using strange.extensions.command.impl;

public class StartSignal : Signal { }

public class StartCommand : Command {
	
	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}
	
	public override void Execute()
	{
		GameObject go = new GameObject();
		go.name = "RoomCreatorView";
		go.AddComponent<RoomCreator>();
		go.transform.parent = contextView.transform;
	}
}