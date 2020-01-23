using LeoDeg.Math;
using UnityEngine;

public static class ExtensionsMethods
{
	public static Coords ToCoords (this Vector3 vector)
	{
		return new Coords (vector.x, vector.y, vector.z);
	}

}
