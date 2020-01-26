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

		public int Size { get { return RowsCount * ColumnsCount; } }
		public int RowsCount { get; }
		public int ColumnsCount { get; }

		public Matrix (Matrix matrix)
		{
			this.values = matrix.values;
			RowsCount = matrix.RowsCount;
			ColumnsCount = matrix.ColumnsCount;
		}

		public Matrix (int rowsCount, int columnsCount, float[] values)
		{
			this.values = values;
			RowsCount = rowsCount;
			ColumnsCount = columnsCount;
		}

		public Matrix (int rowsCount, int columnsCount, float[,] values)
		{
			RowsCount = rowsCount;
			ColumnsCount = columnsCount;

			for (int i = 0; i < rowsCount; i++)
				for (int j = 0; j < columnsCount; j++)
					this[i, j] = values[i, j];
		}

		public float this[int index]
		{
			get
			{
				if (index >= Size)
					throw new IndexOutOfRangeException ();
				return values[index];
			}
			set
			{
				if (index >= Size)
					throw new IndexOutOfRangeException ();
				values[index] = value;
			}
		}

		public float this[int row, int column]
		{
			get
			{
				if (row >= RowsCount || column >= ColumnsCount)
					throw new IndexOutOfRangeException ();
				return values[row * ColumnsCount + column];
			}
			set
			{
				if (row >= RowsCount || column >= ColumnsCount)
					throw new IndexOutOfRangeException ();
				values[row * ColumnsCount + column] = value;
			}
		}

		public float[] To1DArray ()
		{
			return values;
		}

		public float[,] To2DArray ()
		{
			float[,] values = new float[RowsCount, ColumnsCount];

			for (int i = 0; i < RowsCount; i++)
				for (int j = 0; j < ColumnsCount; j++)
					values[i, j] = this[i, j];

			return values;
		}

		public Coords ToCoords ()
		{
			if ((RowsCount == 2 && ColumnsCount == 1) || (ColumnsCount == 2 && RowsCount == 1))
				return new Coords (values[0], values[1]);

			if ((RowsCount == 3 && ColumnsCount == 1) || (ColumnsCount == 3 && RowsCount == 1))
				return new Coords (values[0], values[1], values[2]);

			if ((RowsCount == 4 && ColumnsCount == 1) || (ColumnsCount == 4 && RowsCount == 1))
				return new Coords (values[0], values[1], values[2], values[3]);

			throw new InvalidOperationException ("Matrix cannot be casted to Coords type. Rows count: " + RowsCount + ", Columns count: " + ColumnsCount);
		}

		public static Matrix operator + (Matrix a, Matrix b)
		{
			if (a.Size != b.Size)
				throw new InvalidOperationException ("Matrix::Error::Matrices have a different sizes.");

			Matrix result = new Matrix (a.RowsCount, a.ColumnsCount, a.values);
			for (int i = 0; i < a.Size; i++)
				result.values[i] += b.values[i];
			return result;
		}

		public static Matrix operator * (Matrix a, float value)
		{
			Matrix result = new Matrix (a.RowsCount, a.ColumnsCount, a.values);
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
			if (a.ColumnsCount != b.RowsCount)
				throw new InvalidOperationException ("Matrix::Error::Matrices sizes is wrong for multiplication.");

			Matrix results = new Matrix (a.RowsCount, b.ColumnsCount, new float[a.RowsCount * b.ColumnsCount]);
			for (int i = 0; i < a.RowsCount; i++)
				for (int j = 0; j < b.ColumnsCount; j++)
					for (int k = 0; k < a.ColumnsCount; k++)
						results[i, j] += a[i, k] * b[k, j];

			return results;
		}

		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();

			for (int i = 0; i < RowsCount; i++)
			{
				for (int j = 0; j < ColumnsCount; j++)
				{
					stringBuilder.Append (values[i * ColumnsCount + j]).Append (" ");
				}
				stringBuilder.AppendLine ();
			}

			return stringBuilder.ToString ();
		}
	}
}
