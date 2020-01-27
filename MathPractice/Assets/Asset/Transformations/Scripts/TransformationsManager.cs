using LeoDeg.Math;
using UnityEngine;

namespace LeoDeg.Transformations
{
	public class TransformationsManager : MonoBehaviour
	{
		public GameObject[] points;

		[Header ("Show Examples")]
		public bool rotationExample;
		public bool translationExample;
		public bool scaleExample;
		public bool shearExample;
		public bool reflectionExample;

		[Header ("Transformations Properties")]
		public GameObject centre;
		public Vector3 angles;
		public Vector3 translation;
		public Vector3 scale;
		public Vector3 shear;

		[Header ("Reflection Properties")]
		public bool reflectX;
		public bool reflectY;
		public bool reflectZ;

		void Start ()
		{
			if (translationExample)
				TranslatePointsToPosition ();

			if (scaleExample)
				ScalePointsByScalingValues ();

			if (rotationExample)
				RotatePoints ();

			if (shearExample)
				ShearPoints ();

			if (reflectionExample)
				ReflectPoints ();
		}

		private void ScalePointsByScalingValues ()
		{
			foreach (GameObject point in points)
			{
				point.transform.position = Math.Math.Scale (new Vector (point.transform.position), new Vector (scale)).ToVector3 ();
			}
		}

		private void TranslatePointsToPosition ()
		{
			foreach (GameObject point in points)
			{
				Debug.Log ("-------------------------");

				Vector translationPosition = new Vector (translation);
				Vector pointPosition = new Vector (point.transform.position);

				Vector direction = Math.Math.Direction (pointPosition, translationPosition);
				Vector3 translate = Math.Math.Translate (new Vector (point.transform.position), translationPosition).ToVector3 ();

				Debug.Log ("Translate pos: " + translate.ToString ());
				Debug.Log ("Old Point pos: " + point.transform.position.ToString ());

				point.transform.position = translate;
				Debug.Log ("New Point pos: " + point.transform.position.ToString ());
			}
		}

		private void RotatePoints ()
		{
			foreach (GameObject point in points)
			{
				Vector pointPosition = new Vector (point.transform.position);
				Vector rotation = angles.ToVector ();
				point.transform.position = Math.Math.Rotate (pointPosition, rotation.Deg2Rad (), true, true, true).ToVector3 ();
			}
		}

		private void ShearPoints ()
		{
			foreach (GameObject point in points)
			{
				Vector pointPosition = new Vector (point.transform.position);
				Vector shear = this.shear.ToVector ();
				point.transform.position = Math.Math.Shear (pointPosition, shear).ToVector3 ();
			}
		}

		private void ReflectPoints ()
		{
			foreach (GameObject point in points)
			{
				Vector pointPosition = new Vector (point.transform.position);
				point.transform.position = Math.Math.Reflect (pointPosition, reflectX, reflectY, reflectZ).ToVector3 ();
			}
		}

		void Update ()
		{

		}
	}
}