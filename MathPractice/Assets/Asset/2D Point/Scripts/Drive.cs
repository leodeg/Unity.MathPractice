using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace LeoDeg.Point
{
	public class Drive : MonoBehaviour
	{
		public float speed = 10.0f;
		public float rotationSpeed = 100.0f;
		public Text energyAmt;
		Vector3 currentLocation;

		void Start ()
		{
			currentLocation = this.transform.position;
		}

		void Update ()
		{
			if (float.Parse (energyAmt.text) <= 0) return;

			float translation = Input.GetAxis ("Vertical") * speed;
			float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;

			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;

			transform.Translate (0, translation, 0);
			transform.Rotate (0, 0, -rotation);
			energyAmt.text = (float.Parse (energyAmt.text)
				- Vector3.Distance (currentLocation, this.transform.position)) + "";

			currentLocation = this.transform.position;
		}
	}
}
