using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Ability", menuName = "ScriptableObjects/PassiveAbilities/AttackAbility")]
public class PassAbiliDiscardedDeckAttack : PassiveAbilitiesCard
{
    public string PassiveAbilitiesText;

    public override string GetPassiveAbilityText()
    {
        return PassiveAbilitiesText;
    }

    public override void ActivatePassiveAbilities()
    {
        Debug.Log("Attack ability activated!");
    }
}