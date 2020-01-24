﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoDeg.Math
{
	public class Math
	{
		static public Coords GetNormal (Coords vector)
		{
			float length = Distance (vector, new Coords (0, 0, 0));
			vector.X /= length;
			vector.Y /= length;
			vector.Z /= length;

			return vector;
		}

		static public float Distance (Coords start, Coords target)
		{
			float diffSquared = Square (target.X - start.X) +
								Square (target.Y - start.Y) +
								Square (target.Z - start.Z);
			return Mathf.Sqrt (diffSquared);

		}

		static public Coords Direction (Coords current, Coords target)
		{
			return new Coords (target.X - current.X, target.Y - current.Y, target.Z - current.Z);
		}

		static public float Square (float value)
		{
			return value * value;
		}

		static public float Dot (Coords vector1, Coords vector2)
		{
			return (vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z);
		}

		static public float Angle (Coords vector1, Coords vector2)
		{
			float dotDivide = Dot (vector1, vector2) /
						(Distance (vector1, new Coords (0, 0, 0)) * Distance (vector2, new Coords (0, 0, 0)));
			return Mathf.Acos (dotDivide); //radians.  For degrees * 180/Mathf.PI;
		}

		static public Coords LookAt2D (Coords forwardVector, Coords position, Coords focusPoint)
		{
			Coords direction = new Coords (focusPoint.X - position.X, focusPoint.Y - position.Y, position.Z);
			float angle = Math.Angle (forwardVector, direction);

			bool clockwise = false;
			if (Math.Cross (forwardVector, direction).Z < 0)
				clockwise = true;

			Coords newDir = Math.Rotate (forwardVector, angle, clockwise);
			return newDir;
		}

		/// <summary>
		/// Rotate vector and by using radians.
		/// </summary>
		/// <param name="vector">a vector that need to rotate</param>
		/// <param name="angle">angle of the rotation</param>
		/// <param name="clockwise">is need rotate to right or to left</param>
		static public Coords Rotate (Coords vector, float angle, bool clockwise) //in radians
		{
			if (clockwise)
			{
				angle = 2 * Mathf.PI - angle;
			}

			float xVal = vector.X * Mathf.Cos (angle) - vector.Y * Mathf.Sin (angle);
			float yVal = vector.X * Mathf.Sin (angle) + vector.Y * Mathf.Cos (angle);
			return new Coords (xVal, yVal, 0);
		}

		static public Coords Translate (Coords position, Coords facing, Coords vector)
		{
			if (Math.Distance (vector, new Coords (0, 0, 0)) <= 0) return position;
			float angle = Math.Angle (vector, facing);
			float worldAngle = Math.Angle (vector, new Coords (0, 1, 0));

			bool clockwise = false;
			if (Math.Cross (vector, facing).Z < 0)
				clockwise = true;

			vector = Math.Rotate (vector, angle + worldAngle, clockwise);

			float xVal = position.X + vector.X;
			float yVal = position.Y + vector.Y;
			float zVal = position.Z + vector.Z;
			return new Coords (xVal, yVal, zVal);
		}

		static public Coords Cross (Coords vector1, Coords vector2)
		{
			float xMult = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
			float yMult = vector1.Z * vector2.X - vector1.X * vector2.Z;
			float zMult = vector1.X * vector2.Y - vector1.Y * vector2.X;
			return new Coords (xMult, yMult, zMult);
		}

		static public Coords Lerp (Coords start, Coords end, float time)
		{
			time = Mathf.Clamp (time, 0, 1);
			Coords vector = Direction (start, end);
			float pointX = start.X + vector.X * time;
			float pointY = start.Y + vector.Y * time;
			float pointZ = start.Z + vector.Z * time;
			return new Coords (pointX, pointY, pointZ);
		}
	}
}
