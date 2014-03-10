using System;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using UnityEngine;


public class AddPlayerSignal : Signal<Player> { }


public class AddPlayerCommand : Command {
	
	[Inject]
	public Player player { get; set; }

	public override void Execute()
	{
		GameObject characterPrefab = (GameObject) Resources.Load("Character");
		GameObject characterGO = (GameObject) GameObject.Instantiate(
			characterPrefab, new Vector3(player.location.x, player.location.y, 0), Quaternion.identity);
		characterGO.name = player.name;
	}
}
