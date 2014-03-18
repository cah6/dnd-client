using System;
using UnityEngine;
using strange.extensions.mediation.impl;

public class MainMenuMediator : Mediator
{
	[Inject]
	public MainMenuView view{ get; set;}

	/* Signals we want to listen for to turn on */
	[Inject]
	public ConnectionSuccessfulSignal connectionSuccessfulSignal { get; set; }
	
	[Inject]
	public MainMenuSignal mainMenuSignal { get; set; }
	/*------------------------------------------*/
	
	//Called after all injections have been made.
	public override void OnRegister()
	{		
		//add listeners to turn this menu on
		connectionSuccessfulSignal.AddListener(show);
		mainMenuSignal.AddListener(show);

		view.init ();	//startup the view

		hide();			//hide on startup
	}
	
	//Called when the associated view is deleted.
	public override void OnRemove()
	{	
		//remove listeners to turn this menu on
		connectionSuccessfulSignal.RemoveListener(show);
		mainMenuSignal.AddListener(show);
	}

	private void show(){
		gameObject.SetActive(true);
	}

	private void hide(){
		gameObject.SetActive(false);
	}
}
