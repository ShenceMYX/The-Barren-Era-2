using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 鲨鱼头冲刺啃咬技能
    /// </summary>
    public class DashBiteSkill : PlayerSkill
    {
        public Transform hitPoint;
        public float attackDuration = 3f;
        private UltimateCharacterLocomotion ultimateCharacterLocomotion;
        private Vector3 acceleration;
        public int atk = 10;
        private void Start()
        {
            ultimateCharacterLocomotion = transform.root.GetComponent<UltimateCharacterLocomotion>();
            acceleration = ultimateCharacterLocomotion.MotorAcceleration;
        }

        public override void ExecuteSkill(GameObject enemyGO)
        {
            StartCoroutine(KeepBiting(enemyGO));
        }

        private IEnumerator KeepBiting(GameObject enemyGO)
        {
            float startTime = 0;

            while (startTime < attackDuration)
            {
                if (enemyGO == null) break;
                ultimateCharacterLocomotion.MotorAcceleration = acceleration * 2;
                if (Vector3.Distance(new Vector3(enemyGO.transform.position.x,0, enemyGO.transform.position.z), new Vector3(hitPoint.position.x,0,hitPoint.position.z))<1.5f)
                {
                    enemyGO.GetComponent<CharacterStatus>().Damage(atk);
                    Debug.Log("damage");
                }
                yield return new WaitForSeconds(0.5f);
                startTime += 0.5f;
            }
            ultimateCharacterLocomotion.MotorAcceleration = acceleration;

        }
    }
}