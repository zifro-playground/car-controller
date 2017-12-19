using UnityEngine;

public class Danger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			print("Player crached");
		}
	}
}
