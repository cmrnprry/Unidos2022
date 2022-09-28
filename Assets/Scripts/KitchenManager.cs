using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    Cumin,
    GarlicPepper,
    None
}
public enum KitchenPoint
{
    FryingPan,
    SaucePan,
    Sink,
    CuttingBoard
}
public class KitchenManager : Singleton<KitchenManager>
{

    public List<RecipeStepScriptableObject> recipeSteps;
    public RecipeStepScriptableObject currentStep;
    public bool waitingForNextStep;
    public bool recipesFinished;
    public KitchenTool? lastToolHovered;
    public RecipeItem? currentItem;

    private string debugTxt = "food - {0} spot - {1}"; 
    public TextMeshProUGUI textbox;
    //not terribly happy about how this works but idk it works i guess
    void Start()
    {
        waitingForNextStep = true;
        recipesFinished = false;
        NextStep();
    }

    void Update()
    {
        if(waitingForNextStep)
        {
            waitingForNextStep = false;
            NextStep();
        }
    }
    //wait for step to finish -- 

    public void CheckStep(Food food)
    {
        //hate this but gotta love that it works
        Vector3[] corners = new Vector3[4];
        currentItem.rectTransform.GetWorldCorners(corners);
        Rect rec1 = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);

        lastToolHovered.rectTransform.GetWorldCorners(corners);
        Rect rec2 = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
        
        if (rec1.Overlaps(rec2)){
            if (currentStep.startedFood == food && currentStep.neededSpot == lastToolHovered?.point)
            {
                waitingForNextStep = true;
                //update tool to be next image?
            }
        }
       
        ResetToolHovered();
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
    void NextStep()
    {
        
        waitingForNextStep = false;
        if(recipeSteps.Count > 0)
        {
            currentStep = recipeSteps[0];
            recipeSteps.RemoveAt(0);
            textbox.text = string.Format(debugTxt, currentStep.startedFood, currentStep.neededSpot);
        }
        else
        {
            recipesFinished = true;
            textbox.text = "Done!";
        }
        
    }
}
