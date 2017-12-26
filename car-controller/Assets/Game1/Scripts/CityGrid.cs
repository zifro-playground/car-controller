using UnityEngine;

public class CityGrid : MonoBehaviour
{
	private static float xMin;
	private static float yMin;
	private static float zMin;

	public static float DistanceBetweenPoints;

	//public static Vector3[,] Positions = new Vector3[9, 9];


	private void InitBounds()
	{
		Bounds bounds = GetComponent<MeshRenderer>().bounds;
		xMin = bounds.min.x;
		yMin = bounds.min.y;
		zMin = bounds.min.z;

		DistanceBetweenPoints = (bounds.center.x + bounds.extents.x) / 4;

		/* Use GetWorldPosition instead
		for (int i = 0; i < Positions.GetLength(0); i++)
		{
			for (int j = 0; j < Positions.GetLength(0); j++)
			{
				Positions[i, j] = new Vector3(leftBound + i * DistanceBetweenPoints, upperBound - j * DistanceBetweenPoints, -0.3f);
			}
		}
		*/
	}

	public static Vector3 GetWorldPosition(Position position)
	{
		float worldX = xMin + position.x * DistanceBetweenPoints;
		float worldY = yMin + position.y * DistanceBetweenPoints;
		float worldZ = zMin + position.z * DistanceBetweenPoints;

		return new Vector3(worldX, worldY, 0.5f);
	}

	public void Awake()
	{
		InitBounds();
	}
}
