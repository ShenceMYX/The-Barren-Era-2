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
    public class DragDropWire : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Canvas canvas;
        public RectTransform possessionParentRT;
        private string possessionParentName = "Content";
        public RectTransform wireParentRT;
        private WireController wireController;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = transform.root.GetComponent<Canvas>();
            possessionParentRT = transform.root.FindChildByName(possessionParentName).GetComponent<RectTransform>();
            wireParentRT = transform.root.FindChildByName("Wire Connections").GetComponent<RectTransform>();
            wireController = GetComponentInChildren<WireController>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = .6f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            if (!RectTransformUtility.RectangleContainsScreenPoint(possessionParentRT, Input.mousePosition, Camera.main))
            {
                transform.SetParent(canvas.transform);
                transform.localScale = Vector3.one;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            Debug.Log(wireController.connectedPortCount + " , " + wireController.wirePortCount);
            if (RectTransformUtility.RectangleContainsScreenPoint(possessionParentRT, Input.mousePosition, Camera.main)
                || wireController.connectedPortCount % wireController.wirePortCount != 0 || wireController.connectedPortCount == 0)
                //如果不是所有导线的端口全部连接到了端口
            {
                ReturnPositionToPossessions();
            }
            else
            {
                transform.SetParent(wireParentRT);
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
            transform.SetParent(possessionParentRT);
            transform.localScale = (Vector3.one) * 0.75f;
        }
    }
}