using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPositions : MonoBehaviour {

	public Vector3[,] positions = new Vector3[7,7];

	private Vector3 leftBound;
	private Vector3 rightBound;
	private Vector3 upperBound;
	private Vector3 lowerBound;

	private float distanceBetweenPoints;


	void Start () {
		Bounds bounds = GetComponent<MeshRenderer> ().bounds;
		leftBound = bounds.center - bounds.extents;
		print (leftBound);
	}
}
