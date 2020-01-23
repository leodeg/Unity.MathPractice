using UnityEngine;
using System.Collections;
using LeoDeg.Math;

/// <summary>
/// Line-line intersection example.
/// </summary>
public class CreateTennisGame : MonoBehaviour
{
	[SerializeField] private GameObject tennisBall;
	[SerializeField] private float stopTennisBallDistance = 0.5f;

	private Line road;
	private Line wall;
	private Vector3 intersectionPosition;

	void Start ()
	{
		CheckTennisBallPrefab ();
		InstantiateTennisBall ();
		DrawGameMap ();
		CalculateIntersectionPosition ();
	}

	private void CheckTennisBallPrefab ()
	{
		if (tennisBall == null)
			Debug.Log ("CreateTennisGame::Error::Tennis ball is empty.");
	}

	private void InstantiateTennisBall ()
	{
		tennisBall = Instantiate (tennisBall);
		tennisBall.name = "TennisBall";
		tennisBall.transform.position = Vector3.zero;
		tennisBall.transform.parent = this.transform;
	}

	private void DrawGameMap ()
	{
		road = new Line (new Coords (0, 0, 0), new Coords (20, 0, 0));
		wall = new Line (new Coords (10, -5, 0), new Coords (0, 10, 0));

		road.Draw (0.3f, Color.yellow).transform.parent = this.transform;
		wall.Draw (1f, Color.blue).transform.parent = this.transform;
	}

	private void CalculateIntersectionPosition ()
	{
		float intersectionAtLine = road.IntersectAt (wall);
		float intersectionAtWall = wall.IntersectAt (road);

		if (!float.IsNaN (intersectionAtLine) && !float.IsNaN (intersectionAtWall))
		{
			intersectionPosition = Math.Lerp (road.Start, road.End, intersectionAtLine).ToVector ();
			Debug.Log (intersectionPosition.ToString ());
		}
	}

	void Update ()
	{
		if (tennisBall == null) return;
		if (intersectionPosition == null) return;
		MoveTennisBallToIntersectionPosition ();
	}

	private void MoveTennisBallToIntersectionPosition ()
	{
		float distanceToIntersection = Math.Distance (tennisBall.transform.position.ToCoords (), intersectionPosition.ToCoords ());
		if (distanceToIntersection > stopTennisBallDistance)
		{
			Debug.Log ("Distance to intersection: " + distanceToIntersection);
			tennisBall.transform.Translate (intersectionPosition * Time.deltaTime);
		}
	}
}
