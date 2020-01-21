using UnityEngine;
using System.Collections;
using LeoDeg.Math;

namespace LeoDeg.Intersactions
{
	public class CreatePlane : MonoBehaviour
	{
		[SerializeField] private Transform start;
		[SerializeField] private Transform firstVector;
		[SerializeField] private Transform secondVector;

		private GameObject spheresParent;
		private Math.Plane plane;

		private const int ONE = 1;
		private const float ONE_STEP = 0.1f;

		void Start ()
		{
			plane = new Math.Plane (new Coords (start.position),
				new Coords (firstVector.position),
				new Coords (secondVector.position));

			spheresParent = new GameObject ("SpheresParent");
			spheresParent.transform.parent = this.transform;

			CreatePlaneByUsingSpheres ();
		}

		private void CreatePlaneByUsingSpheres ()
		{
			for (float s = 0; s < ONE; s += ONE_STEP)
			{
				for (float t = 0; t < ONE; t += ONE_STEP)
				{
					GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
					sphere.transform.position = plane.Lerp (s, t).ToVector ();
					sphere.transform.parent = spheresParent.transform;
				}
			}
		}

		void Update ()
		{

		}
	}
}