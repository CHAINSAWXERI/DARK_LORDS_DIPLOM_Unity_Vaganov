using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassiveAbilitiesCard
{
    void ActivatePassiveAbilities();
    string GetPassiveAbilityText(); // Новый метод для получения текста
}