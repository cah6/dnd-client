using System;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using UnityEngine;

public class UpdatePlayerSignal : Signal<Player> { }

public class UpdatePlayerCommand : Command {
	
	[Inject]
	public Player player { get; set; }

	public override void Execute()
	{
		Debug.Log("setting location to " + player.location.x + "," + player.location.y);
		Transform playerTransform = GameObject.Find(player.name).transform;
		playerTransform.position = new Vector3(player.location.x, player.location.y, 0);
	}
}
