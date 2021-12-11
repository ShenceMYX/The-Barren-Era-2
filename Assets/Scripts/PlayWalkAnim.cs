using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class PlayWalkAnim : MonoBehaviour
	{
        private Animator anim;
        private float xInput, yInput;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
            if (xInput != 0 || yInput != 0)
                anim.SetBool("walk", true);
            else
                anim.SetBool("walk", false);
        }
    }
}