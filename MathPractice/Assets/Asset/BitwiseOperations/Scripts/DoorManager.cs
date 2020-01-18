using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;


public class DoorManager : MonoBehaviour
{
	public enum DoorType { None, Magic }
	public DoorType doorType = DoorType.Magic;

	private int doorKey;
	private BoxCollider boxCollider;

	void Start ()
	{
		boxCollider = this.GetComponent<BoxCollider> ();

		if (doorType.Equals (DoorType.Magic))
			doorKey = Attribute.MAGIC;
	}

	void Update ()
	{

	}

	private void OnCollisionEnter (Collision collision)
	{
		int collisionObjectHasKey = collision.gameObject.GetComponent<AttributeManager> ().Attributes & doorKey;
		this.boxCollider.isTrigger = collisionObjectHasKey != 0;
	}

	private void OnTriggerExit (Collider other)
	{
		this.boxCollider.isTrigger = false;
	}
}
