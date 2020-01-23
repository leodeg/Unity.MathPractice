﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LeoDeg.Math
{
	public class Coords
	{
		public float x;
		public float y;
		public float z;

		public Coords (float _X, float _Y)
		{
			x = _X;
			y = _Y;
			z = -1;
		}

		public Coords (float _X, float _Y, float _Z)
		{
			x = _X;
			y = _Y;
			z = _Z;
		}

		public Coords (Vector3 vecpos)
		{
			x = vecpos.x;
			y = vecpos.y;
			z = vecpos.z;
		}

		public static Coords operator * (Coords a, float value)
		{
			return new Coords (a.x * value, a.y * value, a.z * value);
		}

		public static Coords operator / (Coords a, float value)
		{
			return new Coords (a.x / value, a.y / value, a.z / value);
		}

		public static Coords operator * (Coords a, Coords b)
		{
			return new Coords (a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static Coords operator + (Coords a, Coords b)
		{
			return new Coords (a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Coords operator - (Coords a, Coords b)
		{
			return new Coords (a.x - b.x, a.y - b.y, a.z - b.z);
		}

		/// <summary>
		/// Return a perpendicular vector.
		/// </summary>
		public static Coords Perp (Coords vector)
		{
			return new Coords (vector.y, -vector.x);
		}

		public override string ToString ()
		{
			return "(" + x + "," + y + "," + z + ")";
		}

		public Vector3 ToVector ()
		{
			return new Vector3 (x, y, z);
		}

		public static GameObject DrawLine (Coords startPoint, Coords endPoint, float width, Color colour)
		{
			GameObject line = new GameObject ("Line_" + startPoint.ToString () + "_" + endPoint.ToString ());
			LineRenderer lineRenderer = line.AddComponent<LineRenderer> ();
			lineRenderer.material = new Material (Shader.Find ("Unlit/Color"));
			lineRenderer.material.color = colour;
			lineRenderer.positionCount = 2;
			lineRenderer.SetPosition (0, new Vector3 (startPoint.x, startPoint.y, startPoint.z));
			lineRenderer.SetPosition (1, new Vector3 (endPoint.x, endPoint.y, endPoint.z));
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
			lineRenderer.SetPosition (0, new Vector3 (position.x - width / 3.0f, position.y - width / 3.0f, position.z));
			lineRenderer.SetPosition (1, new Vector3 (position.x + width / 3.0f, position.y + width / 3.0f, position.z));
			lineRenderer.startWidth = width;
			lineRenderer.endWidth = width;
			return line;
		}
	}
}
