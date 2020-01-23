using UnityEngine;
using LeoDeg.Math;

namespace LeoDeg.Intersactions
{
	public class CreatePlaneHit : MonoBehaviour
	{
		[Header ("Points")]
		[SerializeField] private Transform planeStart;
		[SerializeField] private Transform planeFirstVector;
		[SerializeField] private Transform planeSecondVector;
		[SerializeField] private Transform lineStart;
		[SerializeField] private Transform lineEnd;

		[Header ("Intersection Point Properties")]
		[SerializeField] private float pointSize = 1.0f;
		[SerializeField] private Color pointColor = Color.red;

		private GameObject spheresParent;
		private Math.Plane plane;
		private Line line;

		private const int ONE = 1;
		private const float ONE_STEP = 0.1f;

		void Start ()
		{
			InitializeVariables ();
			CreateSpheresParent ();
			CreatePlaneByUsingSpheres ();
			DrawPointAtIntersectionPosition (CalculateIntersectionPosition (), pointSize, pointColor);
		}

		private void InitializeVariables ()
		{
			plane = new Math.Plane (planeStart.position.ToCoords (),
												planeFirstVector.position.ToCoords (),
												planeSecondVector.position.ToCoords ());
			line = new Line (lineStart.position.ToCoords (),
							lineEnd.position.ToCoords (),
							Line.LineType.Ray);
			line.Draw (0.3f, Color.green);
		}

		private void CreateSpheresParent ()
		{
			spheresParent = new GameObject ("SpheresParent");
			spheresParent.transform.parent = this.transform;
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

		private Vector3 CalculateIntersectionPosition ()
		{
			float positionOnLine = line.IntersectAt (plane);
			return line.Lerp (positionOnLine).ToVector ();
		}

		private void DrawPointAtIntersectionPosition (Vector3 position, float size, Color color)
		{
			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.position = position;
			sphere.transform.localScale *= size;
			sphere.GetComponent<Renderer> ().material.color = color;
			sphere.name = string.Format ("IntersectionSphere_{0}", position.ToString ());
		}

		void Update ()
		{

		}
	}
}