using UnityEngine;
using WebSocketSharp;
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
using strange.extensions.signal.impl;

public delegate void Task();

public class WebSocketView : View {

	public WebSocket ws;

	private Queue<Task> taskQueue = new Queue<Task>();
	private object queueLock = new object();

	[Inject]
	public IEventDispatcher dispatcher{get; set;}

	//Called on program startup, connects to server and defines OnMessage and OnErroor handling.
	internal void init () {
		ws = new WebSocket ("ws://localhost:9000/connect/cah6");

		ws.OnError += (sender, e) => {
			Debug.Log("Error " + e + " from " + sender);
		};

		ws.OnMessage += (sender, e) => 
		{
			switch (e.Type) {
				case Opcode.TEXT:
				parseInput(e.Data);
				break;
				case Opcode.BINARY:
				Debug.Log ("Message was raw data!");
				break;
			}
		};

		ws.Connect();

		Debug.Log ("Connected to server!");
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
	void parseInput(string e) {
		Debug.Log ("Received: " + e);

		//for now, using SimpleJSON to find the type of data we need to switch to
		var N = JSON.Parse(e);

		switch (N["type"].Value) {
			case "startInfoType" : 
				Debug.Log("Updating grid size!");
				ScheduleTask(new Task (delegate {
					var startInfo = JsonReader.Deserialize<StartInfo>(e);
					GenerateGrid(startInfo.gridSize.x, startInfo.gridSize.y);
					AddPlayers(startInfo.players);
					}));
				break;
			case "positionType" :
				Debug.Log("Updating player location!");
				ScheduleTask (new Task (delegate { 
					var player = JsonReader.Deserialize<Player>(e);
					UpdateLocation(player);
					}));
				break;
			case "joinType" :
				Debug.Log("Adding player to the game.");
				ScheduleTask(new Task (delegate {
					var player = JsonReader.Deserialize<Player>(e);
					AddPlayer(player);
					}));
				break;
			case "quitType" :
				Debug.Log("Removing player from the game.");
				ScheduleTask(new Task (delegate {
					var player = JsonReader.Deserialize<Player>(e);
					RemovePlayer(player);
					}));
				break;
			default: Debug.Log("Could not recognize input type: " + N["type"].Value); break;
		}
	}

	public void AddPlayers(Player[] players) {
		foreach (Player p in players) {
			AddPlayer(p);
		}
	}

	public void AddPlayer(Player player) {
		GameObject characterPrefab = (GameObject) Resources.Load("Character");
		GameObject characterGO = (GameObject) GameObject.Instantiate(
			characterPrefab, new Vector3(player.location.x, player.location.y, 0), Quaternion.identity);
		characterGO.name = player.name;
	}

	public void RemovePlayer(Player p) {
		Destroy(GameObject.Find(p.name));
	}

	public void UpdateLocation(Player p){
		Debug.Log("setting location to " + p.location.x + "," + p.location.y);
		Transform playerTransform = GameObject.Find(p.name).transform;
		playerTransform.position = new Vector3(p.location.x, p.location.y, 0);
	}

	public void GenerateGrid(int x, int y){
		bool tileIsWhite = true;
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++){
				MakeQuad(new Vector3(i, j, 0) , tileIsWhite, 1);

				tileIsWhite = !tileIsWhite;
			}
		}
		GameObject.Find("Main Camera").transform.Translate(x/2, y/2, 0);
	}

	void MakeQuad(Vector3 center, bool tileIsWhite, int scale) {
		GameObject tilePrefab = (GameObject) Resources.Load("greyTile");
		if (tileIsWhite)
			tilePrefab = (GameObject) Resources.Load("whiteTile");
		//tilePrefab.renderer.material.SetTexture ("_MainTex", tileTexture);\
		tilePrefab.transform.localScale = new Vector3(scale, scale, scale);
		GameObject newEntity = (GameObject) GameObject.Instantiate(
			tilePrefab, center, Quaternion.identity);
		
	}
}
