using UnityEngine;
using System.Collections;

namespace LeoDeg.DrivebaleTank
{

	public class DriveToTarget : MonoBehaviour
	{
		[SerializeField] private ObjectManager objectManager;
		[SerializeField] private float speed = 10.0f;
		[SerializeField] private float rotationSpeed = 100.0f;
		[SerializeField] private float stopingDistance = 0.5f;

		void Update ()
		{
			if ((objectManager.fuel != null) && (objectManager.fuel.transform.position != this.transform.position))
			{
				if (Vector3.Distance (transform.position, objectManager.fuel.transform.position) > stopingDistance)
				{
					Vector3 direction = objectManager.fuel.transform.position - this.transform.position;
					direction.z = 0f;
					transform.position += direction * Time.deltaTime;
					transform.rotation = new Quaternion (0, 0, transform.rotation.z, transform.rotation.w);

					if (Vector3.Dot (transform.up, direction) != 0)
						transform.up = direction;
				}
			}
		}
	}
}