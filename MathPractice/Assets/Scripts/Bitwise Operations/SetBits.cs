using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LeoDeg.MathPractice
{
	public class SetBits : MonoBehaviour
	{
		public int bitsSequence = 0;

		void Start ()
		{
			Debug.Log (Convert.ToString (bitsSequence, 2));
		}

		void Update ()
		{

		}

		private void OnDrawGizmos ()
		{

		}
	}
}
