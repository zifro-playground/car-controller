using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWest : Compiler.Function {

	public MoveWest(){
		this.name = "åk_mot_väst";
		this.inputParameterAmount.Add (0);
		this.hasReturnVariable = false;
		this.pauseWalker = true;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().MoveWest ();

		return new Compiler.Variable ();
	}
	#endregion
}
