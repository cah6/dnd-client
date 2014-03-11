using System;
using UnityEngine;
using strange.extensions.mediation.impl;

public class CreateCharMediator : Mediator
{
	[Inject]
	public CreateCharView view{ get; set;}

	[Inject]
	public CreateCharMenuSignal createCharMenuSignal { get; set; }
	
	public override void OnRegister()
	{
		createCharMenuSignal.AddListener(show);

		view.init ();
		hide();
	}
	
	public override void OnRemove()
	{
		Debug.Log("Mediator OnRemove");

		createCharMenuSignal.RemoveListener(show);
	}

	private void show(){
		gameObject.SetActive(true);
	}

	private void hide(){
		gameObject.SetActive(false);
	}
}