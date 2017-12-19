using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMCaseSwitched{

	public PlayerMovement player;
	public LevelAnsweres answeres;
	
	public List<GameObject> chargeStationPrefabs = new List<GameObject>();
	public List<GameObject> chargeStations = new List<GameObject>();

	public GridPositions grid;
	private Vector3[,] gridPositions;

	public void OnPMCompilerStopped (HelloCompiler.StopStatus status) {
		if (status == HelloCompiler.StopStatus.Finished) {
			if (player.atChargeStation) {
				//player.resetPlayer ();
				PMWrapper.RaiseTaskError ("Bilen laddades inte. Kom ihåg att ladda när bilen kommer fram till stationen.");
			} else {
				if (!PMWrapper.levelShouldBeAnswered) {
					//player.resetPlayer ();
					PMWrapper.RaiseTaskError ("Bilen kom inte hela vägen fram.");
				}
			}
			
		} else {
			//player.resetPlayer ();
		}
	}

	public void OnPMCaseSwitched (int caseNumber) {
		player.resetPlayer();

		gridPositions = grid.calculatePositions ();
		player.distanceBetweenPoints = grid.distanceBetweenPoints;

		LoadCurrentTextLevel ();
	}


	public void LoadCurrentTextLevel() {
		deleteLastLevel ();

		string resourceName = "TextLevels/Level_{0}_{1}";
		resourceName = string.Format (resourceName, PMWrapper.currentLevel, PMWrapper.currentCase);
		TextAsset asset = Resources.Load<TextAsset> (resourceName);

		if (asset == null)
			throw new Exception ("Could not find asset \"" + resourceName + "\"");

		string[] rows = asset.text.Split ('\n');
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

	private void deleteLastLevel() {
		foreach (GameObject obj in chargeStations) {
			Destroy (obj);
		}
		chargeStations.Clear ();
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
