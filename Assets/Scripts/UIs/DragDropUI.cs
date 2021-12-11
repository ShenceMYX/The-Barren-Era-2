using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class DragDropUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Canvas canvas;
        public RectTransform possessionParentRT;
        public RectTransform bodyPartUIsRT;
        public RectTransform slotsRT;
        private string possessionParentName = "Content";
        private string bodyPartUIsName = "Body Part UIs";
        private string slotsName = "Slots";
        public Slot occupiedSlot;
        private ChipPortController[] portControllers;
        public DragDropUI connectedChipUI;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = transform.root.GetComponent<Canvas>();
            possessionParentRT = transform.root.FindChildByName(possessionParentName).GetComponent<RectTransform>();
            bodyPartUIsRT = transform.root.FindChildByName(bodyPartUIsName).GetComponent<RectTransform>();
            slotsRT = transform.root.FindChildByName(slotsName).GetComponent<RectTransform>();
            portControllers = GetComponentsInChildren<ChipPortController>();
        }

        private void Update()
        {
            //generally blocking the raycast of all DragDropUI object, otherwise, the one on top of the slot will block the raycast detection for the slot (front graphics will block the back graphics raycast in UI event system)
            if(Input.GetMouseButton(0))
                canvasGroup.blocksRaycasts = false;
            if(Input.GetMouseButtonUp(0))
                canvasGroup.blocksRaycasts = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = .6f;
            if (occupiedSlot != null)
            {
                occupiedSlot.ContainedBodyPartUI = null;
                occupiedSlot = null;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            //如果canvas的render mode是screen space - camera则一定要有camera这个参数 才能检测到  如果render mode是Screen Space - Overlay 则camera这个参数应该是null
            if (!RectTransformUtility.RectangleContainsScreenPoint(possessionParentRT, Input.mousePosition, Camera.main))
            {
                transform.SetParent(canvas.transform);
                transform.localScale = Vector3.one * 0.8f;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            bool anyPortIsConnected = false;
            //如果有任何port连接到wire了则证明occupiedWireController一定不是null
            WireController occupiedWireController = null;
            foreach (var port in portControllers)
            {
                if(port.currentAvaiblePortCount == 0)
                {
                    anyPortIsConnected = true;
                    occupiedWireController = port.GetComponentInChildren<ChipPort>().occupiedWire;
                    break;
                }
            }
            if (occupiedSlot == null || !anyPortIsConnected)
                ReturnPositionToPossessions();
            else
            {
                //记录连接的另一个芯片的DragDropUI的引用
                foreach (var key in occupiedWireController.connectedChipNameDIC.Keys)
                {
                    if (key != name)
                    {
                        connectedChipUI = occupiedWireController.connectedChipNameDIC[key];
                    }
                }
                if (connectedChipUI != null)
                {
                    if (connectedChipUI.occupiedSlot != null)
                    {
                        string bodyPartCategoryName = connectedChipUI.occupiedSlot.name.Replace("_slot", string.Empty);
                        //相同重复的相同身体部分命名规则：在Forelimbs里第一个名字不变，第二个变成1；在Hindlimbs里第一个变成2，第二个变成3
                        if (bodyPartCategoryName == "Hindlimbs")
                            name = name.Replace("_icon", "2_icon").Trim();

                        //string.replace causing some space inbewteen, must use trim first before comparision, otherwise always return false
                        Transform bodyPartChipUITrans = null;
                        //for (int i = 0; i < bodyPartUIsRT.childCount; i++)
                        //{
                        //    Debug.Log(bodyPartUIsRT.GetChild(i).name+" , "+ name+":"+(bodyPartUIsRT.GetChild(i).name == name));

                        //    if (bodyPartUIsRT.GetChild(i).name == name)
                        //    {
                        //        bodyPartChipUITrans = bodyPartUIsRT.GetChild(i);
                        //    }
                        //}
                        bodyPartChipUITrans = bodyPartUIsRT.Find(name);
                        if (bodyPartChipUITrans != null)
                        {
                            if (bodyPartCategoryName == "Hindlimbs")
                            {
                                name = name.Replace("2_icon", "3_icon").Trim();

                            }
                            else
                                name = name.Replace("_icon", "1_icon").Trim();
                        }
                    }
                }
                
                
                    

                //if (bodyPartChipUITrans != null)
                //{
                //    DragDropUI theFirstBodyPartUI = bodyPartChipUITrans.GetComponent<DragDropUI>();
                //    string theFirstBodyPartCategoryName = theFirstBodyPartUI.connectedChipUI.occupiedSlot.name.Replace("_slot", string.Empty);
                //    int hindLimbsNameIndex = 2;
                //    //如果第一个bodyPartChip UI的类别是"Hindlimbs"，将其名字改为"2_slot"， 否则第一个类别就是"Forelimbs" 那么名字保持不变
                //    if (theFirstBodyPartCategoryName == "Hindlimbs")
                //    {
                //        //theFirstBodyPartUI.name = theFirstBodyPartUI.name.Replace("_icon", "2_icon");
                //        hindLimbsNameIndex++;
                //    }


                //    if (bodyPartCategoryName == "Forelimbs")
                //    {
                //        if(theFirstBodyPartCategoryName != "Hindlimbs")
                //            name = name.Replace("_icon", "1_icon");
                //    }
                //    else if (bodyPartCategoryName == "Hindlimbs")
                //    {
                //        if(hindLimbsNameIndex == 2)
                //        {
                //            if (bodyPartUIsRT.FindChildByName(name.Replace("_icon", "2_icon")) != null)
                //                hindLimbsNameIndex++;
                //        }
                //        name = name.Replace("_icon", string.Format("{0}_icon", hindLimbsNameIndex));
                //    }
                //}


                transform.SetParent(bodyPartUIsRT);
                if (GetComponent<BodyPart>() != null)
                {
                    string slotName = GetComponent<BodyPart>().GetType().Name + "_slot";
                    if (slotsRT.FindChildByName(slotName) == null)
                    {
                        occupiedSlot.name = slotName;
                        occupiedSlot.transform.SetParent(slotsRT);
                    }
                }
                

            }

        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnDrop(PointerEventData eventData)
        {
            
        }

        public void ReturnPositionToPossessions()
        {
            occupiedSlot = null;
            transform.SetParent(possessionParentRT);
            transform.localScale = (Vector3.one * 0.8f) * 0.75f;
        }


        
    }
}
