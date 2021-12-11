using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// AI
	/// </summary>
	public abstract class EnemyAI : MonoBehaviour
	{
        public enum State
        {
            Attack,
            PathFinding
        }

        public State currentState = State.PathFinding;
        private EnemyMotor motor;

        public Transform target;
        protected float startAttackTime;
        public float attackInterval;
        public int atk = 5;
        protected Animator anim;

        public GameObject[] allPickups;

        protected void Start()
        {
            motor = GetComponent<EnemyMotor>();
            anim = GetComponentInChildren<Animator>();
            GetComponent<CharacterStatus>().OnDeathHandler += OnDeath;
        }

        private void OnDeath()
        {
            Instantiate(allPickups[Random.Range(0, allPickups.Length)], transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        private void OnDisable()
        {
            GetComponent<CharacterStatus>().OnDeathHandler -= OnDeath;
        }

        protected void Update()
        {
            switch (currentState)
            {
                case State.PathFinding:
                    PathFinding();
                    break;
                case State.Attack:
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            if (target == null)
                currentState = State.PathFinding;

            AttackAbility();

            
        }

        protected abstract void AttackAbility();
        

        private void PathFinding()
        {
            motor.PathFinding();

            if (target != null) 
                currentState = State.Attack;
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = null;
            }
        }

       
    }
}