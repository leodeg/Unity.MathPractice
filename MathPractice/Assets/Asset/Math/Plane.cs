using UnityEngine;
using System.Collections;
using LeoDeg.Math;

namespace LeoDeg.Math
{
	public class Plane
	{
		public Coords Start { get; private set; }
		public Coords FirstVector { get; private set; }
		public Coords SecondVector { get; private set; }

		public Coords U { get; private set; }
		public Coords V { get; private set; }

		public Coords Normal { get { return Math.Cross (U, V);  } }

		public Plane (Coords start, Coords firstPoint, Coords secondPoint)
		{
			this.Start = start;
			this.FirstVector = firstPoint;
			this.SecondVector = secondPoint;

			V = firstPoint - start;
			U = secondPoint - start;
		}

		public Plane (Vector3 startPoint, Vector3 u, Vector3 v)
		{
			this.Start = new Coords (startPoint.x, startPoint.y, startPoint.z);
			this.U = new Coords (u.x, u.y, u.z);
			this.V = new Coords (v.x, v.y, v.z);
		}

		public Coords Lerp (float s, float t)
		{
			float xst = Start.x + V.x * s + U.x * t;
			float yst = Start.y + V.y * s + U.y * t;
			float zst = Start.z + V.z * s + U.z * t;
			return new Coords (xst, yst, zst);
		}
	}
}