using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float PlayerSpeed = 4;

	public Vector2 CurrentGridPosition;
	public Vector3 StartPosition;
	private Vector3 lastPosition;

	private bool isMoving;
	private Direction currentDirection;

	public bool AtChargeStation;

	private void OnEnable()
	{
		Reset();
	}

	private void Update()
	{
		if (!isMoving) return;

		transform.position += transform.up * Time.deltaTime * PMWrapper.speedMultiplier * PlayerSpeed;

		// Calculate differance in distance without sqrt
		if (Mathf.Pow(transform.position.x - lastPosition.x, 2) + Mathf.Pow(transform.position.y - lastPosition.y, 2) > Mathf.Pow(CityGrid.DistanceBetweenPoints, 2))
		{
			transform.position = lastPosition + transform.up * CityGrid.DistanceBetweenPoints;
			isMoving = false;
			lastPosition = transform.position;
			PMWrapper.UnpauseWalker();
		}
	}

	public void Reset()
	{
		transform.position = StartPosition;
		isMoving = false;
		currentDirection = Direction.North;
		transform.localEulerAngles = new Vector3(180, 0, 180);
		AtChargeStation = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ChargeStation"))
		{
			AtChargeStation = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("ChargeStation"))
		{
			AtChargeStation = false;
		}
	}


	#region Custom functions called from user
	public void MoveEast()
	{
		lastPosition = transform.position;
		CurrentGridPosition.x += 1;
		if (currentDirection != Direction.East)
		{
			transform.localEulerAngles = new Vector3(180, 0, -90);
			currentDirection = Direction.East;
		}
		isMoving = true;
	}

	public void MoveWest()
	{
		lastPosition = transform.position;
		CurrentGridPosition.x -= 1;
		if (currentDirection != Direction.West)
		{
			transform.localEulerAngles = new Vector3(180, 0, 90);
			currentDirection = Direction.West;
		}
		isMoving = true;
	}

	public void MoveNorth()
	{
		lastPosition = transform.position;
		CurrentGridPosition.y += 1;
		if (currentDirection != Direction.North)
		{
			transform.localEulerAngles = new Vector3(180, 0, 180);
			currentDirection = Direction.North;
		}
		isMoving = true;
	}

	public void MoveSouth()
	{
		lastPosition = transform.position;
		CurrentGridPosition.y -= 1;
		if (currentDirection != Direction.South)
		{
			transform.localEulerAngles = new Vector3(180, 0, 0);
			currentDirection = Direction.South;
		}
		isMoving = true;
	}

	public void Charge(int lineNumber)
	{
		if (AtChargeStation)
		{
			PMWrapper.SetCaseCompleted();
		}
		else
		{
			PMWrapper.RaiseError(lineNumber, "Kan inte ladda här. Se till att köra hela vägen till laddningsstationen.");
		}
	}

	public int CheckPositionX()
	{
		return (int)CurrentGridPosition.x;
	}

	public int CheckPositionY()
	{
		return (int)CurrentGridPosition.y;
	}
	#endregion

}

public enum Direction
{
	East,
	West,
	North,
	South
}
