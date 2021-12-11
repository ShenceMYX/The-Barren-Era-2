using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class FlyEnemyAI : EnemyAI
    {
        public float chaseSpeed = 5f;
        public Vector3 targetOffset;
        public LayerMask layer;
        private RaycastHit hit;
        public float chaseWaitTime = 1f;
        private float startChaseTime;
        public float rotationSpeed = 10f;
        public bool fly = false;
        private float initialY;

        private new void Start()
        {
            base.Start();
            anim.GetComponent<AnimationEventBehaviour>().attackHandler += DamagePlayer;
            layer = LayerMask.GetMask(new string[1] { "Player" });
            initialY = transform.position.y;
            startAttackTime = attackInterval;
        }

        private void DamagePlayer()
        {
            if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit, 10f, layer))
            {
                if(Vector3.Distance(hit.point, transform.position)<0.5f)
                    target.root.GetComponent<CharacterStatus>().Damage(atk);
            }
        }

        protected override void AttackAbility()
        {
            if (target == null) return;
            if(!fly)
                LookRotation(target.position);
            else
                transform.LookAt(target.position);
            //Debug.DrawLine(transform.position, transform.position + (target.position - transform.position).normalized * 10f, Color.red);

            if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit, 10f, layer))
            {
                if (Vector3.Distance(hit.point, transform.position) < 0.5f)
                {
                    startChaseTime = 0;
                    if (startAttackTime < 0)
                    {
                        anim.SetBool("attack", true);
                        startAttackTime = attackInterval;
                    }
                    else
                    {
                        startAttackTime -= Time.deltaTime;
                    }
                }
                else
                {
                    startChaseTime += Time.deltaTime;
                    if (startChaseTime > chaseWaitTime)
                        ChaseTarget(target.position + targetOffset);
                }

            }
            //if (Vector3.Distance(transform.position, target.position + targetOffset) < 3.5f) 
            //{
            //    if (startAttackTime < Time.time)
            //    {
            //        anim.SetBool("attack", true);
            //        startAttackTime = Time.time + attackInterval;
            //    }
            //}
            //else
            //{
            //    ChaseTarget(target.position + targetOffset);
            //}

        }

        private void LookRotation(Vector3 target)
        {
            Quaternion dir = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rotationSpeed * Time.deltaTime);
            Vector3 euler = dir.eulerAngles;
            transform.eulerAngles = new Vector3(0, euler.y, 0);
        }

        private void ChaseTarget(Vector3 target)
        {
            
            Vector3 targetPos = Vector3.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
            if (!fly)
                transform.position = new Vector3(targetPos.x, initialY, targetPos.z);
            else
                transform.position = targetPos;
        }

        private void OnDisable()
        {
            anim.GetComponent<AnimationEventBehaviour>().attackHandler -= DamagePlayer;
        }

        private void OnDrawGizmos()
        {
            if(hit.point!=null)
            {
                Gizmos.DrawSphere(hit.point, 0.1f);
                Gizmos.color = Color.red;
            }

        }


    }
}