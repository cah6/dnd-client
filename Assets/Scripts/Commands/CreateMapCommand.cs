using System;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using UnityEngine;

public class CreateMapSignal : Signal<MapInfo> { }

public class CreateMapCommand : Command {
	
	[Inject]
	public MapInfo mapInfo { get; set; }

	public override void Execute()
	{
		bool tileIsWhite = true;
		for (int i = 0; i < mapInfo.x; i++) {
			for (int j = 0; j < mapInfo.y; j++){
				MakeQuad(new Vector3(i, j, 0) , tileIsWhite, 1);
				tileIsWhite = !tileIsWhite;
			}
		}
		GameObject.Find("Main Camera").transform.Translate(mapInfo.x/2, mapInfo.y/2, 0);
	}

	private void MakeQuad(Vector3 center, bool tileIsWhite, int scale) {
		GameObject tilePrefab = (GameObject) Resources.Load("greyTile");
		if (tileIsWhite)
			tilePrefab = (GameObject) Resources.Load("whiteTile");
		tilePrefab.transform.localScale = new Vector3(scale, scale, scale);
		GameObject newTile = (GameObject) GameObject.Instantiate(tilePrefab, center, Quaternion.identity);
		newTile.AddComponent<TileView>();
	}
}
