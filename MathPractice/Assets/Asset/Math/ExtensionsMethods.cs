using LeoDeg.Math;
using UnityEngine;

public static class ExtensionsMethods
{
	public static Vector ToVector (this Vector3 vector)
	{
		return new Vector (vector.x, vector.y, vector.z);
	}

}
