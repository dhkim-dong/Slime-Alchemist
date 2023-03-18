using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
#if UNITY_EDITOR
        Debug.Log("Begin.Drag");
#endif
    }

    public void OnDrag(PointerEventData eventData)
    {
       transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
      
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
    }  
}
