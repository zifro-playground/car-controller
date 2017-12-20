using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMCaseSwitched
{
	public PlayerMovement Player;
	public LevelAnsweres Answeres;

	public List<GameObject> ChargeStationPrefabs = new List<GameObject>();
	public List<GameObject> ChargeStations = new List<GameObject>();

	public GridPositions Grid;
	private Vector3[,] gridPositions;

	[SuppressMessage("ReSharper", "InvertIf")]
	public void OnPMCompilerStopped(HelloCompiler.StopStatus status)
	{
		Player.resetPlayer();
		if (status == HelloCompiler.StopStatus.Finished)
		{
			if (Player.atChargeStation)
			{
				PMWrapper.RaiseTaskError("Bilen laddades inte. Kom ihåg att ladda när bilen kommer fram till stationen.");
			}
			else
			{
				if (!PMWrapper.levelShouldBeAnswered)
				{
					PMWrapper.RaiseTaskError("Bilen kom inte hela vägen fram.");
				}
			}
		}
	}

	public void OnPMCaseSwitched(int caseNumber)
	{
		Player.resetPlayer();

		gridPositions = Grid.calculatePositions();
		Player.distanceBetweenPoints = Grid.distanceBetweenPoints;

		LoadCurrentTextLevel();
	}


	public void LoadCurrentTextLevel()
	{
		DeleteLastLevel();

		string resourceName = "TextLevels/Level_{0}_{1}";
		resourceName = string.Format(resourceName, PMWrapper.currentLevel, PMWrapper.currentCase);
		TextAsset asset = Resources.Load<TextAsset>(resourceName);

		if (asset == null)
			throw new Exception("Could not find asset \"" + resourceName + "\"");

		string[] rows = asset.text.Split('\n');
		int nextStationToSpawn = 0;

		for (int i = 0; i < 9; i++)
		{
			string[] characters = rows[i].Trim().Split(' ');

			for (int j = 0; j < 9; j++)
			{

				if (characters[j] == "P")
				{
					Player.transform.position = gridPositions[j, i];
					Player.startPosition = Player.transform.position;
					Player.currentPosition = new Vector2(j, 9 - (i + 1));
				}
				else if (characters[j] == "C")
				{
					GameObject station = Instantiate(ChargeStationPrefabs[nextStationToSpawn], new Vector3(gridPositions[j, i].x, gridPositions[j, i].y, -0.1f), Quaternion.Euler(new Vector3(180, 0, 180)));
					station.GetComponent<ChargeStation>().position = new Vector2(j, 9 - (i + 1));
					ChargeStations.Add(station);
					nextStationToSpawn++;
				}
			}
		}
	}

	private void DeleteLastLevel()
	{
		foreach (GameObject obj in ChargeStations)
		{
			Destroy(obj);
		}
		ChargeStations.Clear();
	}

	public double CalculateDistanceToStation(double i)
	{

		int index;
		int.TryParse(i.ToString(CultureInfo.InvariantCulture), out index);

		if (index < 0 || index > ChargeStations.Count - 1)
		{
			PMWrapper.RaiseError("Stationsnummret " + (index + 1) + " finns inte med i denna uppgift. Prova att stoppa in en siffra mellan 0 och " + ChargeStations.Count);
		}

		ChargeStation station = ChargeStations[index].GetComponent<ChargeStation>();

		float distanceX = Mathf.Abs(Player.currentPosition.x - station.position.x);
		float distanceY = Mathf.Abs(Player.currentPosition.y - station.position.y);

		return distanceX + distanceY;
	}
}
