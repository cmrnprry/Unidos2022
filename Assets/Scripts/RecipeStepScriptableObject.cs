using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RecipeStepScriptableObject", order = 1)]
public class RecipeStepScriptableObject : ScriptableObject
{
    public Food startedFood;
    public KitchenPoint neededSpot;
    public bool isChoppingGame;
}
