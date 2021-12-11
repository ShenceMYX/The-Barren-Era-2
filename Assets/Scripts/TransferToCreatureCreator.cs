using System;
using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class TransferToCreatureCreator : MonoBehaviour
	{
        private bool enterOnce = false;
        private bool sceneJustLoaded = false;
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        //如果场景刚加载，玩家在基因编辑器的触发器里（意味着玩家刚从基因编辑工厂出来）则触发器会自动又出发一次，这时不应该让玩家再度进去
        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if(arg0.name == "MainScene")
            {
                sceneJustLoaded = true;
                StartCoroutine(SceneAlreadyLoaded());
            }
        }

        private IEnumerator SceneAlreadyLoaded()
        {
            yield return new WaitForEndOfFrame();
            sceneJustLoaded = false;
        }

        //private static bool enterOnce = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (sceneJustLoaded) enterOnce = true;
                if (enterOnce) return;

                other.transform.root.GetComponent<UnityInput>().DisableCursor = false;
                other.transform.root.GetComponent<UnityInput>().enabled = false;
                other.transform.root.GetComponent<UltimateCharacterLocomotion>().enabled = false;
                other.transform.root.GetComponent<UltimateCharacterLocomotionHandler>().enabled = false;
                SceneManager.LoadScene("Hologram Room");
            }
        }

        private void loadLoevel()
        {
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

    }
}
