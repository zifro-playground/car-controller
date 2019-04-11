using Compiler;
using UnityEngine;

public class MoveNorth : Function {

	public MoveNorth(){
		name = "åk_mot_norr";
		buttonText = "åk_mot_norr()";
		inputParameterAmount.Add (0);
		hasReturnVariable = false;
		pauseWalker = true;
	}


	#region implemented abstract members of Function
	public override Variable runFunction (Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().MoveNorth ();

		return new Variable ();
	}
	#endregion
}
