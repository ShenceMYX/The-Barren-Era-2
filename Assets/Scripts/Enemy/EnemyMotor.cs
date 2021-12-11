using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	public class EnemyMotor : MonoBehaviour
	{
		public float moveSpeed = 5f;

		public Transform[] wayPoints;

		public int currentPointIndex;

		public float waitTime = 0.5f;
		private float startWaitTime = 0.5f;

        private void Start()
        {
			startWaitTime = waitTime;
        }

        public void MoveForward()
        {
			this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }

		public void RotateToTarget(Vector3 targetPoint)
		{
			Quaternion dir = Quaternion.LookRotation(targetPoint - transform.position);
			Vector3 eulerDir = dir.eulerAngles;
			transform.eulerAngles = new Vector3(0, eulerDir.y, 0);
		}


		public bool PathFinding()
        {

			if (Vector3.Distance(this.transform.position, wayPoints[currentPointIndex].position) < 0.3f)
            {
				startWaitTime -= Time.deltaTime;

				if (startWaitTime <= 0)
                {
					if (currentPointIndex < wayPoints.Length - 1)
						currentPointIndex++;
					else
						currentPointIndex = 0;

					startWaitTime = waitTime;
				}
            }
            else
            {
				RotateToTarget(wayPoints[currentPointIndex].position);
				MoveForward();
			}


			return true;
		}
	}
}