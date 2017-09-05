using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMLevelChanged {

	public PlayerMovement player;
	public GameObject chargeStation;

	public GridPositions grid;
	private Vector3[,] gridPositions;

	public List<TextAsset> textLevel = new List<TextAsset>();
	private List<GameObject> activeGameObjects = new List<GameObject> ();

	public LevelSettings levelSettings;


	void Start () {		
		PMWrapper.numOfLevels = textLevel.Count;
	}

	public void OnPMCompilerStopped (HelloCompiler.StopStatus status) {
		player.resetPlayer ();
		LoadCurrentLevel();
	}

	public void OnPMLevelChanged () {
		gridPositions = grid.calculatePositions ();
		player.distanceBetweenPoints = grid.distanceBetweenPoints;
		LoadCurrentLevel ();
	}


	public void LoadCurrentLevel() {
		deleteLastLevel ();

		levelSettings.setLevelSettings (PMWrapper.currentLevel);

		string[] rows = textLevel [PMWrapper.currentLevel].text.Split ('\n');

		for (int i = 0; i < 7; i++) {
			string[] characters = rows [i].Trim().Split (' ');

			for (int j = 0; j < 7; j++) {

				if (characters [j] == "P") {
					player.transform.position = gridPositions [j, i];
				} else if (characters [j] == "C") {
					GameObject station = Instantiate (chargeStation, gridPositions [j, i], Quaternion.identity);
					activeGameObjects.Add (station);
				}
			}
		}
	}

	public void deleteLastLevel() {
		foreach (GameObject obj in activeGameObjects) {
			Destroy (obj);
		}
	}
}
