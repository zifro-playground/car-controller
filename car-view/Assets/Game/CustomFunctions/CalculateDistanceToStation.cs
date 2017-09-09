using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDistanceToStation : Compiler.Function {

	public CalculateDistanceToStation(){
		this.name = "räkna_avstånd_till_station";
		this.inputParameterAmount.Add (1);
		this.hasReturnVariable = true;
		this.pauseWalker = false;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		if (inputParas[0].variableType != Compiler.VariableTypes.number)
			PMWrapper.RaiseError ("Funktionen vill ha ett tal för att kunna räkna ut avståndet");

		double totalDistance = GameObject.FindGameObjectWithTag ("Game").GetComponent<GameController> ().calculateDistanceToStation (inputParas[0].getNumber()-1);

		return new Compiler.Variable ("distance", totalDistance);
	}
	#endregion
}
