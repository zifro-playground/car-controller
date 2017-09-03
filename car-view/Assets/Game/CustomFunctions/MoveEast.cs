using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEast : Compiler.Function {

	public MoveEast(){
		this.name = "åk_mot_öst";
		this.inputParameterAmount.Add (0);
		this.hasReturnVariable = false;
		this.pauseWalker = true;
	}


	#region implemented abstract members of Function
	public override Compiler.Variable runFunction (Compiler.Scope currentScope, Compiler.Variable[] inputParas, int lineNumber)
	{
		throw new System.NotImplementedException ();
	}
	#endregion
}
