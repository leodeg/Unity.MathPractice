using LeoDeg.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoDeg.Transformations
{
	public class CreateMatrix : MonoBehaviour
	{

		void Start ()
		{
			float[] matrixValues = { 1f, 2f, 3f, 4f, 5f, 6f };
			Matrix matrix = new Matrix (2, 3, matrixValues);
			Matrix matrix2 = new Matrix (3, 2, matrixValues);
			Matrix result = matrix * matrix2;
			Debug.Log (result.ToString ());
		}

		void Update ()
		{

		}
	}
}