using System;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using UnityEngine;

public class SendToServerSignal : Signal<string> { }

public class SendToServerCommand : Command {
	
	[Inject]
	public string message { get; set; }

	//Inject service since we need to call a method on it
	[Inject]
	public IServerConnectionService serverConnectionService { get; set; }

	public override void Execute() {
		serverConnectionService.Send(message);
	}
}
