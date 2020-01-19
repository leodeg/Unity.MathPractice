using UnityEngine;
using System.Collections;

namespace LeoDeg.DrivebaleTank
{
	class CameraFollow : MonoBehaviour
	{
		[SerializeField] private Camera camera;
		[SerializeField] private Transform target;
		[SerializeField] private float speed = 10;

		private void Start ()
		{
			if (camera == null)
				Debug.Log ("CameraFollow::Error::Camera is empty. Please assign a camera.");
			if (target == null)
				Debug.Log ("CameraFollow::Error::Target is empty. Please assign a target to follow.");
		}

		private void Update ()
		{
			if (camera.transform.position != target.position)
			{
				Vector3 position = target.position;
				position.z = -10;
				camera.transform.position = Vector3.Lerp (camera.transform.position, position, speed * Time.deltaTime);
			}
		}
	}
}
