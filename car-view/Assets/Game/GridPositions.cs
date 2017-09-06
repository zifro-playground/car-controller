using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPositions : MonoBehaviour {

	private Vector3[,] positions = new Vector3[9,9];

	public float distanceBetweenPoints;

	private float leftBound;
	private float upperBound;


	public Vector3[,] calculatePositions() {

		Bounds bounds = GetComponent<MeshRenderer> ().bounds;
		leftBound = bounds.center.x - bounds.extents.x;
		upperBound = bounds.center.y + bounds.extents.y;
		
		distanceBetweenPoints = (bounds.center.x + bounds.extents.x) / 4;

		for (int i = 0; i < positions.GetLength(0)-1; i++) {
			for (int j = 0; j < positions.GetLength(0)-1; j++) {
				positions [i, j] = new Vector3 (leftBound + i * distanceBetweenPoints, upperBound - j * distanceBetweenPoints);
			}
		}
		return positions;
	}
}
