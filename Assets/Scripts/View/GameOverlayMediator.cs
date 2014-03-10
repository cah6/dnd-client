using System;
using UnityEngine;
using strange.extensions.mediation.impl;

public class GameOverlayMediator : Mediator
{
	[Inject]
	public GameOverlayView view{ get; set;}
	
	public override void OnRegister()
	{
		view.init ();
	}
	
	public override void OnRemove()
	{
		Debug.Log("Mediator OnRemove");
	}
}