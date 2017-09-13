using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	#region attributes

	public float distanceBetweenPoints;
	public float playerSpeed = 4;

	public Vector2 currentPosition;
	public Vector3 startPosition;
	private Vector3 lastPosition;

	private bool isMoving;
	private direction currentDirection;

	public bool atChargeStation;

	#endregion


	void Start () {
		resetPlayer ();
	}

	void Update () {
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
		transform.position = startPosition;
		isMoving = false;
		currentDirection = direction.north;
		transform.localEulerAngles = new Vector3 (180, 0, 0);
		atChargeStation = false;
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("ChargeStation")) {
			atChargeStation = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("ChargeStation")) {
			atChargeStation = false;
		}
	}


	#region Custom functions called from user
	public void moveEast(){
		lastPosition = transform.position;
		currentPosition.x += 1;
		if (currentDirection != direction.east) {
			transform.localEulerAngles = new Vector3 (180, 0, -90);
			currentDirection = direction.east;
		}
		isMoving = true;
	}

	public void moveWest(){
		lastPosition = transform.position;
		currentPosition.x -= 1;
		if (currentDirection != direction.west) {
			transform.localEulerAngles = new Vector3 (180, 0, 90);
			currentDirection = direction.west;
		}
		isMoving = true;
	}

	public void moveNorth(){
		lastPosition = transform.position;
		currentPosition.y += 1;
		if (currentDirection != direction.north) {
			transform.localEulerAngles = new Vector3 (180, 0, 0);
			currentDirection = direction.north;
		}
		isMoving = true;
	}

	public void moveSouth(){
		lastPosition = transform.position;
		currentPosition.y -= 1;
		if (currentDirection != direction.south) {
			transform.localEulerAngles = new Vector3 (180, 0, 180);
			currentDirection = direction.south;
		}
		isMoving = true;
	}

	public void charge(){
		if (atChargeStation) {
			//PMWrapper.RaiseError (transform.position, "Bra jobbat! Bilen hittade fram.");
			PMWrapper.SetLevelCompleted ();
		} else {
			PMWrapper.RaiseError (transform.position, "Det går inte att ladda här. Försök igen!");
		}
	}

	public int checkPositionX(){
		return (int)currentPosition.x;
	}

	public int checkPositionY(){
		return (int)currentPosition.y;
	}
	#endregion

}

public enum direction{
	east,
	west,
	north,
	south
}
