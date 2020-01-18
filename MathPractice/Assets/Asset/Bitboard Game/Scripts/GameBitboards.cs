using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace LeoDeg.Bitboard
{

	class GameBitboards
	{
		public Bitboard dirt = new Bitboard ();
		public Bitboard desert = new Bitboard ();
		public Bitboard grain = new Bitboard ();
		public Bitboard pasture = new Bitboard ();
		public Bitboard rock = new Bitboard ();
		public Bitboard water = new Bitboard ();
		public Bitboard woods = new Bitboard ();

		public Bitboard house = new Bitboard ();
		public Bitboard tree = new Bitboard ();

		private Dictionary<string, Action<int, int, int>> setCellDictionary;

		public GameBitboards ()
		{
			setCellDictionary = new Dictionary<string, Action<int, int, int>>
			{
				{ TileTags.Dirt, dirt.SetCellState },
				{ TileTags.Desert, desert.SetCellState },
				{ TileTags.Grain, grain.SetCellState },
				{ TileTags.Pasture, pasture.SetCellState },
				{ TileTags.Rock, rock.SetCellState },
				{ TileTags.Water, water.SetCellState },
				{ TileTags.Woods, woods.SetCellState },
				{ TileTags.House, house.SetCellState },
				{ TileTags.Tree, tree.SetCellState }
			};
		}

		public void AssignBitboard (string tileTag, int boardWidth, int row, int column)
		{
			if (setCellDictionary != null)
			{
				Action<int, int, int> onSetCellState;
				if (setCellDictionary.TryGetValue (tileTag, out onSetCellState))
					onSetCellState.Invoke (boardWidth, row, column);
			}
		}
	}
}
