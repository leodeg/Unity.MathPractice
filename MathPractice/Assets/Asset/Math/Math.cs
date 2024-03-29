﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoDeg.Math
{
	public class Math
	{
		static public Vector GetNormal (Vector vector)
		{
			float length = Distance (vector, new Vector (0, 0, 0));
			float multiplier = 1.0f / length;
			vector.X *= multiplier;
			vector.Y *= multiplier;
			vector.Z *= multiplier;

			return vector;
		}

		static public float Distance (Vector start, Vector target)
		{
			float diffSquared = Square (target.X - start.X) +
								Square (target.Y - start.Y) +
								Square (target.Z - start.Z);
			return Mathf.Sqrt (diffSquared);

		}

		static public Vector Direction (Vector current, Vector target)
		{
			return new Vector (target.X - current.X, target.Y - current.Y, target.Z - current.Z);
		}

		static public float Square (float value)
		{
			return value * value;
		}

		static public float Dot (Vector vector1, Vector vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		static public Vector Cross (Vector vector1, Vector vector2)
		{
			return new Vector (
				vector1.Y * vector2.Z - vector1.Z * vector2.Y,
				vector1.Z * vector2.X - vector1.X * vector2.Z,
				vector1.X * vector2.Y - vector1.Y * vector2.X);
		}

		static public Vector Lerp (Vector start, Vector end, float time)
		{
			time = Mathf.Clamp (time, 0, 1);
			Vector vector = Direction (start, end);
			return new Vector (
				start.X + vector.X * time,
				start.Y + vector.Y * time,
				start.Z + vector.Z * time);
		}

		static public float Angle (Vector vector1, Vector vector2)
		{
			float dotDivide = Dot (vector1, vector2) /
						(Distance (vector1, new Vector (0, 0, 0)) * Distance (vector2, new Vector (0, 0, 0)));
			return Mathf.Acos (dotDivide); //radians.  For degrees * 180/Mathf.PI;
		}

		static public Vector LookAt2D (Vector forwardVector, Vector position, Vector focusPoint)
		{
			Vector direction = new Vector (focusPoint.X - position.X, focusPoint.Y - position.Y, position.Z);
			float angle = Math.Angle (forwardVector, direction);

			bool clockwise = false;
			if (Math.Cross (forwardVector, direction).Z < 0)
				clockwise = true;

			return Math.Rotate (forwardVector, angle, clockwise);
		}

		#region Transformation

		/// <summary>
		/// Rotate vector and by using radians.
		/// </summary>
		/// <param name="vector">a vector that need to rotate</param>
		/// <param name="angle">angle of the rotation</param>
		/// <param name="clockwise">is need rotate to right or to left</param>
		static public Vector Rotate (Vector vector, float angle, bool clockwise) //in radians
		{
			if (clockwise)
				angle = 2 * Mathf.PI - angle;

			float xVal = vector.X * Mathf.Cos (angle) - vector.Y * Mathf.Sin (angle);
			float yVal = vector.X * Mathf.Sin (angle) + vector.Y * Mathf.Cos (angle);
			return new Vector (xVal, yVal, 0);
		}

		static public Vector Translate (Vector position, Vector facing, Vector vector)
		{
			if (Distance (vector, new Vector (0, 0, 0)) <= 0) return position;
			float angle = Angle (vector, facing);
			float worldAngle = Angle (vector, new Vector (0, 1, 0));

			bool clockwise = false;
			if (Cross (vector, facing).Z < 0)
				clockwise = true;

			vector = Rotate (vector, angle + worldAngle, clockwise);
			return new Vector (
				position.X + vector.X,
				position.Y + vector.Y,
				position.Z + vector.Z);
		}

		static public Vector TranslateTo (Vector start, Vector vector)
		{
			if (Math.Distance (vector, new Vector (0, 0, 0)) <= 0) return start;
			return start + vector;
		}

		static public Vector Translate (Vector position, float x, float y, float z)
		{
			return Translate (position, new Vector (x, y, z));
		}

		static public Vector Translate (Vector position, Vector vector)
		{
			Matrix resultPosition = vector.ToTranslationMatrix () * position.ToMatrix ();
			return resultPosition.ToVector4 ();
		}

		static public Vector Scale (Vector position, float x, float y, float z)
		{
			return Scale (position, new Vector (x, y, z));
		}

		static public Vector Scale (Vector position, Vector scale)
		{
			Matrix resultScale = scale.ToScaleMatrix () * position.ToMatrix ();
			return resultScale.ToVector4 ();
		}

		public static Vector Rotate (Vector position, Vector rotationRad, bool clockwiseX, bool clockwiseY, bool clockwiseZ)
		{
			return Rotate (position, rotationRad.X, clockwiseX, rotationRad.Y, clockwiseY, rotationRad.Z, clockwiseZ);
		}

		public static Vector Rotate (Vector position, float angleXRad, bool clockwiseX, float angleYRad, bool clockwiseY, float angleZRad, bool clockwiseZ)
		{
			Matrix rotationMatrix = GetRotationMatrix (angleXRad, clockwiseX, angleYRad, clockwiseY, angleZRad, clockwiseZ);
			Matrix result = rotationMatrix * position.ToMatrix ();
			return result.ToVector4 ();
		}

		public static Matrix GetRotationMatrix (float angleXRad, bool clockwiseX, float angleYRad, bool clockwiseY, float angleZRad, bool clockwiseZ)
		{
			if (clockwiseX) angleXRad = (2 * Mathf.PI) - angleXRad;
			if (clockwiseY) angleYRad = (2 * Mathf.PI) - angleYRad;
			if (clockwiseZ) angleZRad = (2 * Mathf.PI) - angleZRad;

			float[] xRollValues = {
				1,0,0,0,
				0, Mathf.Cos (angleXRad), -Mathf.Sin(angleXRad), 0,
				0, Mathf.Sin (angleXRad), Mathf.Cos(angleXRad), 0,
				0,0,0,1,
			};

			float[] yRollValues = {
				Mathf.Cos(angleYRad),0, Mathf.Sin(angleYRad),0,
				0,1,0,0,
				-Mathf.Sin(angleYRad),0, Mathf.Cos(angleYRad),0,
				0,0,0,1
			};

			float[] zRollValues = {
				Mathf.Cos(angleZRad), -Mathf.Sin(angleZRad),0,0,
				Mathf.Sin(angleZRad), Mathf.Cos(angleZRad),0,0,
				0, 0, 1, 0,
				0, 0, 0, 1
			};

			Matrix xRoll = new Matrix (4, 4, xRollValues);
			Matrix yRoll = new Matrix (4, 4, yRollValues);
			Matrix zRoll = new Matrix (4, 4, zRollValues);

			return zRoll * yRoll * xRoll;
		}

		public static Vector Shear (Vector position, Vector shearValues)
		{
			return Shear (position, shearValues.X, shearValues.Y, shearValues.Z);
		}

		public static Vector Shear (Vector position, float shearX, float shearY, float shearZ)
		{
			float[] shearValues =
			{
				1, shearX, 0, 0,
				0, 1, shearY, 0,
				0,shearZ, 1, 0,
				0, 0, 0, 1
			};

			Matrix shearMatrix = new Matrix (4, 4, shearValues);
			Matrix result = shearMatrix * position.ToMatrix ();
			return result.ToVector4 ();
		}

		public static Vector ReflectX (Vector position)
		{
			return Reflect (position, true, false, false);
		}

		public static Vector ReflectY (Vector position)
		{
			return Reflect (position, false, true, false);
		}

		public static Vector ReflectZ (Vector position)
		{
			return Reflect (position, false, false, true);
		}

		public static Vector Reflect (Vector position, bool reflectX, bool reflectY, bool reflectZ)
		{
			int x = 1, y = 1, z = 1;

			if (reflectX) x = -1;
			if (reflectY) y = -1;
			if (reflectZ) z = -1;

			float[] reflectValues = {
				x, 0, 0, 0,
				0, y, 0, 0,
				0, 0, z, 0,
				0, 0, 0, 1
			};

			Matrix reflectionMatrix = new Matrix (4, 4, reflectValues);
			Matrix result = reflectionMatrix * position.ToMatrix ();
			return result.ToVector4 ();
		}

		static public Vector QuaternionRotation (Vector position, Vector axis, float angleDegrees)
		{
			Matrix quaternionMatrix = GetQuaternionRotationMatrix (axis, angleDegrees);
			Matrix result = quaternionMatrix * position.ToMatrix ();
			return result.ToVector4 ();
		}

		static public Matrix GetQuaternionRotationMatrix (Vector axis, float angleDegrees)
		{
			Vector axisNormal = axis.Normal ();
			float cos = Mathf.Cos (angleDegrees * Mathf.Deg2Rad / 2);
			float sin = Mathf.Sin (angleDegrees * Mathf.Deg2Rad / 2);
			Vector q = new Vector (axisNormal.X * sin, axisNormal.Y * sin, axisNormal.Z * sin, cos);

			float[] quaternionValues = {
				1 - 2*q.Y*q.Y - 2*q.Z*q.Z, 2*q.X*q.Y - 2*q.W*q.Z,      2*q.X*q.Z + 2*q.W*q.Y,     0,
				2*q.X*q.Y + 2*q.W*q.Z,     1 - 2*q.X*q.X - 2*q.Z*q.Z,  2*q.Y*q.Z - 2*q.W*q.X,     0,
				2*q.X*q.Z - 2*q.W*q.Y,     2*q.Y*q.Z + 2*q.W*q.X,      1 - 2*q.X*q.X - 2*q.Y*q.Y, 0,
				0,                         0,                          0,                         1
			};

			return new Matrix (4, 4, quaternionValues);
		}

		static public float GetRotationAxisAngle (Matrix rotation)
		{
			float value = rotation[0, 0] + rotation[1, 1] + rotation[2, 2] + rotation[3, 3] - 2;
			return Mathf.Acos (0.5f * value);
		}

		static public Vector GetRotationAxis (Matrix rotation, float angle)
		{
			float sin = 2 * Mathf.Sin (angle);
			float multiplier = 1.0f / sin;

			return new Vector (
				(rotation[2, 1] - rotation[1, 2]) * multiplier,
				(rotation[0, 2] - rotation[2, 0]) * multiplier,
				(rotation[1, 0] - rotation[0, 1]) * multiplier
			);
		}

		#endregion
	}
}
