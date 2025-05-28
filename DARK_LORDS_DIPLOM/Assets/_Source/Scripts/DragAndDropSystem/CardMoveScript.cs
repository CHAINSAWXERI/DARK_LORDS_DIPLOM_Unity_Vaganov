using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class CardMoveScript : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //MonoBehaviour
{
    [SerializeField] public CardInfoScript cardInfoScript;
    public Camera MainCamera;
    private Vector3 offset;
    public Transform DeafoultParent;
    public bool isDraggable;

    public void SetCamera()
    {
        if (cardInfoScript == null)
        {
            Debug.LogError("CardInfoScript is null!");
        }

        if (cardInfoScript.GameManager == null)
        {
            Debug.LogError("GameManager is null in CardInfoScript!");
        }

        Debug.Log("SetCamera");

        if (cardInfoScript.GameManager.GameType == GameType.PVP) // строка 19
        {
            if (cardInfoScript.SelfCard.WhoseCard == WhoseCard.BluePlayer)
            {
                //MainCamera = GameObject.Find("CameraPlayer").GetComponent<Camera>(); //Player //23 строка
                GameObject cameraObj = GameObject.Find("CameraPlayer");
                if (cameraObj != null)
                {
                    Camera cameraComponent = cameraObj.GetComponent<Camera>();
                    if (cameraComponent != null)
                    {
                        MainCamera = cameraComponent;
                    }
                    else
                    {
                        Debug.LogError("Camera component not found on cameraObj");
                    }
                }
                else
                {
                    Debug.LogError("cameraObj is null");
                }
            }
            if (cardInfoScript.SelfCard.WhoseCard == WhoseCard.RedPlayer)
            {
                //MainCamera = GameObject.Find("CameraEnemy").GetComponent<Camera>(); //Enemy // 27 строка
                GameObject cameraObj = GameObject.Find("CameraEnemy");
                if (cameraObj != null)
                {
                    Camera cameraComponent = cameraObj.GetComponent<Camera>();
                    if (cameraComponent != null)
                    {
                        MainCamera = cameraComponent;
                    }
                    else
                    {
                        Debug.LogError("Camera component not found on cameraObj");
                    }
                }
                else
                {
                    Debug.LogError("cameraObj is null");
                }
            }
        }
        else
        {
            MainCamera = Camera.main;
        }

        if (MainCamera == null)
        {
            Debug.LogError("CAMERA IS NULL");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position); ///////////////
        DeafoultParent = transform.parent;

        if ((DeafoultParent.GetComponent<DropPlaceScript>().fieldType == FieldType.SELF_HAND) || (DeafoultParent.GetComponent<DropPlaceScript>().fieldType == FieldType.ENEMY_HAND))
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

        if ((cardInfoScript.SelfCard.CardType == CardType.Spell) && (cardInfoScript.SelfCard.WhoseCard == WhoseCard.BluePlayer) && (cardInfoScript.GameManager.IsPlayerTurn == true))
        {
            cardInfoScript.GameManager.BlueSpellScreen.gameObject.SetActive(true);
        }
        if ((cardInfoScript.SelfCard.CardType == CardType.Spell) && (cardInfoScript.SelfCard.WhoseCard == WhoseCard.RedPlayer) && (cardInfoScript.GameManager.IsPlayerTurn == false))
        {
            cardInfoScript.GameManager.RedSpellScreen.gameObject.SetActive(true);
        }
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
        cardInfoScript.GameManager.RedSpellScreen.gameObject.SetActive(false);
        cardInfoScript.GameManager.BlueSpellScreen.gameObject.SetActive(false);
        transform.SetParent(DeafoultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
