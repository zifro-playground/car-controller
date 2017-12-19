using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNorth : Compiler.Function {

	public MoveNorth(){
		this.name = "åk_mot_norr";
		this.inputParameterAmount.Add (0);
		this.hasReturnVariable = false;
		this.pauseWalker = true;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().moveNorth ();

		return new Compiler.Variable ();
	}
	#endregion
}
