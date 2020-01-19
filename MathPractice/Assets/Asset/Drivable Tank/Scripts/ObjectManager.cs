using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
	[SerializeField] private GameObject fuelPrefab;

	[SerializeField] private int minPos = 0;
	[SerializeField] private int maxPos = 80;

	public GameObject fuel;
	public List<GameObject> FuelPrefabs { get; private set; }

	void Start ()
	{
		FuelPrefabs = new List<GameObject> ();
		InstantiateFulePrefab ();
	}

	private void InstantiateFulePrefab ()
	{
		if (fuelPrefab == null)
		{
			Debug.LogWarning ("ObjectManager::Warning::Fuel prefab is empty.");
		}
		else
		{
			GameObject fuel = Instantiate (fuelPrefab);
			fuel.transform.position = GetRandomPosition ();
			fuel.transform.parent = this.transform;
			this.fuel = fuel;
		}
	}
	private Vector3 GetRandomPosition ()
	{
		return new Vector3 (Random.Range (minPos, maxPos), Random.Range (minPos, maxPos), -0.1f);
	}

	void Update ()
	{

	}
}
