using Mellis;
using Mellis.Core.Interfaces;
using UnityEngine;

public class Charge : ClrYieldingFunction
{
	public Charge() : base("ladda")
	{
	}

	public override void InvokeEnter(params IScriptType[] arguments)
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Charge();
	}
}
