using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace LeoDeg.Math
{
	public class Line
	{
		private Coords start;
		private Coords end;

		/// <summary>
		/// Start point of the line.
		/// </summary>
		public Coords Start
		{
			get { return start; }
			set 
			{ 
				start = value;
				Vector = Math.Direction (Start, End);
			}
		}

		/// <summary>
		/// End point of the line.
		/// </summary>
		public Coords End 
		{ 
			get { return end; }
			set
			{
				end = value;
				Vector = Math.Direction (Start, End);
			}
		}

		/// <summary>
		/// Direction from the start point to the end point.
		/// </summary>
		public Coords Vector { get; private set; }

		public enum LineType { Line, Segment, Ray }
		[SerializeField] private LineType lineType;

		public Line (Coords pointA, Coords pointB)
		{
			this.Start = pointA;
			this.End = pointB;
			Vector = Math.Direction (Start, End);
		}

		public Line (Coords pointA, Coords pointB, LineType lineType) : this (pointA, pointB)
		{
			this.lineType = lineType;
		}

		public Coords GetPointAt (float time)
		{
			time = TimeClamp (time);
			float pointX = Start.x + Vector.x * time;
			float pointY = Start.y + Vector.y * time;
			float pointZ = Start.z + Vector.z * time;
			return new Coords (pointX, pointY, pointZ);
		}

		private float TimeClamp (float time)
		{
			switch (lineType)
			{
				case LineType.Line:
					return time;
				case LineType.Segment:
					if (time < 0) return 0;
					else if (time > 1) return 1;
					return time;

				case LineType.Ray:
					if (time < 0) return 0;
					return time;
			}

			throw new ArgumentOutOfRangeException ("Line::Error::Invalid Line type.");
		}
	}
}