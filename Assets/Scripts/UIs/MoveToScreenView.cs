using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class MoveToScreenView : MonoBehaviour, IPointerClickHandler
    {
        public Animator anim;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                anim.SetTrigger("focus");
                GetComponent<BoxCollider>().enabled = false;
            }
        }
        
    }
}