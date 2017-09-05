using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {

	public void setLevelSettings(int level){
		if (level == 0)
			setLevel1Settings ();
	}

	public void setLevel1Settings(){

		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_norr()"
		});

		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveNorth()
		});

		// Set pre code

		// Set given code
	}

	public void setLevel2Settings(){

		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_norr()"
		});

		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveNorth()
		});

		// Set pre code

		// Set given code
	}
}
