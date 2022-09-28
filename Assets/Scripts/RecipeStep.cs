using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RecipeStep : MonoBehaviour
{
    //so if we want to do something like, cutting onion has a minigame attached, i think just setting flags on initialize is okay
    
    //wait okay i think this is right, but we still need a way to handle which steps are done...
    //so like onion returns chopped onion, but that is still part of a single step...
    //not loving this, but idk it is something i guess...
    
    //okay i think if recipes are multi step, this is what they have to look like :/
    //public Dictionary<Dictionary<Food, KitchenPoint>, bool> stepCompleted;
    //public bool CheckStepRequirements(Food food, KitchenPoint spot)
    //{
    //    return ((neededFood == food) && (neededSpot == spot)) && minigameFinished;
    //}
    //Okay so like here is where we are. I think we do something like 
    //This is a validator? so like we check to see if the
    
}
