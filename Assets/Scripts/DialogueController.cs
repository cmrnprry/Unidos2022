using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class DialogueController : MonoBehaviour
{
    /*want to control text:
     * speed
     * size1
     * position
    */

    /*want to ocntrol text box:
     * position
     * movement
     */
    [Header("Ink")]
    public TextAsset ink;
    public float wait = 0.15f;
    private Story story;
    private List<string> currentTags = new List<string>();

    private Coroutine coroutine;

    [Header("TextBoxes")]
    public TextMeshProUGUI textbox;
    public Animator anim;

    [Header("Dialogue")]
    private bool isTyping;
    public bool WaitFor;
    public Sprite[] heads;
    public GameObject exclaimation;
    private string side = "";

    [Header("Scenes")]
    public GameObject[] rooms;
    private int index = 1;  //start in kitchen

    public static DialogueController instance;
    public LivinngRoomManager livingRoom;

    [SerializeField]
    private Button buttonPrefab = null;
    public GameObject choiceHolder;
    public AudioClip talkingSfx; //FF
    private AudioSource audioSource; //FF
    public GameObject settings;
    public GameObject backgrounTextBox;

    public bool FakeClick = false;

    [SerializeField]
    public TextMeshProUGUI speakerHolder; //A TextBox for a Sprite 
    private const string SpriteAdd = "<sprite name=\"{0}\">";

    private void Awake()
    {
        audioSource = GameObject.Find("TalkingSFX").GetComponent<AudioSource>(); //FF
        audioSource.clip = talkingSfx;//FF
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
        //livingRoom = FindObjectOfType<LivinngRoomManager>();
    }

    private void Start()
    {
        story = new Story(ink.text);
        coroutine = StartCoroutine(ShowNextLine());

        story.ObserveVariable("stramerChoice", (string varName, object newValue) =>
        {
            livingRoom.readVars(varName, (bool)newValue);
        });

        story.ObserveVariable("signChoice", (string varName, object newValue) =>
        {
            livingRoom.readVars(varName, (bool)newValue);
        });

        story.ObserveVariable("baloonChoice", (string varName, object newValue) =>
        {
            livingRoom.readVars(varName, (bool)newValue);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settings.SetActive(!settings.activeSelf);
        }
    }

    private IEnumerator ShowNextLine()
    {
        RemoveChildren();
        if (WaitFor)
            yield return new WaitUntil(() => !WaitFor);

        if (exclaimation.activeSelf)
        {
            FadeToBlack(true);
            yield return new WaitForSeconds(1f);

            Tranition();
            exclaimation.SetActive(false);
            textbox.text = "";
            AudioController.instance.SetAudio(index);
            yield return new WaitForEndOfFrame();
            FadeToBlack(false);
            ClearSpeaker();
            yield return new WaitForSeconds(1f);
            anim.gameObject.SetActive(false);
        }

        if (story.canContinue)
        {
            string nextLine = story.Continue().Trim();
            currentTags = story.currentTags;

            if (currentTags.Count > 0)
            {
                CheckTags();
            }

            coroutine = StartCoroutine(incrementText(nextLine, textbox));
        }
        else
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }

            yield return null;

        }
    }

    void Tranition()
    {
        int temp = index;
        switch (side)
        {
            case "left":
                index -= 1;
                break;
            case "right":
                index += 1;
                break;
            default:
                Debug.LogWarning("Transition unclear");
                break;
        }

        rooms[temp].SetActive(false);
        rooms[index].SetActive(true);

        if(index==1)
        {
            KitchenManager.Instance.ResetCabinets();
        }
    }

    void FadeToBlack(bool isFadeOut)
    {
        anim.gameObject.SetActive(true);
        string animation = (isFadeOut) ? "FadeOut" : "FadeIn";
        anim.SetTrigger(animation);
    }

    private void CheckTags()
    {
        foreach (string tag in currentTags)
        {
            tag.ToLower().Replace(" ", "");
            string[] split = tag.Split(':', StringSplitOptions.RemoveEmptyEntries);
            string TAG = split[0];
            switch (TAG)
            {
                case "speaker":
                    string character = split[1];
                    SetSpeaker(character);
                    break;
                case "prompt":
                    side = split[1].Trim();
                    //set the side
                    exclaimation.GetComponent<RectTransform>().anchoredPosition = (side == "left") ? new Vector3(-758, 235, 0) : new Vector3(784, 235, 0);
                    exclaimation.SetActive(true);
                    WaitFor = true;
                    break;

                case "WaitUntil":
                    WaitFor = true;
                    break;
                case "Recipe":
                    KitchenManager.Instance.NextStep(int.Parse(split[1]));
                    break;
                case "RemoveBarrier":
                    Zipper.Instance.barrier.SetActive(false);
                    break;
                case "PictureTime":
                    livingRoom.showPlaces();
                    backgrounTextBox.gameObject.SetActive(false);
                    //turn off text box
                    textbox.gameObject.SetActive(false);
                    //hand written text
                    break;
                case "ButtonTime":
                    Zipper.Instance.zipped = true;
                    break;
                default:
                    Debug.LogError($"Tag not found: {TAG}");
                    break;
            }
        }
    }

    public void SetSpeaker(string character)
    {
        var tmpChar = "";
        switch (character.Trim())
        {

            case "Abuelita":
                tmpChar = "abuelita";
                break;
            case "aunt":
                tmpChar = "aunt";
                break;
            case "aunt-h":
                tmpChar = "aunt-h";
                break;
            case "Cousin1":
                tmpChar = "cousin-1";
                break;
            case "Cousin2":
                tmpChar = "cousin-2";
                break;
            case "uncle":
                tmpChar = "uncle";
                break;
            default:
                //just set it to grandma no harm
                tmpChar = "abuelita";
                break;
        }
        speakerHolder.text = string.Format(SpriteAdd, tmpChar);
    }

    public void ClearSpeaker()
    {
        speakerHolder.text = "";
    }

    public void SetBool(bool value)
    {
        WaitFor = value;
    }

    public IEnumerator incrementText(string text, TMP_Text currentTextbox)
    {
        audioSource.DOFade(0.3f, 0.08f);//FF
        if(!audioSource.isPlaying){//ff
          audioSource.Play();//FF
        }//ff
        //set the text
        isTyping = true;
        currentTextbox.alpha = 0;
        currentTextbox.text = text;

        yield return new WaitUntil(() => currentTextbox.gameObject.activeSelf);
        yield return new WaitForSeconds(0.15f);

        currentTextbox.ForceMeshUpdate();
        TMP_TextInfo textInfo = currentTextbox.textInfo;
        int totalCharacters = currentTextbox.textInfo.characterCount;

        for (int i = 0; i < totalCharacters; i++)
        {
            if (currentTextbox == null)
            {
                break;
            }

            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            TMP_MeshInfo meshinfo = textInfo.meshInfo[materialIndex];

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            var newVertexColors = meshinfo.colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Set all to full alpha
            newVertexColors[vertexIndex + 0].a = 255;
            newVertexColors[vertexIndex + 1].a = 255;
            newVertexColors[vertexIndex + 2].a = 255;
            newVertexColors[vertexIndex + 3].a = 255;

            currentTextbox.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            yield return new WaitForSeconds(wait);
        }
        audioSource.DOFade(0, 0.08f);//FF
        //audioSource.Pause();//FF
        //currentTextbox.alpha = 225;
        isTyping = false;
        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => CheckClicks());
        coroutine = StartCoroutine(ShowNextLine());
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(choiceHolder.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choiceHolder.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        int childCount = choiceHolder.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(choiceHolder.transform.GetChild(i).gameObject);
        }
    }

    bool CheckClicks()
    {
        if(FakeClick || Input.GetButtonDown("Continue"))
        {
            FakeClick = false;
            return true;
        }
        return false;
        
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        StartCoroutine(ShowNextLine());
    }

    
}
