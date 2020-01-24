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

		[Header ("Ball Properties")]
		[SerializeField] private float stopBallDistance = 0.3f;

		private GameObject spheresParent;
		private Math.Plane plane;

		private Line trajectory;

		private GameObject ball;
		private Vector3 intersectionVector;

		private const int ONE = 1;
		private const float ONE_STEP = 0.1f;

		void Start ()
		{
			InitializeVariables ();
			CreateSpheresParent ();
			CreatePlaneByUsingSpheres ();

			CalculateIntersectionPosition ();
			InstantiateSphereAtPosition (intersectionVector, pointSize, pointColor);
			ball = InstantiateSphereAtPosition (trajectory.Lerp (0.1f).ToVector (), pointSize, Color.yellow);
			Debug.Break ();
		}

		private void InitializeVariables ()
		{
			plane = new Math.Plane (planeStart.position.ToCoords (),
												planeFirstVector.position.ToCoords (),
												planeSecondVector.position.ToCoords ());
			trajectory = new Line (lineStart.position.ToCoords (),
							lineEnd.position.ToCoords (),
							Line.LineType.Segment);
			trajectory.Draw (0.3f, Color.green);
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

		private void CalculateIntersectionPosition ()
		{
			float intersectionAtLine = trajectory.IntersectAt (plane);
			if (!float.IsNaN (intersectionAtLine))
			{
				intersectionVector = trajectory.Lerp (intersectionAtLine).ToVector ();
			}
		}

		private GameObject InstantiateSphereAtPosition (Vector3 position, float size, Color color)
		{
			GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.position = position;
			sphere.transform.localScale *= size;
			sphere.GetComponent<Renderer> ().material.color = color;
			sphere.name = string.Format ("IntersectionSphere_{0}", position.ToString ());
			return sphere;
		}

		void Update ()
		{
			float distanceToIntersection = Math.Math.Distance (ball.transform.position.ToCoords (), intersectionVector.ToCoords ());
			if (distanceToIntersection > stopBallDistance)
			{
				Vector3 direction = intersectionVector - ball.transform.position;
				Debug.Log ("Distance to Intersection: " + distanceToIntersection.ToString ());
				ball.transform.Translate (direction * Time.deltaTime);
			}
			else
			{
				intersectionVector = trajectory.Reflect (plane.Normal).ToVector ().normalized;
				Vector3 direction = intersectionVector - ball.transform.position;
				ball.transform.Translate (direction * Time.deltaTime);
			}
		}
	}
}