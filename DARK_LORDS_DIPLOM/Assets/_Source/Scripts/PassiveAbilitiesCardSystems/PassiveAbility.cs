using UnityEngine;

public abstract class PassiveAbility : ScriptableObject
{
    public abstract void Activate(DropPlaceScript dropPlaceOn, CardInfoScript fieldOn, CardInfoScript fieldOpposite, CardInfoScript fieldRight, CardInfoScript fieldLeft, GameManager gameManager); //DropPlaceScript fieldOn, DropPlaceScript fieldOpposite, DropPlaceScript fieldRight, DropPlaceScript fieldLeft
    public abstract string GetAbilityText();
}
