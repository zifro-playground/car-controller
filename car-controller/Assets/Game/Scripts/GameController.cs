using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMCaseSwitched{

	public PlayerMovement player;
	public LevelAnsweres answeres;

	public string[] taskDescriptions;
	public List<GameObject> chargeStationPrefabs = new List<GameObject>();
	public List<GameObject> chargeStations = new List<GameObject>();

	public GridPositions grid;
	private Vector3[,] gridPositions;


	void Awake () {		
		PMWrapper.SetTaskDescriptions (taskDescriptions);
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

	public void OnPMCaseSwitched (int caseNumber) {
		gridPositions = grid.calculatePositions ();
		player.distanceBetweenPoints = grid.distanceBetweenPoints;
		LoadCurrentTextLevel ();
		SetAvailableFunctions ();
		SetLevelAnswere ();
	}


	public void LoadCurrentTextLevel() {
		deleteLastLevel ();

		string resourceName = "TextLevels/Level_{0}_{1}";
		resourceName = string.Format (resourceName, PMWrapper.currentLevel, PMWrapper.currentCase);
		TextAsset asset = Resources.Load<TextAsset> (resourceName);

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

	private bool currentLevelShouldBeAnswered(){
		#region return for game 2
		//return true;
		#endregion

		#region return for game 1
		return false;
		#endregion
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

	private void SetAvailableFunctions() {

		int level = PMWrapper.currentLevel;

		#region functions for game 1
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveWest(),
			new MoveNorth(),
			new MoveSouth(),
			new Charge()
		});
		#endregion

		#region functions for game 2
		/*if (level == 0 || level == 1 || level == 2 || level == 3) {
			PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
				new CheckPositionX (),
				new AnswereFunction()
			});
		}

		if (level == 4 || level == 5 || level == 6) {
			PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
				new CheckPositionX (),
				new CheckPositionY (),
				new AnswereFunction()
			});
		}

		if (level == 7 || level == 8) {
			PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
				new CalculateDistanceToStation(),
				new AnswereFunction()
			});
		}*/
		#endregion
	}

	private void SetLevelAnswere () {

		int level = PMWrapper.currentLevel;
		int caseNumber = PMWrapper.currentCase;

		#region answeres for game 2
		/*if (level == 0) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "6" });
		}

		if (level == 1) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "1" });
			if (caseNumber == 1)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "3" });
		}

		if (level == 2) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "5" });
			if (caseNumber == 1)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "4" });
		}

		if (level == 3) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "2" });
		}

		if (level == 4) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[2] { "3", "4" });
		}

		if (level == 5) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[2] { "7", "5" });
			if (caseNumber == 1)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[2] { "5", "6" });
		}

		if (level == 6) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "6" });
			if (caseNumber == 1)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "4" });
		}

		if (level == 7) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "7" });
			if (caseNumber == 1)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "4" });
		}

		if (level == 8) {
			if (caseNumber == 0)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "5" });
			if (caseNumber == 1)
				PMWrapper.SetCurrentLevelAnswere (Compiler.VariableTypes.number, new string[1] { "7" });
		}*/
		#endregion
	}
}
