using UnityEngine;

public class CityGrid : MonoBehaviour, IPMCaseSwitched
{
	public static Vector3[,] Positions = new Vector3[9, 9];

	public static float DistanceBetweenPoints;


	private void CalculatePositions()
	{
		Bounds bounds = GetComponent<MeshRenderer>().bounds;
		float leftBound = bounds.center.x - bounds.extents.x;
		float upperBound = bounds.center.y + bounds.extents.y;

		DistanceBetweenPoints = (bounds.center.x + bounds.extents.x) / 4;

		for (int i = 0; i < Positions.GetLength(0); i++)
		{
			for (int j = 0; j < Positions.GetLength(0); j++)
			{
				Positions[i, j] = new Vector3(leftBound + i * DistanceBetweenPoints, upperBound - j * DistanceBetweenPoints, -0.3f);
			}
		}
	}

	public void OnPMCaseSwitched(int caseNumber)
	{
		CalculatePositions();
	}
}
