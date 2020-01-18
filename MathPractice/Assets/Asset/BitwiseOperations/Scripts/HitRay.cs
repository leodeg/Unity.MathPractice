using UnityEngine;
using System.Collections;

public class HitRay : MonoBehaviour
{
	void Update ()
	{
		int layerMask = (1 << 9) | (1 << 10) | (1 << 11);
		//layerMask = ~layerMask; // all except 11

		RaycastHit raycastHit;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out raycastHit, Mathf.Infinity, layerMask))
		{
			Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * raycastHit.distance, Color.blue);
		}
		else
		{
			Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * 100, Color.red);
		}
	}
}
