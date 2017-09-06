using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {

	public void setLevelSettings(int level){
		if (level == 0)
			setLevel0Settings ();
		else if (level == 1)
			setLevel1Settings ();
		else if (level == 2)
			setLevel2Settings ();
	}

	public void setLevel0Settings(){

		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_norr()",
			"ladda()"
		});

		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveNorth(),
			new Charge()
		});

		// Set pre code

		// Set given code
	}

	public void setLevel1Settings(){

		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_norr()",
			"ladda()"
		});

		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveNorth(),
			new Charge()
		});

		// Set pre code

		// Set given code
	}

	public void setLevel2Settings(){

		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_norr()",
			"ladda()"
		});

		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveNorth(),
			new Charge()
		});

		// Set pre code

		// Set given code
	}
}
