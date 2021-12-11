using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class Pickup : MonoBehaviour
	{
        private float startTime;
        public float canCollectTime = 0.5f;

        private void Update()
        {
            startTime += Time.deltaTime;

        }

        private void OnTriggerStay(Collider other)
        {
            if (startTime < canCollectTime) return;
            if (other.CompareTag("Player"))
            {
                Debug.Log("collected!!!!!1");
                //other.GetComponent<PlayerPossession>().possessDIC.Add(gameObject.name, true);
                Destroy(gameObject);
            }
        }
    }
}
