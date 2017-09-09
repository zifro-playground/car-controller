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
		else if (level == 8)
			setLevel8Settings ();
		else if (level == 9)
			setLevel9Settings ();
		else if (level == 10)
			setLevel10Settings ();
		else if (level == 11)
			setLevel11Settings ();
		else if (level == 12)
			setLevel12Settings ();
		else if (level == 13)
			setLevel13Settings ();
		else if (level == 14)
			setLevel14Settings ();
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
		//PMWrapper.mainCode = "for i in range(6):\n\tåk_mot_öst()ladda()";
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
		//PMWrapper.mainCode = "for i in range(6):\n\tåk_mot_norr()\nfor i in range(4):\n\tåk_mot_öst()";
		PMWrapper.codeRowsLimit = 7;
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
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new Answere()
		});
		PMWrapper.preCode = "station_x = 6";
		PMWrapper.codeRowsLimit = 20;
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
		PMWrapper.mainCode = "bil_x = kolla_x_läge()\n\nsvara(bil_x)";
	}

	public void setLevel8Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"kolla_x_läge()",
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CheckPositionX(),
			new Answere()
		});
		PMWrapper.preCode = "station_x = 6";
	}

	public void setLevel9Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"kolla_x_läge()",
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CheckPositionX(),
			new Answere()
		});
		PMWrapper.preCode = "station1_x = 1\nstation2_x = 6";
	}

	public void setLevel10Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"kolla_x_läge()",
			"kolla_y_läge()",
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CheckPositionX(),
			new CheckPositionY(),
			new Answere()
		});
		PMWrapper.preCode = "station_x = 3\nstation_y = 4";
		PMWrapper.mainCode = "\nsvara(station_x, station_y)";
	}

	public void setLevel11Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"kolla_x_läge()",
			"kolla_y_läge()",
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CheckPositionX(),
			new CheckPositionY(),
			new Answere()
		});
		PMWrapper.mainCode = "\nbil_x = kolla_x_läge()";
	}

	public void setLevel12Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"kolla_x_läge()",
			"kolla_y_läge()",
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CheckPositionX(),
			new CheckPositionY(),
			new Answere()
		});
		PMWrapper.preCode = "station_x = 3\nstation_y = 5";
		PMWrapper.mainCode = "\nbil_x = kolla_x_läge()\navstånd_x = station_x - bil_x";
	}

	public void setLevel13Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"räkna_avstånd_till_station()",
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CalculateDistanceToStation(),
			new Answere()
		});
		PMWrapper.mainCode = "avstånd_1 = räkna_avstånd_till_station()";
	}

	public void setLevel14Settings(){
		PMWrapper.SetSmartButtons(new string[] {
			"räkna_avstånd_till_station()",
			"svara()"
		});
		PMWrapper.SetCompilerFunctions (new Compiler.Function[] {
			new CalculateDistanceToStation(),
			new Answere()
		});
	}
}
