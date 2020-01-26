using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoDeg.Math
{
	public class Coords
	{
		public static readonly Coords zero = new Coords (0, 0, 0);
		public static readonly Coords up = new Coords (0, 1, 0);
		public static readonly Coords down = new Coords (0, -1, 0);
		public static readonly Coords right = new Coords (1, 0, 0);
		public static readonly Coords left = new Coords (-1, 0, 0);
		public static readonly Coords forward = new Coords (0, 0, 1);
		public static readonly Coords backward = new Coords (0, 0, -1);

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float W { get; set; }

		public Coords (float _X, float _Y)
		{
			X = _X;
			Y = _Y;
			Z = 0;
			W = 0;
		}

		public Coords (float _X, float _Y, float _Z)
		{
			X = _X;
			Y = _Y;
			Z = _Z;
			W = 0;
		}

		public Coords (float _X, float _Y, float _Z, float _W)
		{
			X = _X;
			Y = _Y;
			Z = _Z;
			W = _W;
		}

		public Coords (Vector3 vecpos)
		{
			X = vecpos.x;
			Y = vecpos.y;
			Z = vecpos.z;
			W = 0;
		}


		public Coords Normal ()
		{
			float magnitude = Math.Distance (Coords.zero, this);
			return new Coords (X / magnitude, Y / magnitude, Z / magnitude);
		}

		public static Coords Perp (Coords vector)
		{
			return new Coords (vector.Y, -vector.X);
		}

		public static Coords operator / (Coords a, float value)
		{
			return new Coords (a.X / value, a.Y / value, a.Z / value);
		}

		public static Coords operator * (Coords a, float value)
		{
			return new Coords (a.X * value, a.Y * value, a.Z * value);
		}

		public static Coords operator * (float value, Coords a)
		{
			return new Coords (a.X * value, a.Y * value, a.Z * value);
		}

		public static Coords operator * (Coords a, Coords b)
		{
			return new Coords (a.X * b.X, a.Y * b.Y, a.Z * b.Z);
		}

		public static Coords operator + (Coords a, Coords b)
		{
			return new Coords (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Coords operator - (Coords a, Coords b)
		{
			return new Coords (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static bool operator == (Coords a, Coords b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator != (Coords a, Coords b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Return a vertical matrix. Rows count = 4, column count = 1.
		/// </summary>
		public Matrix ToMatrix ()
		{
			return new Matrix (4, 1, this.ToArray ());
		}

		/// <summary>
		/// Return a horizontal matrix. Rows count = 1, column count = 4.
		/// </summary>
		public Matrix ToHorizontalMatrix ()
		{
			return new Matrix (1, 4, this.ToArray ());
		}

		public Vector3 ToVector ()
		{
			return new Vector3 (X, Y, Z);
		}

		/// <summary>
		/// Return a vertical matrix. Rows count = 4, column count = 1.
		/// </summary>
		public Matrix ToTranslateMatrix ()
		{
			float[] translateValues = {
				1,0,0, X,
				0,1,0, Y,
				0,0,1, Z,
				0,0,0, 1,
			};

			return new Matrix (4, 4, translateValues);
		}

		public float[] ToArray ()
		{
			return new float[] { X, Y, Z, W };
		}

		public override string ToString ()
		{
			return "(" + X + "," + Y + "," + Z + ")";
		}

		public static GameObject DrawLine (Coords startPoint, Coords endPoint, float width, Color colour)
		{
			GameObject line = new GameObject ("Line_" + startPoint.ToString () + "_" + endPoint.ToString ());
			LineRenderer lineRenderer = line.AddComponent<LineRenderer> ();
			lineRenderer.material = new Material (Shader.Find ("Unlit/Color"));
			lineRenderer.material.color = colour;
			lineRenderer.positionCount = 2;
			lineRenderer.SetPosition (0, new Vector3 (startPoint.X, startPoint.Y, startPoint.Z));
			lineRenderer.SetPosition (1, new Vector3 (endPoint.X, endPoint.Y, endPoint.Z));
			lineRenderer.startWidth = width;
			lineRenderer.endWidth = width;
			return line;
		}

		public static GameObject DrawPoint (Coords position, float width, Color colour)
		{
			GameObject line = new GameObject ("Point_" + position.ToString ());
			LineRenderer lineRenderer = line.AddComponent<LineRenderer> ();
			lineRenderer.material = new Material (Shader.Find ("Unlit/Color"));
			lineRenderer.material.color = colour;
			lineRenderer.positionCount = 2;
			lineRenderer.SetPosition (0, new Vector3 (position.X - width / 3.0f, position.Y - width / 3.0f, position.Z));
			lineRenderer.SetPosition (1, new Vector3 (position.X + width / 3.0f, position.Y + width / 3.0f, position.Z));
			lineRenderer.startWidth = width;
			lineRenderer.endWidth = width;
			return line;
		}
	}
}
