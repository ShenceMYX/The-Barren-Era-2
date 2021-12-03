using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class DragDropUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Canvas canvas;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = transform.root.GetComponent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = .6f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            Debug.Log("OnEndDrag");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }

        public void OnDrop(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}
