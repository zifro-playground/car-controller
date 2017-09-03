using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	#region attributes

	public float distanceBetweenPoints;

	private Vector3 lastPosition;

	private bool isMoving;
	private direction currentDirection;

	#endregion


	void Start () {
		isMoving = false;
		currentDirection = direction.east;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			lastPosition = transform.position;
			isMoving = true;
		}

		if (isMoving) {
			transform.position += transform.up * Time.deltaTime;

			// Calculate differance in distance without sqrt
			if (Mathf.Pow((transform.position.x - lastPosition.x), 2) + Mathf.Pow((transform.position.y - lastPosition.y), 2) > Mathf.Pow(distanceBetweenPoints, 2)) {

				transform.position = lastPosition + transform.up * distanceBetweenPoints;
				isMoving = false;
				lastPosition = transform.position;
			}
		}
	}
}


public enum direction{
	east,
	west,
	north,
	south
}