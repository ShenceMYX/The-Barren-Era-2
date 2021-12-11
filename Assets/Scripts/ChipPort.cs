using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class ChipPort : MonoBehaviour
	{
        private ChipPortController portController;
        public WireController occupiedWire;
        private DragDropUI bodyPartChip;

        private void Start()
        {
            portController = transform.parent.GetComponent<ChipPortController>();
            bodyPartChip = portController.transform.parent.parent.GetComponent<DragDropUI>();

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (occupiedWire != null) return;
            if (collision.CompareTag("Wire"))
            {
                WireController wireController = collision.transform.parent.parent.parent.GetComponent<WireController>();
                //Debug.Log(name + " enter: count: " + portController.currentPortCount);
                if (portController.currentAvaiblePortCount - 1 >= 0)
                {
                    portController.currentAvaiblePortCount--;
                    wireController.connectedPortCount++;
                    occupiedWire = wireController;
                    if (!occupiedWire.connectedChipNameDIC.ContainsKey(bodyPartChip.name))
                    {
                        occupiedWire.connectedChipNameDIC.Add(bodyPartChip.name, bodyPartChip);
                    }
                    //Debug.Log(name + " enter: " + collision.name + ", occupied: " + occupiedWire);
                }

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Wire"))
            {
                WireController wireController = collision.transform.parent.parent.parent.GetComponent<WireController>();
                if (wireController == occupiedWire)
                //if (portController.currentPortCount - collision.GetComponent<WireController>().wirePortCount >= 0)
                {
                    portController.currentAvaiblePortCount++;
                    wireController.connectedPortCount--;
                    if (!occupiedWire.connectedChipNameDIC.ContainsKey(bodyPartChip.name))
                        occupiedWire.connectedChipNameDIC.Remove(bodyPartChip.name);
                    occupiedWire = null;
                    
                    //Debug.Log(name + " exit: " + collision.name + ", occupied: " + occupiedWire);
                }
            }
        }

    }
}