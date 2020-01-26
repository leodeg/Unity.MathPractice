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
			//CreateIntersectedLines ();
			CreateParallelLines ();
		}

		private void CreateIntersectedLines ()
		{
			first = new Line (new Coords (-100, 0, 0), new Coords (200, 150, 0));
			second = new Line (new Coords (0, -100, 0), new Coords (0, 200, 0));
			DrawLinesAndIntersection ();
		}

		private void CreateParallelLines ()
		{
			first = new Line (new Coords (-100, 0, 0), new Coords (200, 150, 0));
			second = new Line (new Coords (-100, 10, 0), new Coords (200, 150, 0));
			DrawLinesAndIntersection ();
		}

		private void DrawLinesAndIntersection ()
		{
			first.Draw (1, Color.green).transform.parent = this.transform;
			second.Draw (1, Color.blue).transform.parent = this.transform;

			float intersectAtFirst = first.IntersectAt (second);
			float intersectAtSecond = second.IntersectAt (first);

			if (!float.IsNaN (intersectAtFirst) && !float.IsNaN (intersectAtSecond))
			{
				Vector3 intersectionPosition = Math.Math.Lerp (first.Start, first.End, intersectAtFirst).ToVector3 ();
				CreatePointAt (intersectionPosition, 3f);
			}
		}

		private void CreatePointAt (Vector3 position, float width)
		{
			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.localScale *= width;
			sphere.GetComponent<Renderer> ().material.color = Color.red;
			sphere.transform.position = position;
		}

		void Update ()
		{

		}
	}
}