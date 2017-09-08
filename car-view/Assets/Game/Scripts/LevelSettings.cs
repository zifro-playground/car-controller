using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {

	/*
	 * Things that should or could be set in setLevelXSettings:
	 * 
	 *	- PMWrapper.SetSmartButtons
	 *	- PMWrapper.SetCompilerFunctions
	 *	- PMWrapper.preCode
	 *	- PMWrapper.AddCode
	 *	- PMWrapper.codeRowsLimit
	 *
	 * */

	public void setLevelSettings(int level){
		if (level == 0)
			setLevel0Settings ();
		else if (level == 1)
			setLevel1Settings ();
		else if (level == 2)
			setLevel2Settings ();
		else if (level == 3)
			setLevel3Settings ();
		else if (level == 4)
			setLevel4Settings ();
		else if (level == 5)
			setLevel5Settings ();
		else if (level == 6)
			setLevel6Settings ();
		else if (level == 7)
			setLevel7Settings ();
	}

	public void setLevel0Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_väst()",
			"åk_mot_norr()",
			"åk_mot_syd()",
			"ladda()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveWest(),
			new MoveNorth(),
			new MoveSouth(),
			new Charge()
		});
	}

	public void setLevel1Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_väst()",
			"åk_mot_norr()",
			"åk_mot_syd()",
			"ladda()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveWest(),
			new MoveNorth(),
			new MoveSouth(),
			new Charge()
		});
	}

	public void setLevel2Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_väst()",
			"åk_mot_norr()",
			"åk_mot_syd()",
			"ladda()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveWest(),
			new MoveNorth(),
			new MoveSouth(),
			new Charge()
		});
	}

	public void setLevel3Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_väst()",
			"åk_mot_norr()",
			"åk_mot_syd()",
			"ladda()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveWest(),
			new MoveNorth(),
			new MoveSouth(),
			new Charge()
		});
		PMWrapper.AddCode ("for i in range(6):\n\tåk_mot_öst()\nladda()");
		PMWrapper.codeRowsLimit = 7;
	}

	public void setLevel4Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_väst()",
			"åk_mot_norr()",
			"åk_mot_syd()",
			"ladda()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveWest(),
			new MoveNorth(),
			new MoveSouth(),
			new Charge()
		});
		PMWrapper.AddCode ("for i in range(6):\n\tåk_mot_norr()\nfor i in range(4):\n\tåk_mot_öst()");
		PMWrapper.codeRowsLimit = 20;
	}

	public void setLevel5Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"åk_mot_öst()",
			"åk_mot_väst()",
			"åk_mot_norr()",
			"åk_mot_syd()",
			"ladda()"

		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new MoveEast(),
			new MoveWest(),
			new MoveNorth(),
			new MoveSouth(),
			new Charge()
		});
	}

	public void setLevel6Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"kolla_x_läge()",
			"svara()"

		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CheckPositionX(),
			new Answere()
		});
	}

	public void setLevel7Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"kolla_x_läge()",
			"svara()"

		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CheckPositionX(),
			new Answere()
		});
	}
}
