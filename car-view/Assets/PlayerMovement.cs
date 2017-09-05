using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	#region attributes

	public float distanceBetweenPoints;
	public float playerSpeed = 4;

	private Vector3 lastPosition;

	private bool isMoving;
	private direction currentDirection;

	#endregion


	void Start () {
		resetPlayer ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			isMoving = true;
		}

		if (isMoving) {
			transform.position += transform.up * Time.deltaTime * PMWrapper.speedMultiplier * playerSpeed;

			// Calculate differance in distance without sqrt
			if (Mathf.Pow((transform.position.x - lastPosition.x), 2) + Mathf.Pow((transform.position.y - lastPosition.y), 2) > Mathf.Pow(distanceBetweenPoints, 2)) {

				transform.position = lastPosition + transform.up * distanceBetweenPoints;
				isMoving = false;
				lastPosition = transform.position;
				PMWrapper.UnpauseWalker ();
			}
		}
	}

	public void resetPlayer() {
		isMoving = false;
		currentDirection = direction.north;
		transform.localEulerAngles = new Vector3 (0, 0, 0);
	}

	public void moveEast(){
		lastPosition = transform.position;
		if (currentDirection != direction.east) {
			transform.localEulerAngles = new Vector3 (0, 0, -90);
			currentDirection = direction.east;
		}
		isMoving = true;
	}

	public void moveNorth(){
		lastPosition = transform.position;
		if (currentDirection != direction.north) {
			transform.localEulerAngles = new Vector3 (0, 0, 0);
			currentDirection = direction.north;
		}
		isMoving = true;
	}
}


public enum direction{
	east,
	west,
	north,
	south
}