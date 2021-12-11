using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class UpdateBodyPart : MonoBehaviour
	{
        private Slot[] slots;
        private Transform bodyPartUIsTrans;
        private GameObject playerModel;

        private void OnEnable()
        {
            playerModel = GameObject.FindGameObjectWithTag("Model");
            GetComponent<OnClickTransferScene>().TransSceneHandler += UpdatePlayerBodyPart;
            bodyPartUIsTrans = transform.root.FindChildByName("Body Part UIs");
        }

        private void UpdatePlayerBodyPart()
        {
            playerModel.SetActive(true);
            //slots = transform.root.FindChildByName("Slots").GetComponentsInChildren<Slot>();
            //foreach (var slot in slots)
            //{
            //    Transform targetParent = playerModel.transform.FindChildByName(slot.name.Replace("_slot", string.Empty));
            //    Debug.Log(targetParent.name);
            //    for (int i = 0; i < targetParent.childCount; i++)
            //    {
            //        Debug.Log(targetParent.GetChild(i).name);

            //        if (bodyPartUIsTrans.Find(targetParent.GetChild(i).name.Trim() + "_icon") != null)
            //        {
            //            Debug.Log(targetParent.GetChild(i).name + "_icon");
            //            targetParent.GetChild(i).gameObject.SetActive(true);
            //        }
            //        else
            //        {
            //            targetParent.GetChild(i).gameObject.SetActive(false);
            //        }

            //    }
            //}


            for(int i =0; i <playerModel.transform.childCount;i++)
            {
                Transform targetParent = playerModel.transform.GetChild(i);
                for (int j = 0; j < targetParent.childCount; j++)
                {
                    if (bodyPartUIsTrans.Find(targetParent.GetChild(j).name.Trim() + "_icon") != null)
                    {
                        targetParent.GetChild(j).gameObject.SetActive(true);
                    }
                    else
                    {
                        targetParent.GetChild(j).gameObject.SetActive(false);
                    }

                }
            }
        }

        private void OnDisable()
        {
            GetComponent<OnClickTransferScene>().TransSceneHandler -= UpdatePlayerBodyPart;
        }
    }
}