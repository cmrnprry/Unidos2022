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
    public GameObject hideSpot;

    public List<RecipeStepScriptableObject> recipeSteps;

    public List<RecipeStepScriptableObject> currentSteps;

    public bool waitingForNextStep;
    
    public bool recipesFinished;
    public KitchenTool? lastToolHovered;
    public RecipeItem? currentItem;

    public RecipeItem? choppingItem;

    private string debugTxt = "food - {0} spot - {1}"; 
    public TextMeshProUGUI textbox;

    public ChoppingMinigame mg;
    //not terribly happy about how this works but idk it works i guess
    void Start()
    {
        waitingForNextStep = true;
        recipesFinished = false;
       
        //NextStep(2);
    }

    void Update()
    {
        if(waitingForNextStep)
        {
            //update dialogue manager we are done
            
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
                    if(tmp.First().shouldHide)
                    {
                        //hide item?
                        currentItem.HideFood();
                    }
                    currentSteps.Remove(tmp.First());
                    //update tool to be next image?
                    
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
    void NextStep(int steps)
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

}
