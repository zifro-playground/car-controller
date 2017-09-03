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
	public List<Level1Settings> levelSettings = new List<Level1Settings> ();


	void Start () {		
		PMWrapper.numOfLevels = textLevel.Count;
	}

	public void OnPMCompilerStopped (HelloCompiler.StopStatus status) {
		LoadCurrentLevel();
	}

	public void OnPMLevelChanged () {
		gridPositions = grid.calculatePositions ();
		player.distanceBetweenPoints = grid.distanceBetweenPoints;
		LoadCurrentLevel ();
	}


	public void LoadCurrentLevel() {
		levelSettings [PMWrapper.currentLevel].setLevelSettings ();

		string[] rows = textLevel [PMWrapper.currentLevel].text.Split ('\n');

		for (int i = 0; i < 7; i++) {
			string[] characters = rows [i].Trim().Split (' ');

			for (int j = 0; j < 7; j++) {

				if (characters [j] == "P") {
					player.transform.position = gridPositions [j, i];
				} else if (characters [j] == "C") {
					Instantiate (chargeStation, gridPositions [j, i], Quaternion.identity);
				}
			}
		}
	}
}
