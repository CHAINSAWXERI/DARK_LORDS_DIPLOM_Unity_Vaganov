using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncRectTransform : NetworkBehaviour
{
    /*
    private RectTransform rectTransform;
    private NetworkVariable<Vector2> anchoredPosition = new();
    private NetworkVariable<Vector2> sizeDelta = new();

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (IsOwner)
        {
            // Отправляем данные с "владельца"
            anchoredPosition.Value = rectTransform.anchoredPosition;
            sizeDelta.Value = rectTransform.sizeDelta;
        }
        else
        {
            // Применяем данные на других клиентах
            rectTransform.anchoredPosition = anchoredPosition.Value;
            rectTransform.sizeDelta = sizeDelta.Value;
        }
    }
    */
}