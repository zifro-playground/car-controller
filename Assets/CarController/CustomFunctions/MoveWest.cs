using Compiler;
using UnityEngine;

public class MoveWest : Function {

	public MoveWest(){
		name = "åk_mot_väst";
		buttonText = "åk_mot_väst()";
		inputParameterAmount.Add (0);
		hasReturnVariable = false;
		pauseWalker = true;
	}


	#region implemented abstract members of Function
	public override Variable runFunction (Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().MoveWest ();

		return new Variable ();
	}
	#endregion
}
