﻿using System;
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
		[SerializeField] private LineType type;

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

		public Line (Coords startPoint, Coords endPoint)
		{
			this.start = startPoint;
			this.end = startPoint + endPoint;
			this.Direction = endPoint;
			this.type = LineType.Segment;
		}

		public Line (Coords startPoint, Coords endPoint, LineType lineType)
		{
			this.start = startPoint;
			this.end = endPoint;
			this.type = lineType;
			this.Direction = Math.Direction (Start, End);
		}

		/// <summary>
		/// If lines is parallel to each other return float.Nan, otherwise return position at the current line.
		/// </summary>
		public float IntersectAt (Line other)
		{
			if (Math.Dot (Coords.Perp (other.Direction), this.Direction) == 0)
				return float.NaN;

			Coords directionToOtherStart = Math.Direction (this.Start, other.Start);
			float dotToDirectionBetweenStarts = Math.Dot (Coords.Perp (other.Direction), directionToOtherStart);
			float dotToCurrentDirection = Math.Dot (Coords.Perp (other.Direction), Direction);
			float position = dotToDirectionBetweenStarts / dotToCurrentDirection;

			if ((position < 0 || position > 1) && type == LineType.Segment)
				return float.NaN;

			return position;
		}

		public float IntersectAt (Plane plane)
		{
			Coords planeNormal = plane.Normal;
			if (Math.Dot (planeNormal, Direction) == 0)
				return float.NaN;

			Coords directionToPlaneStart = Math.Direction (Start, plane.Start);
			return Math.Dot (planeNormal, directionToPlaneStart) / Math.Dot (planeNormal, this.Direction);
		}

		public Coords Reflect (Coords normal)
		{
			Coords directionNormal = Direction.Normal ();
			float dot = Math.Dot (directionNormal, normal);
			if (dot == 0) return this.Direction;

			return directionNormal - (2.0f * Math.Dot (directionNormal, normal) * normal);
		}

		public GameObject Draw (float width, Color color)
		{
			return Coords.DrawLine (Start, End, width, color);
		}

		/// <summary>
		/// Get point position at a time on the current line.
		/// </summary>
		public Coords Lerp (float time)
		{
			time = ClampTime (time);
			float pointX = Start.X + Direction.X * time;
			float pointY = Start.Y + Direction.Y * time;
			float pointZ = Start.Z + Direction.Z * time;
			return new Coords (pointX, pointY, pointZ);
		}

		private float ClampTime (float time)
		{
			switch (type)
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