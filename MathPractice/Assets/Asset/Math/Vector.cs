using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoDeg.Math
{
	public class Vector
	{
		public static readonly Vector zero = new Vector (0, 0, 0);
		public static readonly Vector up = new Vector (0, 1, 0);
		public static readonly Vector down = new Vector (0, -1, 0);
		public static readonly Vector right = new Vector (1, 0, 0);
		public static readonly Vector left = new Vector (-1, 0, 0);
		public static readonly Vector forward = new Vector (0, 0, 1);
		public static readonly Vector backward = new Vector (0, 0, -1);

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float W { get; set; }

		public Vector (float _X, float _Y)
		{
			X = _X;
			Y = _Y;
			Z = 0;
			W = 0;
		}

		public Vector (float _X, float _Y, float _Z)
		{
			X = _X;
			Y = _Y;
			Z = _Z;
			W = 0;
		}

		public Vector (float _X, float _Y, float _Z, float _W)
		{
			X = _X;
			Y = _Y;
			Z = _Z;
			W = _W;
		}

		public Vector (Vector3 vecpos)
		{
			X = vecpos.x;
			Y = vecpos.y;
			Z = vecpos.z;
			W = 0;
		}


		public Vector Normal ()
		{
			float magnitude = Math.Distance (Vector.zero, this);
			float multiplier = 1.0f / magnitude;
			return new Vector (X * multiplier, Y * multiplier, Z * multiplier);
		}

		public static Vector Perp (Vector vector)
		{
			return new Vector (vector.Y, -vector.X);
		}

		public static Vector operator / (Vector a, float value)
		{
			float multiplier = 1.0f / value;
			return new Vector (a.X * multiplier, a.Y * multiplier, a.Z * multiplier);
		}

		public static Vector operator * (Vector a, float value)
		{
			return new Vector (a.X * value, a.Y * value, a.Z * value);
		}

		public static Vector operator * (float value, Vector a)
		{
			return new Vector (a.X * value, a.Y * value, a.Z * value);
		}

		public static Vector operator * (Vector a, Vector b)
		{
			return new Vector (a.X * b.X, a.Y * b.Y, a.Z * b.Z);
		}

		public static Vector operator + (Vector a, Vector b)
		{
			return new Vector (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Vector operator - (Vector a, Vector b)
		{
			return new Vector (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static bool operator == (Vector a, Vector b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator != (Vector a, Vector b)
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

		public Vector3 ToVector3 ()
		{
			return new Vector3 (X, Y, Z);
		}

		/// <summary>
		/// Return a new vector with converted values from degrees to radians.
		/// </summary>
		public Vector Deg2Rad ()
		{
			return new Vector (X * Mathf.Deg2Rad, Y * Mathf.Deg2Rad, Z * Mathf.Deg2Rad);
		}

		/// <summary>
		/// Return a translate matrix. Rows count = 4, column count = 4.
		/// </summary>
		public Matrix ToTranslationMatrix ()
		{
			float[] translateValues = {
				1,0,0, X,
				0,1,0, Y,
				0,0,1, Z,
				0,0,0, 1,
			};

			return new Matrix (4, 4, translateValues);
		}

		/// <summary>
		/// Return a scale matrix. Rows count = 4, column count = 4.
		/// </summary>
		public Matrix ToScaleMatrix ()
		{
			float[] scaleValues = {
				X,0,0, 0,
				0,Y,0, 0,
				0,0,Z, 0,
				0,0,0, 1,
			};

			return new Matrix (4, 4, scaleValues);
		}

		public float[] ToArray ()
		{
			return new float[] { X, Y, Z, W };
		}

		public override string ToString ()
		{
			return "(" + X + "," + Y + "," + Z + ")";
		}

		public static GameObject DrawLine (Vector startPoint, Vector endPoint, float width, Color colour)
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

		public static GameObject DrawPoint (Vector position, float width, Color colour)
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
