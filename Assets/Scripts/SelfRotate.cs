using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class SelfRotate : MonoBehaviour
	{
		public float selfRotateSpeed = 50f;
		private void Update()
		{
			transform.Rotate(Vector3.up * selfRotateSpeed * Time.deltaTime, Space.Self);
		}

	}
}