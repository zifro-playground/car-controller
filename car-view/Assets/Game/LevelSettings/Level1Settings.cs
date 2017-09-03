using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Settings : MonoBehaviour {

	public void setLevelSettings(){
		setGameFunctions ();
		setPreCode ();
		setCode ();
	}

	private void setGameFunctions(){
		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_norr()"
		});
	}

	// Set the pre code that is not editable for the user.
	private void setPreCode(){

	}

	// Set the code that should be given to the user at start.
	private void setCode(){

	}
}
