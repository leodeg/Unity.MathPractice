using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

namespace LeoDeg.Bitboard
{
	class Bitboard
	{
		public long bitboard;

		public Bitboard ()
		{
			bitboard = 0;
		}

		public void SetCellState (int width, int row, int column)
		{
			long bit = 1L << (row * width + column);
			bitboard |= bit;
		}

		/// <summary>
		/// Return true if a cell has a value of 1, otherwise return false.
		/// </summary>
		public bool GetCellState (int width, int row, int column)
		{
			long mask = 1L << (row * width + column);
			return (bitboard & mask) != 0;
		}

		public int Count ()
		{
			int count = 0;
			long boardTemp = bitboard;

			while (boardTemp != 0)
			{
				boardTemp &= boardTemp - 1;
				++count;
			}

			return count;
		}

		public void DebugLog ()
		{
			Debug.Log (Convert.ToString (bitboard, 2).PadLeft (64, '0'));
		}

		public override string ToString ()
		{
			return Convert.ToString (bitboard, 2).PadLeft (64, '0');
		}
	}
}
