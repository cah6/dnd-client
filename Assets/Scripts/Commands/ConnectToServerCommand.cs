using System;
using strange.extensions.signal.impl;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using UnityEngine;

public class ConnectToServerSignal : Signal<string> { }

public class ConnectToServerCommand : Command {
	
	[Inject]
	public string address { get; set; }

	[Inject]
	public IServerConnectionService serverConnectionService{ get; set; }

	public override void Execute() {
		serverConnectionService.Connect(address);
	}
}
