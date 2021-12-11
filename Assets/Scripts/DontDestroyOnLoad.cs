using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class DontDestroyOnLoad : MonoBehaviour
	{
        private static DontDestroyOnLoad playerInstance;
        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (playerInstance == null)
            {
                playerInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
