using System;
using UnityEngine;
using strange.extensions.mediation.impl;

public class MainMenuMediator : Mediator
{
	[Inject]
	public MainMenuView view{ get; set;}
	
	public override void OnRegister()
	{
		view.init ();
		show();
	}
	
	public override void OnRemove()
	{
		Debug.Log("Mediator OnRemove");
	}

	private void show(){
		gameObject.SetActive(true);
	}

	private void hide(){
		gameObject.SetActive(false);
	}
}