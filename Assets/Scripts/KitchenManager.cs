using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public enum Food
{
    Rice,
    Beans,
    Onion,
    ChoppedOnion,
    Garlic,
    ChoppedGarlic,
    OliveOil,
    VegetableOil,
    TomatoSauce,
    Cilantro,
    Salt,
    Water,
    Bouillon,
    BouillonWater,
    Cumin,
    GarlicPepper,
    None
}
public enum KitchenPoint
{
    FryingPan,
    SaucePan,
    Water,
    CuttingBoard
}
public class KitchenManager : Singleton<KitchenManager>
{
    public GameObject hideSpot;

    public List<RecipeStepScriptableObject> recipeSteps;

    public List<RecipeStepScriptableObject> currentSteps;

    public bool waitingForNextStep;
    
    public bool recipesFinished;
    public KitchenTool? lastToolHovered;
    public RecipeItem? currentItem;

    public RecipeItem? choppingItem;

    //need this since one of the recipes is water and we need to transform one and then the other is gonna become a 
    //regular recipe item
    public List<GameObject> waterSource;
    public bool waterFlag = false;
    

    private string debugTxt = "food - {0} spot - {1}"; 
    public TextMeshProUGUI textbox;

    public ChoppingMinigame mg;
    //not terribly happy about how this works but idk it works i guess
    void Start()
    {
        waitingForNextStep = true;
        recipesFinished = false;
       
    }

    void Update()
    {
        if(waitingForNextStep)
        {
            //update dialogue manager we are done
            //DialogueController.instance.WaitFor = false;
            waitingForNextStep = false;
            //waitingForNextStep = false;
            //NextStep(3);
        }
    }
    //wait for step to finish -- 
    //oops all side effects
    public void CheckStep(Food food)
    {
        if(lastToolHovered != null)
        {
            //hate this but gotta love that it works
            Vector3[] corners = new Vector3[4];
            currentItem.rectTransform.GetWorldCorners(corners);
            Rect rec1 = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);

            lastToolHovered.rectTransform.GetWorldCorners(corners);
            Rect rec2 = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);

            if (rec1.Overlaps(rec2))
            {
                var tmp = currentSteps.Where(p => p.startedFood == food && p.neededSpot == lastToolHovered?.point);
                if (tmp.Any())
                {
                    if(lastToolHovered.isChopper)
                    {
                        currentItem.lastPosition = lastToolHovered.transform.position;
                        currentItem.isChopping = true;
                        choppingItem = currentItem;
                    }
                    
                    if (tmp.First().shouldHide)
                    {
                        //hide item?
                        currentItem.HideFood();
                    }
                    
                    currentSteps.Remove(tmp.First());
                    //update tool to be next image?

                    //Okay since we step out of the function we have to do this last i believe
                    if (lastToolHovered.point == KitchenPoint.Water)
                    {
                        //okay we need to handle the water step
                        WaterStep(lastToolHovered);
                    }
                }
            }
            if (currentSteps.Count > 0 && currentSteps[0].isChoppingGame)
            {
                mg.StartMinigame();
            }
            else if (currentSteps.Count == 0)
            {
                waitingForNextStep = true;
            }
            
        }
        if (!waterFlag)
        {
            ResetToolHovered();
        }
        
    }
    public void ResetToolHovered()
    {
        lastToolHovered?.SetOrigParent();
        lastToolHovered = null;
    }
    public void ChangeLastToolHovered(KitchenTool tool)
    {
        lastToolHovered?.SetOrigParent();
        lastToolHovered = tool;
    }
    public void SetCurrentItem(RecipeItem item)
    {
        currentItem = item;
    }
    public bool HasItem()
    {
        return currentItem != null;
    }
    public void ResetItem()
    {
        currentItem = null;
    }
    public void NextStep(int steps)
    {
        waitingForNextStep = false;
        if(recipeSteps.Count > 0)
        {
            currentSteps = recipeSteps.GetRange(0, steps);
            recipeSteps.RemoveRange(0, steps);

            /*
            textbox.text = "";
            
            var tmpString = "";
            foreach (var step in currentSteps)
            {
                //okay adding to it
                tmpString += string.Format(debugTxt, step.startedFood, step.neededSpot);
            }
            textbox.text = tmpString;
            */
        }
        else
        {
            recipesFinished = true;
            //textbox.text = "Done!";
        }
        
    }

    public void ChopDone()
    {
        choppingItem.OnChopped();
        choppingItem = null;
        currentSteps.RemoveAt(0);
    }

    public void WaterStep(KitchenTool kt)
    {
        waterFlag = true;
        //Okay on water step we do two things.
        waterSource.Remove(kt.gameObject);
        kt.ifWater.enabled = true;
        kt.ifWater.TransformFood();
        kt.gameObject.transform.SetParent(kt.ifWater.OrigParent.transform);
        
        var tmpWater = waterSource[0].GetComponent<KitchenTool>();
        tmpWater.ifWater.enabled = true;
        tmpWater.gameObject.transform.SetParent(tmpWater.ifWater.OrigParent.transform);

        kt.enabled = false;
        tmpWater.enabled = false;
        lastToolHovered = null;
    }

}
