using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static System.Net.Mime.MediaTypeNames;

public class CardInfoScript : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI PassiveAbilitiesText;
    public int ID;


    public void ShowCardInfo(Card card, int id)
    {
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
        Attack.text = card.Attack.ToString();
        Health.text = card.Health.ToString();
        PassiveAbilitiesText.text = card.PassiveAbilities.GetPassiveAbilityText();
        ID = id;
    }

    private void Start()
    {
        //ShowCardInfo(CardManagerStatic.AllCards[transform.GetSiblingIndex()]);
    }
}
