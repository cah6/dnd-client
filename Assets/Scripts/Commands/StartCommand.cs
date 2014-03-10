using System;
using UnityEngine;
using strange.extensions.signal.impl;
using strange.extensions.context.api;
using strange.extensions.command.impl;

public class StartSignal : Signal { }

public class StartCommand : Command {
	
	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}
	
	//Executed when game starts, making the main menu screen.
	public override void Execute()
	{
		// GameObject prefabToMake = (GameObject)Resources.Load("prefabs/MainMenu");
		// NGUITools.AddChild(GameObject.Find("UI Root (2D)"), prefabToMake);
		// GameObject go = new GameObject();
		// go.name = "MainMenuView";
		// go.AddComponent<MainMenuView>();
		// go.transform.parent = contextView.transform;
	}
}