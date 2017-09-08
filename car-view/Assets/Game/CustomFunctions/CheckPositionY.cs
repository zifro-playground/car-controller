using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPositionY : Compiler.Function {

	public CheckPositionY(){
		this.name = "kolla_y_läge";
		this.inputParameterAmount.Add (0);
		this.hasReturnVariable = true;
		this.pauseWalker = false;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		int yPosition = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().checkPositionY ();

		return new Compiler.Variable("y", yPosition);
	}
	#endregion
}
