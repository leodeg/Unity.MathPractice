using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class UIDistanceManager : MonoBehaviour
{
	[Header ("Positions")]
	[SerializeField] private Transform tank;
	[SerializeField] private ObjectManager objectManager;

	[Header ("UI")]
	[SerializeField] private Text textDistanceToFuel;

	private Vector3 oldTankPosition;

	void Start ()
	{

	}

	void Update ()
	{
		if (oldTankPosition != tank.position)
		{
			oldTankPosition = tank.position;
			float distanceToFuel = (objectManager.fuel.transform.position - tank.position).magnitude - 3.0f;
			if (distanceToFuel > 0)
			{
				textDistanceToFuel.text = string.Format ("Distance to Fuel: {0:0.00}", distanceToFuel);
			}
		}
	}
}
