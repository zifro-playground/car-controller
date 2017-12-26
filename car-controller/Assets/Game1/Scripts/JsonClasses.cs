using System.Collections.Generic;

public class Position
{
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }
}

public class Car
{
	public Position position { get; set; }
}

public class Station
{
	public Position position { get; set; }
}

public class Obstacled
{
	public Position position { get; set; }
}

public class Case
{
	public List<Car> cars { get; set; }
	public List<Station> stations { get; set; }
	public List<Obstacled> obstacled { get; set; }
}

public class Level
{
	public string id { get; set; }
	public List<Case> cases { get; set; }
}

public class Game
{
	public List<Level> levels { get; set; }
}
