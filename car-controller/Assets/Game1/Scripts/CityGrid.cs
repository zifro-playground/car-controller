/*
 * Place this script on the parent object of all objects that decide the size of the grid. 
 */

using UnityEngine;

public class CityGrid : MonoBehaviour
{
	private static float xMin;
	private static float yMin;
	private static float zMax;

	public static float DistanceBetweenPoints;

	private void InitBounds()
	{
		Bounds bounds = CalculateBoundsInChildren(gameObject);
		xMin = bounds.min.x;
		yMin = bounds.min.y;
		zMax = bounds.max.z;

		DistanceBetweenPoints = (bounds.center.x + bounds.extents.x) / 4;
	}

	public static Vector3 GetWorldPosition(Position position)
	{
		float worldX = xMin + position.x * DistanceBetweenPoints;
		float worldY = yMin + position.y * DistanceBetweenPoints;
		float worldZ = zMax + position.z;

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
		foreach (Renderer renderer in renderers)
		{
			bounds.Encapsulate(renderer.bounds);
		}

		return bounds;
	}
}
