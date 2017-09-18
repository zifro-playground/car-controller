using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnsweres : MonoBehaviour {

	public PlayerMovement player;
	public float waitTimeBeforeLevelComplete = 2;

	public bool recievedCorrectAnswere = false;

	public void Answere (double ans){

		#region Check answere against correct answeres
		if (PMWrapper.currentLevel == 6){
			if (ans == 6)
				recievedCorrectAnswere = true;
		}
		
		else if (PMWrapper.currentLevel == 7){
			if (ans.Equals(player.currentPosition.x))
				recievedCorrectAnswere = true;
		}

		else if (PMWrapper.currentLevel == 8){
			if (ans == 5)
				recievedCorrectAnswere = true;
		}

		else if (PMWrapper.currentLevel == 9){
			if (ans == 2)
				recievedCorrectAnswere = true;
		}

		else if (PMWrapper.currentLevel == 12){
			if (ans == 6)
				recievedCorrectAnswere = true;
		}

		else if (PMWrapper.currentLevel == 13){
			if (ans == 7)
				recievedCorrectAnswere = true;
		}

		else if (PMWrapper.currentLevel == 14){
			if (ans == 5)
				recievedCorrectAnswere = true;
		}

		#endregion

		if (recievedCorrectAnswere) {
			print (player.transform.position);
			PMWrapper.ShowGuideBubble (1, "Svar: " + ans);
			StartCoroutine (LevelCompleted());
		}
		else
			PMWrapper.ShowGuideBubble (1, ans + " är tyvärr fel svar. Försök igen!");
	}

	public void Answere (double x, double y){
		if (PMWrapper.currentLevel == 10) {
			if (x == 3 && y == 4) {
				PMWrapper.ShowGuideBubble (1, "Svar: " + x + ", " + y);
				StartCoroutine (LevelCompleted());
			}
			else
				PMWrapper.RaiseError ("Det är tyvärr fel svar. Försök igen!");
		}
		else if (PMWrapper.currentLevel == 11) {
			if (x.Equals (player.currentPosition.x) && y.Equals (player.currentPosition.y)) {
				PMWrapper.ShowGuideBubble (1, "Svar: " + x + ", " + y);
				StartCoroutine (LevelCompleted ());
			}
			else
				PMWrapper.RaiseError ("Det är tyvärr fel svar. Försök igen!");
		}
	}

	public IEnumerator LevelCompleted(){
		yield return new WaitForSeconds (2);
		PMWrapper.SetLevelCompleted ();
	}
}
