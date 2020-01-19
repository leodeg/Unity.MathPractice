using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoDeg.Point
{
	public class Draw2DAxis : MonoBehaviour
	{
		[SerializeField] private int axisSize = 20;
		[SerializeField] private int step = 2;
		[SerializeField] private int width = 200;
		[SerializeField] private GameObject prefab;
		private Coords endObjectPointAxis = new Coords (0, 0);
		private LineRenderer lineRenderer = new LineRenderer ();

		void Start ()
		{
			Coords.DrawLine (new Coords (axisSize, 0), new Coords (-axisSize, 0), 0.25f, Color.red);
			Coords.DrawLine (new Coords (0, axisSize), new Coords (0, -axisSize), 0.25f, Color.green);

			for (int i = -axisSize; i < axisSize; i++)
			{
				int xPos = i * step;
				if (Mathf.Abs (i) % 2 == 0)
					Coords.DrawLine (new Coords (xPos, width), new Coords (xPos, -width), 0.15f, Color.gray);
				else Coords.DrawLine (new Coords (xPos, width), new Coords (xPos, -width), 0.25f, Color.white);

				int yPos = i * step;
				if (Mathf.Abs (i) % 2 == 0)
					Coords.DrawLine (new Coords (width, yPos), new Coords (-width, yPos), 0.15f, Color.gray);
				else Coords.DrawLine (new Coords (width, yPos), new Coords (-width, yPos), 0.25f, Color.white);
			}
		}

		void Update ()
		{

		}
	}
}
