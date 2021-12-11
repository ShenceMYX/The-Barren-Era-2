using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class PlayerCharacterAI : MonoBehaviour
	{
        private float startAttackTime;
        public float attackInterval = 1.5f;
        public int atk = 20;

        //private Animator anim;

        public Transform target;
        private PlayerSkill[] skills;

        //private void Start()
        //{
        //    //anim = GetComponentInChildren<Animator>();
        //    //GetComponentInChildren<AnimationEventBehaviour>().attackHandler += AttackEnemy;
        //    skills = GetComponentsInChildren<PlayerSkill>();
        //}

        public void UpdateSkills()
        {
            skills = GetComponentsInChildren<PlayerSkill>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if(startAttackTime < Time.time)
                {
                    //target = other.transform;
                    skills[Random.Range(0, skills.Length)].ExecuteSkill(other.gameObject);
                    //anim.SetTrigger("attack");
                    startAttackTime = Time.time + attackInterval;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //target = null;
            }
        }

        public void AttackEnemy()
        {
            //if (target == null) return;
            //Debug.Log(Vector3.Distance(transform.position, target.position));
            //if (Vector3.Distance(transform.position, target.position) < 5f)
            //    target.GetComponent<CharacterStatus>().Damage(atk);
        }

        private void OnDisable()
        {
            //GetComponentInChildren<AnimationEventBehaviour>().attackHandler -= AttackEnemy;
        }
    }
}
