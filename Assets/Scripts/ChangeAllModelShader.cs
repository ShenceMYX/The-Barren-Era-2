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
	public class ChangeAllModelShader : MonoBehaviour
	{
		public Material originalMat;
		public Material hologramMat;

		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
			if (scene.name == "Hologram Room")
            {
				ChangeAllMaterial(hologramMat);
				transform.parent = GameObject.FindGameObjectWithTag("ModelPivot").transform;
				transform.position = Vector3.zero;
				transform.eulerAngles = Vector3.zero;
				transform.localScale = Vector3.one;
			}
			else if(scene.name == "Retro Screen")
            {
				gameObject.SetActive(false);
            }
            else if (scene.name == "MainScene")
            {
				ChangeAllMaterial(originalMat);
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.Euler(Vector3.zero);
				transform.localScale = Vector3.one;
				transform.root.GetComponent<UnityInput>().DisableCursor = true;
				transform.root.GetComponent<UnityInput>().enabled = true;
				transform.root.GetComponent<UltimateCharacterLocomotion>().enabled = true;
				transform.root.GetComponent<UltimateCharacterLocomotionHandler>().enabled = true;
				transform.root.GetComponent<PlayerCharacterAI>().UpdateSkills();
			}
		}

		public void ChangeAllMaterial(Material mat)
        {

            foreach (var meshRenderer in GetComponentsInChildren<Renderer>())
            {
				meshRenderer.material = mat;
            }
        }

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		
	}
}