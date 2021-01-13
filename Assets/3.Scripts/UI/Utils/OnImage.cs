using UnityEngine;
using UnityEngine.EventSystems;

public class OnImage : EventTrigger
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Debug.Log("按下" + this.gameObject.name);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        Debug.Log("抬起" + this.gameObject.name);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        Debug.Log("离开" + this.gameObject.name);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Debug.Log("进入" + this.gameObject.name);
    }
}