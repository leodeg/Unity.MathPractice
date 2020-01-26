using LeoDeg.Math;
using UnityEngine;

namespace LeoDeg.Transformations
{
	public class TransformationsManager : MonoBehaviour
	{
		public GameObject[] points;

		[Header ("Transformations Properties")]
		public float angle;
		public GameObject centre;
		public Vector3 scale;
		public Vector3 translation;
		public Vector3 scaling;

		void Start ()
		{
			TranslatePointsToPosition ();
			//ScalePointsByScalingValues ();
		}

		private void ScalePointsByScalingValues ()
		{
			foreach (GameObject point in points)
			{
				//Coords position = new Coords (point.transform.position);
				//position = Math.Math.Translate (position, new Coords (centre.transform.position));
				//point.transform.position = Math.Math.Scale (position, new Coords (scaling)).ToVector ();

				point.transform.position = Math.Math.Scale (new Coords (point.transform.position), new Coords (scaling)).ToVector3 ();
			}
		}

		private void TranslatePointsToPosition ()
		{
			foreach (GameObject point in points)
			{
				Debug.Log ("-------------------------");

				Coords direction = Math.Math.Direction (new Coords (point.transform.position), new Coords (translation));
				Vector3 translate = Math.Math.Translate (new Coords (point.transform.position), direction).ToVector3 ();

				Debug.Log ("Translate pos: " + translate.ToString ());
				Debug.Log ("Old Point pos: " + point.transform.position.ToString ());

				point.transform.position = translate;
				Debug.Log ("New Point pos: " + point.transform.position.ToString ());
			}
		}

		void Update ()
		{

		}
	}
}