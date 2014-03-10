using System;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using JsonFx.Json;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public delegate void Task();

public class WebSocketMediator : Mediator
{
	[Inject]
	public WebSocketView view{ get; set;}

	private Queue<Task> taskQueue = new Queue<Task>();
	private object queueLock = new object();

	[Inject]
	public AddPlayerSignal addPlayerSignal { get; set; }

	[Inject]
	public RemovePlayerSignal removePlayerSignal { get; set; }

	[Inject]
	public UpdatePlayerSignal updatePlayerSignal{ get; set; }

	[Inject]
	public CreateMapSignal createMapSignal { get; set; }
	
	public override void OnRegister()
	{
		//Listen to the view saying that it got a message from the server
		view.messageReceivedSignal.AddListener(parseInput);
		
		//Start the view up
		view.init ();
	}
	
	public override void OnRemove()
	{
		// //Clean up listeners when this mediator is removed
		view.messageReceivedSignal.RemoveListener(parseInput);
		Debug.Log("Mediator OnRemove");
	}

	//Update is called once per frame
	void Update () {
		lock (queueLock)
		{
			if (taskQueue.Count > 0)
				taskQueue.Dequeue()();
		}
	}

	//Used to schedule a task on our queue, since Unity methods cannot be called in the thread a message is received in.
	public void ScheduleTask(Task newTask)
	{
		lock (queueLock)
		{
			if (taskQueue.Count < 100)
				taskQueue.Enqueue(newTask);
		}
	}
	
	//Parse the Json string received from the server. Get the data type from "type" field at root of string,
	//then use the rest of the data accordingly.
	private void parseInput(string e) {
		Debug.Log ("Received: " + e);

		//for now, using SimpleJSON to find the type of data we need to switch to
		var N = JSON.Parse(e);

		switch (N["type"].Value) {
			case "startInfoType" : 
				Debug.Log("Updating grid size!");
				ScheduleTask(new Task (delegate {
					var startInfo = JsonReader.Deserialize<StartInfo>(e);
					createMapSignal.Dispatch(new MapInfo(startInfo.gridSize.x, startInfo.gridSize.y));
					foreach (Player p in startInfo.players) {
						addPlayerSignal.Dispatch(p);
					}
					}));
				break;
			case "positionType" :
				Debug.Log("Updating player location!");
				ScheduleTask (new Task (delegate { 
					var player = JsonReader.Deserialize<Player>(e);
					updatePlayerSignal.Dispatch(player);
					}));
				break;
			case "joinType" :
				Debug.Log("Adding player to the game.");
				ScheduleTask(new Task (delegate {
					var player = JsonReader.Deserialize<Player>(e);
					addPlayerSignal.Dispatch(player);
					}));
				break;
			case "quitType" :
				Debug.Log("Removing player from the game.");
				ScheduleTask(new Task (delegate {
					var player = JsonReader.Deserialize<Player>(e);
					removePlayerSignal.Dispatch(player);
					}));
				break;
			default: Debug.Log("Could not recognize input type: " + N["type"].Value); break;
		}
	}
}
