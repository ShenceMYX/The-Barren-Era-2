using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 头部撞击技能
    /// </summary>
    public class HeadTackleSkill : PlayerSkill
    {
        private Animator anim;
        private Transform target;
        public int atk = 15;
        public Transform hitPoint;

        private void Start()
        {
            anim = transform.root.GetComponentInChildren<Animator>();
            transform.root.GetComponentInChildren<AnimationEventBehaviour>().attackHandler += AttackEnemy;
        }

        private void AttackEnemy()
        {
            if (Vector3.Distance(new Vector3(hitPoint.position.x, 0, hitPoint.position.z), new Vector3(target.position.x, 0, target.position.z)) < 1.25f)
                target.GetComponent<CharacterStatus>().Damage(atk);
        }

        public override void ExecuteSkill(GameObject enemyGO)
        {
            Debug.Log("head attack");
            anim.SetBool("attack", true);
            target = enemyGO.transform;
        }

        private void OnDisable()
        {
            transform.root.GetComponentInChildren<AnimationEventBehaviour>().attackHandler -= AttackEnemy;
        }
    }
}