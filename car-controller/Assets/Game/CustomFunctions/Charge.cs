using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : Compiler.Function {

	public Charge(){
		this.name = "ladda";
		this.inputParameterAmount.Add (0);
		this.hasReturnVariable = false;
		this.pauseWalker = false;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().charge ();

		return new Compiler.Variable ();
	}
	#endregion
}
