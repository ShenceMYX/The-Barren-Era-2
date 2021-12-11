using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Slot : MonoBehaviour, IDropHandler
    {
        //whether the ui slot contain a body part ui
        public DragDropUI ContainedBodyPartUI { get { return containedBodyPartUI; } set { containedBodyPartUI = value; if(containedBodyPartUI!=null) containedBodyPartUI.occupiedSlot = this; } }
        public DragDropUI containedBodyPartUI;

        private DragDropUI[] allBodyPartUIs;

        private void Awake()
        {
            //初始化该Slot是否有DragDropUI在其中 如有DragDropUI占据该Slot 为这个DragDropUI初始化他所占据的Slot
            InitializeSlotsAndBodyPartUIWhetherContainEachOther();
        }

        private void InitializeSlotsAndBodyPartUIWhetherContainEachOther()
        {
            allBodyPartUIs = transform.root.GetComponentsInChildren<DragDropUI>();
            RectTransform rectTransform = GetComponent<RectTransform>();
            //坑：1. RectTransform.rect的x和y很奇怪，width和height倒是正常的 2.RectTransform的anchoredPosition的x和y是计算ui所在rect的中心点，而Rect rect = new Rect（)的这个构造函数的x和y是左上角
            Rect rtRect = new Rect(rectTransform.anchoredPosition.x - rectTransform.rect.width / 2, rectTransform.anchoredPosition.y - rectTransform.rect.height / 2, rectTransform.rect.width, rectTransform.rect.height);
            foreach (var item in allBodyPartUIs)
            {
                if (rtRect.Contains(item.GetComponent<RectTransform>().anchoredPosition))
                {
                    ContainedBodyPartUI = item.GetComponent<DragDropUI>();
                    //认为只可能有一个DragDropUI会占据Slot格子 
                    break;
                }
            }
            //通过属性的set方法 每次只要为其赋值 自动将其赋值的occupiedSlot设为this
            //if (containedBodyPartUI != null) containedBodyPartUI.occupiedSlot = this;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                if (ContainedBodyPartUI == null)
                {
                    ContainedBodyPartUI = eventData.pointerDrag.GetComponent<DragDropUI>();
                    //属性set已经自动设置containedBodyPartUI.occupiedSlot = this;
                    //containedBodyPartUI.occupiedSlot = this;
                }
                else
                {
                    ContainedBodyPartUI.ReturnPositionToPossessions();
                    ContainedBodyPartUI = eventData.pointerDrag.GetComponent<DragDropUI>();
                }

            }
        }
    }
}
