using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
	public Text attributeDisplay;
	private int attributes = 0;
	public int Attributes { get { return attributes; } }

	void Start ()
	{
		attributeDisplay.color = Color.red;
		attributeDisplay.fontSize = 20;
		UpdateDisplayTextAttribute ();
	}

	void Update ()
	{
		Vector3 screenPoint = Camera.main.WorldToScreenPoint (this.transform.position);
		attributeDisplay.transform.position = screenPoint + (Vector3.down * 50);
	}

	private void UpdateDisplayTextAttribute ()
	{
		attributeDisplay.text = Convert.ToString (attributes, 2).PadLeft (8, '0');
		attributeDisplay.text += string.Format ("\nInteger Number: {0}", attributes);
	}

	private void OnTriggerEnter (Collider other)
	{
		switch (other.gameObject.tag)
		{
			// Add bits => attributes |= values
			// Add multiple bits => attributes |= (values | values2 | value3)
			case Tags.TAG_MAGIC: attributes ^= Attribute.MAGIC; break;
			case Tags.TAG_CHARISMA: attributes |= Attribute.CHARISMA; break;
			case Tags.TAG_FLY: attributes |= Attribute.FLY; break;
			case Tags.TAG_INTELLIGENCE: attributes |= Attribute.INTELLIGENCE; break;
			case Tags.TAG_INVISIBLE: attributes |= Attribute.INVISIBLE; break;

			// Subtract bits => attributes &= ~values
			// Subtract multiple bits => attributes &= ~(values | values2 | value3)
			case Tags.TAG_SUB_MAGIC: attributes &= ~Attribute.MAGIC; break;
			case Tags.TAG_SUB_CHARISMA: attributes &= ~Attribute.CHARISMA; break;
			case Tags.TAG_SUB_FLY: attributes &= ~Attribute.FLY; break;
			case Tags.TAG_SUB_INTELLIGENCE: attributes &= ~Attribute.INTELLIGENCE; break;
			case Tags.TAG_SUB_INVISIBLE: attributes &= ~Attribute.INVISIBLE; break;
		}

		UpdateDisplayTextAttribute ();
	}
}
