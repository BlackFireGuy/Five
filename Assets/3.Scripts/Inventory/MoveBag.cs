using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MoveBag : MonoBehaviour,IDragHandler
{
    RectTransform currentRect;
    public void OnDrag(PointerEventData eventData)
    {
        //currentRect.anchoredPosition += eventData.delta;
        //暂时不需要了
        
    }
    void Awake()
    {
        currentRect = GetComponent<RectTransform>();

    }

}
