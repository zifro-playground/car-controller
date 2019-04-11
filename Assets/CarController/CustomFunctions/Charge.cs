using Compiler;
using UnityEngine;

public class Charge : Function {

	public Charge(){
		name = "ladda";
		buttonText = "ladda()";
		inputParameterAmount.Add (0);
		hasReturnVariable = false;
		pauseWalker = false;
	}


	#region implemented abstract members of Function
	public override Variable runFunction (Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().Charge (lineNumber);

		return new Variable ();
	}
	#endregion
}
