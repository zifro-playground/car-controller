using Compiler;
using UnityEngine;

public class MoveEast : Function {

	public MoveEast(){
		name = "åk_mot_öst";
		buttonText = "åk_mot_öst()";
		inputParameterAmount.Add (0);
		hasReturnVariable = false;
		pauseWalker = true;
	}


	#region implemented abstract members of Function
	public override Variable runFunction (Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().MoveEast ();

		return new Variable ();
	}
	#endregion
}
