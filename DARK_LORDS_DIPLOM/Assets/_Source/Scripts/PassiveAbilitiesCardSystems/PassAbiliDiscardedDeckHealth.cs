using UnityEngine;

[CreateAssetMenu(fileName = "New Health Ability", menuName = "ScriptableObjects/PassiveAbilities/HealthAbility")]
public class PassAbiliDiscardedDeckHealth : PassiveAbilitiesCard
{
    public string PassiveAbilitiesText;

    public override string GetPassiveAbilityText()
    {
        return PassiveAbilitiesText;
    }

    public override void ActivatePassiveAbilities()
    {
        Debug.Log("Health ability activated!");
    }
}