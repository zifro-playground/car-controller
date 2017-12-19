using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldAnswere  {

	/*public Answere(){
		this.name = "svara";
		this.inputParameterAmount.Add (1);
		this.inputParameterAmount.Add (2);
		this.hasReturnVariable = false;
		this.pauseWalker = false;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		if (inputParas.Length == 0) {
			PMWrapper.RaiseError ("Kom ihåg att skriva svaret mellan parenteserna i svara()");
		} 
		else if (inputParas.Length == 1) {
			if (inputParas [0].variableType == Compiler.VariableTypes.number)
				GameObject.FindGameObjectWithTag ("Game").GetComponent<LevelAnsweres> ().Answere (inputParas [0].getNumber (), lineNumber);
			else
				PMWrapper.RaiseError ("Svaret på denna fråga är ett tal. Försök igen!");
		} 
		else if (inputParas.Length == 2) {
			if (inputParas [0].variableType == Compiler.VariableTypes.number && inputParas [1].variableType == Compiler.VariableTypes.number)
				GameObject.FindGameObjectWithTag ("Game").GetComponent<LevelAnsweres> ().Answere (inputParas [0].getNumber (), inputParas [1].getNumber (), lineNumber);
			else
				PMWrapper.RaiseError ("Svaret på denna fråga är två tal. Försök igen!");
		}

		return new Compiler.Variable();
	}
	#endregion
	*/
}
