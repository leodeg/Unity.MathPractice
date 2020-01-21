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
		public enum LineType { Line, Segment, Ray }
		[SerializeField] private LineType lineType;
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
				Direction = Math.Direction (Start, End);
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
				Direction = Math.Direction (Start, End);
			}
		}

		/// <summary>
		/// Direction from the start point to the end point.
		/// </summary>
		public Coords Direction { get; private set; }

		public Line (Coords startPoint, Coords lineDirection)
		{
			Start = startPoint;
			End = startPoint + lineDirection;
			Direction = lineDirection;
		}

		public Line (Coords start, Coords end, LineType lineType)
		{
			Start = start;
			End = end;
			this.lineType = lineType;
			Direction = Math.Direction (Start, End);
		}

		/// <summary>
		/// Get point position at a time on the current line.
		/// </summary>
		public Coords Lerp (float time)
		{
			time = ClampTime (time);
			float pointX = Start.x + Direction.x * time;
			float pointY = Start.y + Direction.y * time;
			float pointZ = Start.z + Direction.z * time;
			return new Coords (pointX, pointY, pointZ);
		}

		private float ClampTime (float time)
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

				default:
					throw new ArgumentOutOfRangeException ("Line::Error::Invalid line type.");
			}
		}
	}
}