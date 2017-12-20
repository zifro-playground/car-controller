using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPositionX : Compiler.Function {

	public CheckPositionX(){
		this.name = "kolla_x_läge";
		this.inputParameterAmount.Add (0);
		this.hasReturnVariable = true;
		this.pauseWalker = false;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		int xPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().CheckPositionX ();

		return new Compiler.Variable("x", xPosition);
	}
	#endregion
}
