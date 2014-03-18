using System;
using UnityEngine;
using strange.extensions.mediation.impl;

public class LoginMenuMediator : Mediator
{
	[Inject]
	public LoginMenuView view{ get; set; }

	/* Signals we want to listen for to turn on */
	[Inject]
	public LoginMenuSignal loginMenuSignal { get; set; }
	/*------------------------------------------*/

	
	public override void OnRegister()
	{
		//listen to global signals to show our menu
		loginMenuSignal.AddListener(show);

		view.init ();

		//show or hide on startup
		show();
	}
	
	public override void OnRemove()
	{
		//remove global signals we're listening for
		loginMenuSignal.RemoveListener(show);
	}

	private void show(){
		gameObject.SetActive(true);
	}

	private void hide(){
		gameObject.SetActive(true);
	}
}