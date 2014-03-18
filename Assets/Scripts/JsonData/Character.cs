//Helper class to easily serialize / deserialize character data.
public class Character {

	public string type { get; set; }

	public string name { get; set; }
	public string className { get; set; }
	public string race {get; set; }

	public Character(){

	}

	public Character(string name, string className, string race){
		this.type = "createCharacterType";
		this.name = name;
		this.className = className;
		this.race = race;
	}
}