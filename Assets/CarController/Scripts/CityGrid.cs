/*
 * Place this script on the parent object of all objects that decide the size of the grid. 
 */

using UnityEngine;

public class CityGrid : MonoBehaviour
{
	[Header("Grid dimensions in number of houses")]
	public int N = 9;
	public int M = 8;

	[Space()]
	public float Padding = 0.5f;

	private static float xMin;
	private static float yMin;
	private static float zMax;

	public static float DistanceBetweenPoints;

	private void InitBounds()
	{
		Bounds bounds = CalculateBoundsInChildren(gameObject);
		xMin = bounds.min.x - Padding;
		yMin = bounds.min.y - Padding;
		zMax = bounds.max.z;

		DistanceBetweenPoints = (bounds.size.x + 2 * Padding) / N;
	}

	public static Vector3 GetWorldPosition(Position position)
	{
		float worldX = xMin + position.x * DistanceBetweenPoints;
		float worldY = yMin + position.y * DistanceBetweenPoints;
        float worldZ = -0.01f;//zMax + position.z;

		return new Vector3(worldX, worldY, worldZ);
	}

	public void Awake()
	{
		InitBounds();
	}

	private Bounds CalculateBoundsInChildren(GameObject obj)
	{
		Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

		if (renderers.Length == 0)
			throw new System.Exception("Could not find any renderers in children of gameobject \"" + obj.name + "\".");

		Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);
		foreach (Renderer rend in renderers)
		{
			bounds.Encapsulate(rend.bounds);
		}

		return bounds;
	}
}
