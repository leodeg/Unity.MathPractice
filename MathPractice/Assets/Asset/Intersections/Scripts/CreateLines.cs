using UnityEngine;
using LeoDeg.Math;

namespace LeoDeg.Intersactions
{
	public class CreateLines : MonoBehaviour
	{
		private Line first;
		private Line second;

		void Start ()
		{
			first = new Line (new Coords (-100, 0, 0), new Coords (200, 150, 0));
			second = new Line (new Coords (0, -100, 0), new Coords (0, 200, 0));

			first.Draw (1, Color.green).transform.parent = this.transform;
			second.Draw (1, Color.blue).transform.parent = this.transform;

			float intersectAt = first.IntersectAt (second);
			CreatePointAt (Math.Math.Lerp (first.Start, first.End, intersectAt).ToVector ());
		}

		void Update ()
		{

		}

		private void CreatePointAt (Vector3 position)
		{
			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.localScale *= 3;
			sphere.GetComponent<Renderer> ().material.color = Color.red;
			sphere.transform.position = position;
		}
	}
}