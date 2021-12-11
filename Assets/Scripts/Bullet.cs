using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class Bullet : MonoBehaviour
	{
		public Vector3 targetPos;
		public float attack = 10;
		public float speed = 10;
		public string targetTag = "Enemy";

        private void Update()
        {
			if (targetPos == null) return;
			if (Vector3.Distance(transform.position, targetPos) < 0.1f)
				Destroy(gameObject);
			else
				transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        }

        private void OnTriggerStay(Collider other)
        {
			if (other.CompareTag(targetTag))
            {
				Debug.Log(Vector3.Distance(other.transform.position, transform.position));
				if(Vector3.Distance(other.transform.position, transform.position) < 1f)
                {
					other.GetComponent<CharacterStatus>().Damage(attack);
					Destroy(gameObject);
				}
				
			}
		}
    }
}
