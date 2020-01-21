using UnityEngine;
using System.Collections;
using LeoDeg.Math;

namespace LeoDeg.Math
{
	public class Plane
	{
		Coords start;
		Coords firstPoint;
		Coords secondPoint;

		Coords u;
		Coords v;

		public Plane (Coords start, Coords firstPoint, Coords secondPoint)
		{
			this.start = start;
			this.firstPoint = firstPoint;
			this.secondPoint = secondPoint;

			v = firstPoint - start;
			u = secondPoint - start;
		}

		public Plane (Vector3 startPoint, Vector3 u, Vector3 v)
		{
			this.start = new Coords (startPoint.x, startPoint.y, startPoint.z);
			this.u = new Coords (u.x, u.y, u.z);
			this.v = new Coords (v.x, v.y, v.z);
		}

		public Coords Lerp (float s, float t)
		{
			float xst = start.x + v.x * s + u.x * t;
			float yst = start.y + v.y * s + u.y * t;
			float zst = start.z + v.z * s + u.z * t;
			return new Coords (xst, yst, zst);
		}
	}
}