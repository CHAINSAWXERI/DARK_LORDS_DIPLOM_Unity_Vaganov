using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMoveScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler // NetworkBehaviour
{
    [SerializeField] public CardInfoScript cardInfoScript;
    [SerializeField] public BigCardShow BigCard;
    public Camera MainCamera;
    private Vector3 offset;
    public Transform DeafoultParent;
    public bool isDraggable;

    void Start()
    {
        if (MainCamera == null)
        {
            if (cardInfoScript.GameManager.GameType == GameType.PVP)
            {
                Camera[] allCameras = FindObjectsOfType<Camera>(true); // true = включая неактивные
                foreach (Camera cam in allCameras)
                {
                    if (cardInfoScript.SelfCard.WhoseCard == WhoseCard.BluePlayer)
                    {
                        if (cam.gameObject.name == "CameraPlayer")
                        {
                            MainCamera = cam;
                            break;
                        }
                    }
                    if (cardInfoScript.SelfCard.WhoseCard == WhoseCard.RedPlayer)
                    {
                        if (cam.gameObject.name == "CameraEnemy")
                        {
                            MainCamera = cam;
                            break;
                        }
                    }
                }
            }
            else
            {
                MainCamera = Camera.main;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BigCard.BigCardFirst.SetActive(false);
        BigCard.CanTriggerFirst = false;

        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
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

        if (DeafoultParent.gameObject.GetComponent<DropPlaceScript>().fieldType == FieldType.ENEMY_HAND || DeafoultParent.gameObject.GetComponent<DropPlaceScript>().fieldType == FieldType.SELF_HAND)
        {
            BigCard.CanTriggerFirst = true;
        }

        /*
        if (DeafoultParent.gameObject.GetComponent<DropPlaceScript>().fieldType == FieldType.ENEMY_FIELD || DeafoultParent.gameObject.GetComponent<DropPlaceScript>().fieldType == FieldType.SELF_FIELD)
        {
            BigCard.CanTriggerSecond = true;
        }
        */

        if (gameObject.GetComponent<CardInfoScript>().SelfCard.CardType == CardType.Creature)
        {
            gameObject.GetComponent<CardInfoScript>().ShowCardInfo(gameObject.GetComponent<CardInfoScript>().SelfCard, gameObject.GetComponent<CardInfoScript>().ID, gameObject.GetComponent<CardInfoScript>().GameManager, gameObject.GetComponent<CardInfoScript>().WhoseCard);
        }

        cardInfoScript.GameManager.RedSpellScreen.gameObject.SetActive(false);
        cardInfoScript.GameManager.BlueSpellScreen.gameObject.SetActive(false);
        transform.SetParent(DeafoultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //[ClientRpc]
    public void UpdateDeafoultParent()
    {
        transform.SetParent(DeafoultParent);
    }
}
