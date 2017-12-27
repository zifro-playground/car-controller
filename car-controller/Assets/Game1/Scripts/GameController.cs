using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Newtonsoft.Json;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMCaseSwitched
{
	public string gameDataFileName;
	public LevelAnsweres Answeres;

	[Header("Prefabs")]
	public GameObject PlayerPrefab;
	public GameObject ObstaclePrefab;
	public List<GameObject> ChargeStationPrefabs = new List<GameObject>();

	[HideInInspector]
	public List<GameObject> Obstacles = new List<GameObject>();

	[HideInInspector]
	public List<GameObject> ChargeStations = new List<GameObject>();
	
	private Game gameData;
	private PlayerMovement player;
	private GameObject playerObject;

	public void Awake()
	{
		LoadGameData();
	}

	private void LoadGameData()
	{
		TextAsset jsonAsset = Resources.Load<TextAsset>(gameDataFileName);

		if (jsonAsset == null)
			throw new Exception("Could not find the file \"" + gameDataFileName + "\" that should contain game data in json format.");

		string jsonString = jsonAsset.text;

		gameData = JsonConvert.DeserializeObject<Game>(jsonString);
	}

	public void OnPMCompilerStopped(HelloCompiler.StopStatus status)
	{
		if (status == HelloCompiler.StopStatus.Finished)
		{
			if (player.AtChargeStation)
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
		if (player != null)
			player.Reset();
	}

	public void OnPMCaseSwitched(int caseNumber)
	{
		DeleteLastLevel();
		LoadCase(caseNumber);
	}

	private void LoadCase(int caseNumber)
	{
		if (gameData.levels.Count != PMWrapper.numOfLevels)
			Debug.Log("Warning! There are " + gameData.levels.Count + " levels in gameData but " + PMWrapper.numOfLevels + " levels specified in IDE.");

		Case caseData = gameData.levels[PMWrapper.currentLevel].cases[caseNumber];
		CreateAssets(caseData);
	}

	private void CreateAssets(Case caseData)
	{
		foreach (Car car in caseData.cars)
		{
			Vector3 position = CityGrid.GetWorldPosition(car.position);
			playerObject = Instantiate(PlayerPrefab, position, Quaternion.Euler(new Vector3(0, 180, 0)));
			player = playerObject.GetComponent<PlayerMovement>();
			player.CurrentGridPosition = new Vector2(position.x, position.y);
		}

		int nextStationToSpawn = 0;
		foreach (Station station in caseData.stations)
		{
			Vector3 position = CityGrid.GetWorldPosition(station.position);
			GameObject stationObj = Instantiate(ChargeStationPrefabs[nextStationToSpawn], position, Quaternion.Euler(new Vector3(180, 0, 180)));
			stationObj.GetComponent<ChargeStation>().position = new Vector2(position.x, position.y);
			ChargeStations.Add(stationObj);
			nextStationToSpawn++;
		}

		foreach (Obstacled obstacle in caseData.obstacled)
		{
			Vector3 position = CityGrid.GetWorldPosition(obstacle.position);
			GameObject obstacleObj = Instantiate(ObstaclePrefab, position, Quaternion.identity);
			Obstacles.Add(obstacleObj);
		}
	}

	private void DeleteLastLevel()
	{
		Destroy(playerObject);

		foreach (GameObject obj in ChargeStations)
		{
			Destroy(obj);
		}
		ChargeStations.Clear();

		foreach (GameObject obj in Obstacles)
		{
			Destroy(obj);
		}
		Obstacles.Clear();
	}

	// Only used in game 2
	public double CalculateDistanceToStation(double i)
	{

		int index;
		int.TryParse(i.ToString(CultureInfo.InvariantCulture), out index);

		if (index < 0 || index > ChargeStations.Count - 1)
		{
			PMWrapper.RaiseError("Stationsnummret " + (index + 1) + " finns inte med i denna uppgift. Prova att stoppa in en siffra mellan 0 och " + ChargeStations.Count);
		}

		ChargeStation station = ChargeStations[index].GetComponent<ChargeStation>();

		float distanceX = Mathf.Abs(player.CurrentGridPosition.x - station.position.x);
		float distanceY = Mathf.Abs(player.CurrentGridPosition.y - station.position.y);

		return distanceX + distanceY;
	}
}
