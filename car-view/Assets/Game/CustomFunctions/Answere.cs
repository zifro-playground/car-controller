using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answere : Compiler.Function {

	public Answere(){
		this.name = "svara";
		this.inputParameterAmount.Add (1);
		this.inputParameterAmount.Add (2);
		this.hasReturnVariable = false;
		this.pauseWalker = false;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Game").GetComponent<LevelAnsweres> ().Answere (inputParas [0].getNumber());
		return new Compiler.Variable();
	}
	#endregion
}
