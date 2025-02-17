using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PassiveAbilitiesCard
{
    public abstract string GetPassiveAbilityText();
    public abstract void ActivatePassiveAbilities();
}
