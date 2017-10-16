using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMLevelChanged {

	public PlayerMovement player;
	public LevelAnsweres answeres;

	public string[] taskDescriptions;
	public List<GameObject> chargeStationPrefabs = new List<GameObject>();
	public List<GameObject> chargeStations = new List<GameObject>();

	public GridPositions grid;
	private Vector3[,] gridPositions;

	[Space(5)]
	public List<TextAsset> textLevel = new List<TextAsset>();

	[Space(10)]
	public LevelSettings levelSettings;


	void Awake () {		
		PMWrapper.SetTaskDescriptions (taskDescriptions);
	}

	void Start(){
		PMWrapper.numOfLevels = textLevel.Count;
	}

	public void OnPMCompilerStopped (HelloCompiler.StopStatus status) {
		if (status == HelloCompiler.StopStatus.Finished) {
			if (player.atChargeStation) {
				player.resetPlayer ();
				PMWrapper.RaiseError ("Glöm inte att ladda när du kommit fram!");
			} else {
				if (!currentLevelShouldBeAnswered()) {
					player.resetPlayer ();
					PMWrapper.RaiseError ("Bilen kom inte hela vägen fram. Försök igen!");
				}
			}
			
		} else {
			player.resetPlayer ();
		}
	}

	public void OnPMLevelChanged () {
		gridPositions = grid.calculatePositions ();
		player.distanceBetweenPoints = grid.distanceBetweenPoints;
		LoadCurrentLevel ();
	}


	public void LoadCurrentLevel() {
		deleteLastLevel ();

		//levelSettings.setLevelSettings (PMWrapper.currentLevel);

		string[] rows = textLevel [PMWrapper.currentLevel].text.Split ('\n');
		int nextStationToSpawn = 0;

		for (int i = 0; i < 9; i++) {
			string[] characters = rows [i].Trim().Split (' ');

			for (int j = 0; j < 9; j++) {

				if (characters [j] == "P") {
					player.transform.position = gridPositions [j, i];
					player.startPosition = player.transform.position;
					player.currentPosition = new Vector2 (j, 9-(i+1));
				} else if (characters [j] == "C") {
					GameObject station = Instantiate (chargeStationPrefabs[nextStationToSpawn], new Vector3(gridPositions [j, i].x, gridPositions [j, i].y, -0.1f), Quaternion.Euler(new Vector3(180,0,180)));
					station.GetComponent<ChargeStation> ().position = new Vector2(j, 9-(i+1));
					chargeStations.Add (station);
					nextStationToSpawn++;
				}
			}
		}
	}

	public void deleteLastLevel() {
		foreach (GameObject obj in chargeStations) {
			Destroy (obj);
		}
		chargeStations.Clear ();
		PMWrapper.preCode = "";
	}

	public bool currentLevelShouldBeAnswered(){
		if (PMWrapper.currentLevel == 6 || PMWrapper.currentLevel == 7 || PMWrapper.currentLevel == 8 || PMWrapper.currentLevel == 9 || PMWrapper.currentLevel == 10 
			|| PMWrapper.currentLevel == 11 || PMWrapper.currentLevel == 12 || PMWrapper.currentLevel == 13 || PMWrapper.currentLevel == 14)
			return true;
		return false;
	}

	public double calculateDistanceToStation (double i){

		int index;
		int.TryParse(i.ToString(), out index);

		if (index < 0 || index > chargeStations.Count - 1) {
			PMWrapper.RaiseError ("Stationsnummret " + (index + 1) + " finns inte med i denna uppgift. Prova att stoppa in en siffra mellan 0 och " + chargeStations.Count); 
		}

		ChargeStation station = chargeStations [index].GetComponent<ChargeStation> ();

		float distanceX = Mathf.Abs (player.currentPosition.x - station.position.x);
		float distanceY = Mathf.Abs (player.currentPosition.y - station.position.y);

		return (double)(distanceX + distanceY);
	}
}
