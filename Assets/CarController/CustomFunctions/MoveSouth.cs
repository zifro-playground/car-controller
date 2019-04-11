using Compiler;
using UnityEngine;

public class MoveSouth : Function {

	public MoveSouth(){
		name = "åk_mot_syd";
		buttonText = "åk_mot_syd()";
		inputParameterAmount.Add (0);
		hasReturnVariable = false;
		pauseWalker = true;
	}


	#region implemented abstract members of Function
	public override Variable runFunction (Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().MoveSouth ();

		return new Variable ();
	}
	#endregion
}
