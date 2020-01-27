using LeoDeg.Math;
using UnityEngine;

namespace LeoDeg.Transformations
{
	public class QuaternionRotation : MonoBehaviour
	{
		public GameObject[] points;

		[Header ("Transformations Properties")]
		public float angle;
		public Vector3 axis;

		private void Start ()
		{
			RotatePoints ();
		}

		private void RotatePoints ()
		{
			foreach (GameObject point in points)
			{
				Vector pointPosition = new Vector (point.transform.position);
				point.transform.position = Math.Math.QuaternionRotation (pointPosition, axis.ToVector (), angle).ToVector3 ();
			}
		}
	}
}