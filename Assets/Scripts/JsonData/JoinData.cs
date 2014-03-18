//Class encompassing all the data we need to send when we join a game room.
public class JoinGame {

    public string type { get; set; }
    public string gamename { get; set; }

    public JoinGame(string gamename){
    	type = "joinGameType";
    	this.gamename = gamename;
    }
}