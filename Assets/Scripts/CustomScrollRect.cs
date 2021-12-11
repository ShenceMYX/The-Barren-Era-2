using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class CustomScrollRect : ScrollRect
	{
        //只通过鼠标滚轮进行滑动 将用鼠标拖拽滑动的事件override掉
        //坑：ScrollRect不加一个image组件作为滑动区域的话 只有鼠标在ui图标上才可以进行滑动 如果鼠标在空白处或ui间隙处则无法进行滑动
        public override void OnBeginDrag(PointerEventData eventData)
        {
            //base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            //base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            //base.OnEndDrag(eventData);
        }
    }
}