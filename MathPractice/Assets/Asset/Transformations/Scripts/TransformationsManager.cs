using LeoDeg.Math;
using UnityEngine;

namespace LeoDeg.Transformations
{
	public class TransformationsManager : MonoBehaviour
	{
		public GameObject point;
		public float angle;
		public Vector3 translation;

		void Start ()
		{
			Coords position = new Coords (point.transform.position);
			Vector3 translatePosition = Math.Math.Translate (position, new Coords (translation)).ToVector ();
			Debug.Log ("Position: " + position.ToString ());
			Debug.Log ("Translate position: " + translatePosition.ToString ());

			Debug.Log ("Point position: " + point.transform.position.ToString ());
			point.transform.position = translatePosition;
			Debug.Log ("Point position: " + point.transform.position.ToString ());
		}

		void Update ()
		{

		}
	}
}