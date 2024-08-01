using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class DoubleClickEvent : UnityEvent { }

public class CustomEventTrigger : MonoBehaviour, IPointerClickHandler
{
    public DoubleClickEvent onDoubleClick = new DoubleClickEvent();
    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f; // ダブルクリックとみなす時間間隔

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // ダブルクリックが検出された場合の処理
                onDoubleClick.Invoke();
            }
            lastClickTime = Time.time;
        }
    }
}
