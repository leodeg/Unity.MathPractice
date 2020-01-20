using LeoDeg.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoDeg.Intersactions
{
	public class Move : MonoBehaviour
	{
		[SerializeField] private Transform start;
		[SerializeField] private Transform end;
		[SerializeField] [Range (0, 1)] private float time = 0.5f;
		//private LeoDeg.Math.Line line;

		void Start ()
		{
			//line = new Line (new Coords (start.position), new Coords (end.position));
		}

		void Update ()
		{
			//line = new Line (new Coords (start.position), new Coords (end.position));
			//transform.position = line.GetPointAt (time).ToVector ();

			transform.position = Math.Math.Lerp (new Coords (start.position), new Coords (end.position), time).ToVector ();
		}
	}
}