using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class ChipPortController : MonoBehaviour
    {
        public int currentAvaiblePortCount;
        public int maxPortCount { get; private set; }

        private void Start()
        {
            maxPortCount = transform.childCount;
            currentAvaiblePortCount = maxPortCount;
        }

    }
}