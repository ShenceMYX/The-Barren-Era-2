using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class FloatEnemyAI : MonoBehaviour
    {
       
        private EnemyMotor motor;

        public GameObject[] allPickups;

        protected void Start()
        {
            motor = GetComponent<EnemyMotor>();
            GetComponent<CharacterStatus>().OnDeathHandler += OnDeath;
        }

        protected void Update()
        {
            motor.PathFinding();
        }


        private void OnDeath()
        {
            Instantiate(allPickups[Random.Range(0, allPickups.Length)], transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        private void OnDisable()
        {
            GetComponent<CharacterStatus>().OnDeathHandler -= OnDeath;
        }
    }

}