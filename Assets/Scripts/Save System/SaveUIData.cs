using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class SaveUIData : MonoBehaviour
	{
        private void OnEnable()
        {
            GetComponent<OnClickTransferScene>().TransSceneHandler += SaveSceneUIData;
        }

        private void SaveSceneUIData()
        {
            //ES3.Save("")
        }

        private void OnDisable()
        {
            GetComponent<OnClickTransferScene>().TransSceneHandler -= SaveSceneUIData;
        }
    }
}