using UnityEngine;
using System.Collections;
using LeoDeg.Math;

namespace LeoDeg.Math
{
	public class Plane
	{
		public Vector Start { get; private set; }
		public Vector FirstVector { get; private set; }
		public Vector SecondVector { get; private set; }

		public Vector U { get; private set; }
		public Vector V { get; private set; }

		public Vector Normal { get { return Math.Cross (V, U);  } }

		public Plane (Vector start, Vector firstPoint, Vector secondPoint)
		{
			this.Start = start;
			this.FirstVector = firstPoint;
			this.SecondVector = secondPoint;

			V = firstPoint - start;
			U = secondPoint - start;
		}

		public Plane (Vector3 startPoint, Vector3 u, Vector3 v)
		{
			this.Start = new Vector (startPoint.x, startPoint.y, startPoint.z);
			this.U = new Vector (u.x, u.y, u.z);
			this.V = new Vector (v.x, v.y, v.z);
		}

		public Vector Lerp (float s, float t)
		{
			float xst = Start.X + V.X * s + U.X * t;
			float yst = Start.Y + V.Y * s + U.Y * t;
			float zst = Start.Z + V.Z * s + U.Z * t;
			return new Vector (xst, yst, zst);
		}
	}
}