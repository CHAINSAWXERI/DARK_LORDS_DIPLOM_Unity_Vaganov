using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMoveScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera MainCamera;
    private Vector3 offset;
    public Transform DeafoultParent;
    public bool isDraggable;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        DeafoultParent = transform.parent;

        if (DeafoultParent.GetComponent<DropPlaceScript>().fieldType == FieldType.SELF_HAND)
        {
            isDraggable = true;
        }
        else
        {
            isDraggable = false;
            return;
        }

        transform.SetParent(DeafoultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }
        transform.SetParent(DeafoultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
