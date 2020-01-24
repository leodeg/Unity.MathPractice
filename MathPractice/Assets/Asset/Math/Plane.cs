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

		public Coords Normal { get { return Math.Cross (V, U);  } }

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
			float xst = Start.X + V.X * s + U.X * t;
			float yst = Start.Y + V.Y * s + U.Y * t;
			float zst = Start.Z + V.Z * s + U.Z * t;
			return new Coords (xst, yst, zst);
		}
	}
}