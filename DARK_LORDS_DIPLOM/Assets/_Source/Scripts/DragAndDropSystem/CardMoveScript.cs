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

    void Start()
    {
        if (cardInfoScript.GameManager.GameType == GameType.PVP)
        {
            if (cardInfoScript.SelfCard.WhoseCard == WhoseCard.BluePlayer)
            {
                MainCamera = GameObject.Find("CameraPlayer").GetComponent<Camera>(); //Player
                Debug.Log("PLAYER CARD");
            }
            if (cardInfoScript.SelfCard.WhoseCard == WhoseCard.RedPlayer)
            {
                MainCamera = GameObject.Find("CameraEnemy").GetComponent<Camera>(); //Enemy
                Debug.Log("ENEMY CARD");
            }
        }
        else
        {
            MainCamera = Camera.main;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

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
        cardInfoScript.GameManager.RedSpellScreen.gameObject.SetActive(false);
        cardInfoScript.GameManager.BlueSpellScreen.gameObject.SetActive(false);
        transform.SetParent(DeafoultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
