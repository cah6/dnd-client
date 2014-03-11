using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using JsonFx.Json;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class TileView : View {

	[Inject]
	public WebSocketWrapper ws { get; set; }

	internal void init () {
	}

	/**
	 * When this tile is clicked, send a message to server relaying that it was this client that clicked this tile.
	 */
	void OnMouseDown(){
		Debug.Log ("Clicked tile at position " + transform.position.x + "," + transform.position.y);
		Player p = new Player("playerType", "cah6", new Point((int) transform.position.x, (int) transform.position.y));
		ws.Send(JsonWriter.Serialize(p));
	}
}