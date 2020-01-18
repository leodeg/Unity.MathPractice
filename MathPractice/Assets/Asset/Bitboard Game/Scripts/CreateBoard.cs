using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace LeoDeg.Bitboard
{
	public class CreateBoard : MonoBehaviour
	{
		[SerializeField] private Text score;
		[SerializeField] private GameObject treePrefab;
		[SerializeField] private GameObject housePrefab;
		[SerializeField] private GameObject[] tilePrefabs;

		private const int BOARD_SIZE = 8;
		private GameBitboards bitboards;
		private GameObject[] boardTiles;


		void Start ()
		{
			bitboards = new GameBitboards ();
			boardTiles = new GameObject[BOARD_SIZE * BOARD_SIZE];
			CreateDefaultBoard ();
			InvokeRepeating ("PlantTree", 1, 1);
		}

		private void CreateDefaultBoard ()
		{
			for (int row = 0; row < BOARD_SIZE; row++)
			{
				for (int column = 0; column < BOARD_SIZE; column++)
				{
					int randomTile = UnityEngine.Random.Range (0, tilePrefabs.Length);
					int tileIndex = row * BOARD_SIZE + column;
					boardTiles[tileIndex] = InstantiateNewTile (tilePrefabs[randomTile], new Vector3 (column, 0, row));
					bitboards.AssignBitboard (tilePrefabs[randomTile].tag, BOARD_SIZE, row, column);
				}
			}
		}

		private GameObject InstantiateNewTile (GameObject prefab, Vector3 position)
		{
			GameObject tile = Instantiate (prefab, position, Quaternion.identity);
			tile.name = string.Format ("{0}_X_{1}_Z_{2}", tile.tag, position.x, position.z);
			tile.transform.parent = this.transform;
			return tile;
		}

		private void PlantTree ()
		{
			int randomRow = UnityEngine.Random.Range (0, BOARD_SIZE);
			int randomColumn = UnityEngine.Random.Range (0, BOARD_SIZE);

			if (bitboards.dirt.GetCellState (BOARD_SIZE, randomRow, randomColumn) &&
				!bitboards.tree.GetCellState (BOARD_SIZE, randomRow, randomColumn) &&
				!bitboards.house.GetCellState (BOARD_SIZE, randomRow, randomColumn))
			{
				GameObject tree = Instantiate (treePrefab);
				tree.transform.parent = boardTiles[randomRow * BOARD_SIZE + randomColumn].transform;
				tree.transform.localPosition = Vector3.zero;
				tree.name = string.Format ("{0}_X_{1}_Z_{2}", tree.tag,
					tree.transform.position.x, tree.transform.position.z);

				bitboards.tree.SetCellState (BOARD_SIZE, randomRow, randomColumn);
			}

		}

		void Update ()
		{
			if (Input.GetMouseButton (0))
			{
				CreateNewHouse ();
			}
		}

		private void CreateNewHouse ()
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.collider.gameObject.tag == TileTags.Desert ||
					hit.collider.gameObject.tag == TileTags.Grain ||
					hit.collider.gameObject.tag == TileTags.Pasture)
				{
					if (!bitboards.house.GetCellState (BOARD_SIZE,
							(int)hit.transform.position.x,
							(int)hit.transform.position.z))
						InstantiateNewHouse (hit.transform);
				}
			}
		}

		private void InstantiateNewHouse (Transform parentTransform)
		{
			GameObject house = Instantiate (housePrefab);
			house.transform.parent = parentTransform;
			house.transform.localPosition = Vector3.zero;
			house.name = string.Format ("{0}_X_{1}_Z_{2}", house.tag,
				house.transform.position.x, house.transform.position.z);

			bitboards.house.SetCellState (BOARD_SIZE,
				(int)parentTransform.position.x,
				(int)parentTransform.position.z);

			UpdateScore ();
		}

		private void UpdateScore ()
		{
			score.text = string.Format ("Score: {0}", bitboards.house.Count ());
		}
	}
}
