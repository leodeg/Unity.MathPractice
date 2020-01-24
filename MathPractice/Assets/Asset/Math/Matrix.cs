using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace LeoDeg.Math
{
	public class Matrix
	{
		private float[] values;
		private readonly int rowsAmount;
		private readonly int columnsAmount;

		public int Size { get { return rowsAmount * columnsAmount; } }

		public Matrix (int rows, int columns, float[] values)
		{
			this.values = values;
			this.rowsAmount = rows;
			this.columnsAmount = columns;
		}

		public float this[int index]
		{
			get { return values[index]; }
			set { values[index] = value; }
		}

		public float this[int row, int column]
		{
			get { return values[row * columnsAmount + column]; }
			set { values[row * columnsAmount + column] = value; }
		}

		public static Matrix operator + (Matrix a, Matrix b)
		{
			if (a.Size != b.Size)
				throw new InvalidOperationException ("Matrix::Error::Matrices have a different sizes.");

			Matrix result = new Matrix (a.rowsAmount, a.columnsAmount, a.values);
			for (int i = 0; i < a.Size; i++)
				result.values[i] += b.values[i];
			return result;
		}

		public static Matrix operator * (Matrix a, float value)
		{
			Matrix result = new Matrix (a.rowsAmount, a.columnsAmount, a.values);
			for (int i = 0; i < a.Size; i++)
				result.values[i] *= value;
			return result;
		}

		public static Matrix operator * (float value, Matrix a)
		{
			return a * value;
		}

		public static Matrix operator * (Matrix a, Matrix b)
		{
			if (a.columnsAmount != b.rowsAmount)
				throw new InvalidOperationException ("Matrix::Error::Matrices sizes is wrong for multiplication.");

			Matrix results = new Matrix (a.rowsAmount, a.columnsAmount, new float[a.rowsAmount * a.columnsAmount]);
			for (int i = 0; i < a.rowsAmount; i++)
				for (int j = 0; j < b.columnsAmount; j++)
					for (int k = 0; k < a.columnsAmount; k++)
						results[i, j] += a[i, k] * b[k, j];

			return results;
		}

		public Coords ToCoords ()
		{
			if (rowsAmount == 2 && columnsAmount == 1)
				return new Coords (values[0], values[1]);
			else if (rowsAmount == 3 && columnsAmount == 1)
				return new Coords (values[0], values[1], values[2]);

			return null;
		}

		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();

			for (int i = 0; i < rowsAmount; i++)
			{
				for (int j = 0; j < columnsAmount; j++)
				{
					stringBuilder.Append (values[i * columnsAmount + j]).Append (" ");
				}
				stringBuilder.AppendLine ();
			}

			return stringBuilder.ToString ();
		}
	}
}
