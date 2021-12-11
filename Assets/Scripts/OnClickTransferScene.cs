using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class OnClickTransferScene : MonoBehaviour
	{
		public string transferredSceneName;
		public event Action TransSceneHandler;

        private void OnMouseDown()
        {
			TransSceneHandler?.Invoke();
			GameObject.FindGameObjectWithTag("Model").transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
			SceneManager.LoadScene(transferredSceneName);
        }
    }
}