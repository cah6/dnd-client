//Class encapsulating all data needed when you first join a server.
public class StartInfo {
    public string type { get; set;}
    public Point gridSize { get; set;}
    public Player[] players { get; set;}
}

//Helper Point class to serialize / deserialize a point easily.
public class Point {
	public int x { get; set; }
	public int y { get; set; }

	public Point(){

	}

	public Point(int x, int y){
		this.x = x;
		this.y = y;
	}
}

//Encapsulates all data user needs to know about another player.
public class Player {

	public string type { get; set; }
	public string name { get; set; }
	public Point location {get; set; }

	public Player(){
		
	}

	public Player(string type, string name, Point location){
		this.type = type;
		this.name = name;
		this.location = location;
	}

}