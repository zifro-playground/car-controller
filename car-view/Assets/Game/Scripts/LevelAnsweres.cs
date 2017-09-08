using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnsweres : MonoBehaviour {

	public PlayerMovement player;

	public bool recievedCorrectAnswere = false;

	public void Answere (double ans){

		#region Check answere against correct answeres
		if (PMWrapper.currentLevel == 6)
		if (ans == 6)
			recievedCorrectAnswere = true;
		
		else if (PMWrapper.currentLevel == 7)
		if (ans.Equals(player.currentPosition.x))
			recievedCorrectAnswere = true;

		else if (PMWrapper.currentLevel == 8)
		if (ans.Equals(player.currentPosition.x))
			//recievedCorrectAnswere = true;

		#endregion

		if (recievedCorrectAnswere)
			PMWrapper.SetLevelCompleted ();
		else
			PMWrapper.RaiseError (ans + " är tyvärr fel svar. Försök igen!");
	}

	public void Answere (double x, double y){

	}
}
