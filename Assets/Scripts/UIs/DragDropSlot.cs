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
    public class DragDropSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Canvas canvas;
        public RectTransform possessionParentRT;
        private string possessionParentName = "Content";
        public RectTransform slotsParentRT;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = transform.root.GetComponent<Canvas>();
            possessionParentRT = transform.root.FindChildByName(possessionParentName).GetComponent<RectTransform>();
            slotsParentRT = transform.root.FindChildByName("Slots").GetComponent<RectTransform>();
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
                transform.localScale = Vector3.one * 0.8f;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            if (RectTransformUtility.RectangleContainsScreenPoint(possessionParentRT, Input.mousePosition, Camera.main))
            {
                ReturnPositionToPossessions();
                if (GetComponent<Slot>() != null)
                    Destroy(GetComponent<Slot>());
            }
            else
            {
                transform.SetParent(slotsParentRT);
                if (GetComponent<Slot>() == null)
                    gameObject.AddComponent<Slot>();
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
            transform.localScale = (Vector3.one * 0.8f) * 0.75f;
        }
    }
}