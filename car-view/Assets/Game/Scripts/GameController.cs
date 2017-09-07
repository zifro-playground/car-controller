using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMLevelChanged {

	public PlayerMovement player;
	public GameObject chargeStation;

	public GridPositions grid;
	private Vector3[,] gridPositions;

	[Space(5)]
	public List<TextAsset> textLevel = new List<TextAsset>();

	private List<GameObject> activeGameObjects = new List<GameObject> ();

	[Space(10)]
	public LevelSettings levelSettings;


	void Start () {		
		PMWrapper.numOfLevels = textLevel.Count;
	}

	public void OnPMCompilerStopped (HelloCompiler.StopStatus status) {
		if (status == HelloCompiler.StopStatus.Finished) {
			if (player.atChargeStation) {
				player.resetPlayer ();
				LoadCurrentLevel ();
				PMWrapper.RaiseError ("Glöm inte att ladda när du kommit fram!");
			} else {
				player.resetPlayer ();
				LoadCurrentLevel ();
				PMWrapper.RaiseError ("Bilen kom inte hela vägen fram. Försök igen!");
			}
			
		} else {
			player.resetPlayer ();
			LoadCurrentLevel ();
		}
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

		for (int i = 0; i < 9; i++) {
			string[] characters = rows [i].Trim().Split (' ');

			for (int j = 0; j < 9; j++) {

				if (characters [j] == "P") {
					player.transform.position = gridPositions [j, i];
					player.currentPosition = new Vector2 (j, 9-(i+1));
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
