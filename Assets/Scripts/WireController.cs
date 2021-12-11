using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class WireController : MonoBehaviour
	{
		public int wirePortCount { get; private set; }
		public int connectedPortCount;
		public Dictionary<string, DragDropUI> connectedChipNameDIC = new Dictionary<string, DragDropUI>();

        private void Start()
        {
			wirePortCount = transform.GetChild(transform.childCount - 1).childCount;
        }

		
    }
}