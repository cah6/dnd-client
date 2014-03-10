using System;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using UnityEngine;


public class RemovePlayerSignal : Signal<Player> { }


public class RemovePlayerCommand : Command {
	
	[Inject]
	public Player player { get; set; }

	public override void Execute()
	{
		UnityEngine.Object.Destroy(GameObject.Find(player.name));
	}
}
