using UnityEngine;
using System.Collections;
using WebSocketSharp;
using SimpleJSON;
using System.Collections.Generic;

public delegate void Task();

public class WebsocketView : MonoBehaviour {

	public WebSocket ws;

	private Queue<Task> taskQueue = new Queue<Task>();
	private object queueLock = new object();

	// Use this for initialization
	void Start () {
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
	
	// Update is called once per frame
	void Update () {
		lock (queueLock)
		{
			if (taskQueue.Count > 0)
				taskQueue.Dequeue()();
		}
	}

	public void ScheduleTask(Task newTask)
	{
		lock (queueLock)
		{
			if (taskQueue.Count < 100)
				taskQueue.Enqueue(newTask);
		}
	}

	void OnMouseDown(){
	}

	void parseInput(string e) {
		Debug.Log ("Received: " + e);
		var N = JSON.Parse(e);
		switch (N["type"].Value) {
			case "gridSize" : 
				Debug.Log("Updating grid size!");
				ScheduleTask(new Task (delegate {
					GenerateGrid(N["data"][0].AsInt, N["data"][1].AsInt);
					}));
				break;
			case "playerPosition" :
				Debug.Log("Updating player position!");
				ScheduleTask (new Task (delegate { 
					UpdateLocation(N["player"], N["data"][0].AsInt, N["data"][1].AsInt);
					}));
				break;
			case "players" :
				Debug.Log("Adding existing players to local game!");
				ScheduleTask (new Task (delegate {
					CreateExistingPlayers(N["data"][0].Value);
					}));
				break;
			case "join" :
				Debug.Log("Adding player " + N["player"].Value + " to the game.");
				ScheduleTask(new Task (delegate {
					AddPlayer(N["player"].Value);
					}));
				break;
			case "quit" :
				Debug.Log("Removing player " + N["player"].Value + " from the game.");
				ScheduleTask(new Task (delegate {
					RemovePlayer(N["player"].Value);
					}));
				break;
			default: Debug.Log("Could not recognize input type: " + N["type"].Value); break;
		}
	}

	public void CreateExistingPlayers(string playersToAdd) {
		Debug.Log("Creating players " + playersToAdd);
		AddPlayer(playersToAdd);
	}

	public void AddPlayer(string name) {
		GameObject characterPrefab = (GameObject) Resources.Load("Character");
		GameObject characterGO = (GameObject) GameObject.Instantiate(
			characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		characterGO.name = name;
	}

	public void RemovePlayer(string name) {
		Destroy(GameObject.Find(name));
	}

	public void UpdateLocation(string player, int newX, int newY){
		//TODO: change this from finding players to having a model filled with them
		Debug.Log("setting location to " + newX + "," + newY);
		Transform playerTransform = GameObject.Find(player).transform;
		playerTransform.position = new Vector3(newX, newY, 0);
	}

	public void GenerateGrid(int x, int y){
		bool tileIsWhite = true;
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++){
				MakeQuad(new Vector3(-(x-1)/2 + i, (y-1)/2 - j, 0) , tileIsWhite, 1);

				tileIsWhite = !tileIsWhite;
			}
		}
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
