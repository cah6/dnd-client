using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System.Json;

public class TileClick : MonoBehaviour {

	public WebSocket ws;

	JsonObject clickInfo = new JsonObject();
	JsonArray data = new JsonArray();

	/**
	 * On start, get a reference to websocket so we can send messages.
	 */
	void Start () {
		GameObject sc = GameObject.Find ("ServerConnection");
		this.ws = sc.GetComponent<WebsocketView> ().ws;
		
		clickInfo.Add("type", "playerPosition");
		clickInfo.Add("data", data);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		Debug.Log ("Clicked tile at position " + transform.position.x + "," + transform.position.y);
		data.Clear();
		data.Add (transform.position.x);
		data.Add (transform.position.y);
		ws.Send (clickInfo.ToString());
	}
}
