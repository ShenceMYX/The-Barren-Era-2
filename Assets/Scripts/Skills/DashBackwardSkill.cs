using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class DashBackwardSkill : PlayerSkill
    {
        public float dashSpeed = 150f;
        public float dashDuration = 0.6f;
        private UltimateCharacterLocomotion ultimateCharacterLocomotion;
        public GameObject bullet;
        public float distance = 10f;

        private void Start()
        {
            ultimateCharacterLocomotion = transform.root.GetComponent<UltimateCharacterLocomotion>();
        }

        public override void ExecuteSkill(GameObject enemyGO)
        {
            GameObject bulletGO = Instantiate(bullet, transform.position, Quaternion.LookRotation(transform.forward)); ;
            bulletGO.GetComponent<Bullet>().targetPos = transform.position + transform.forward * distance;
            StartCoroutine(Dashing());
        }

        private IEnumerator Dashing()
        {
            float startDashTime = 0;
            while(startDashTime < dashDuration)
            {
                ultimateCharacterLocomotion.MoveDirection = Vector3.right * -1;
                ultimateCharacterLocomotion.Move(0, dashSpeed * Time.deltaTime, 0);
                yield return null;
                startDashTime += Time.deltaTime;
            }
        }
    }
}