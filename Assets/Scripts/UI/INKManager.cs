using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class INKManager : MonoBehaviour
{//JSON text asset to be used for 
    [HideInInspector]
    public bool scrollInProgress = false;
    [SerializeField]
    private List<TextAsset> inkJSONDays;
    [SerializeField]
    private TextAsset inkEnd;
    [HideInInspector]
    //Json file is loaded into this Story Component where we can access and progress the text
    public Story story;
    [SerializeField]
    //takes an object with both the ConsoleText Script and a TextMeshPro text script to display a console
    private ConsoleText consoleDisplay;
    [SerializeField]
    private TextMeshProUGUI textPrefab;
    [SerializeField]
    private TextMeshProUGUI charaNameText;
    [SerializeField]
    private Button buttonPrefab;
    [SerializeField]
    private Transform buttonManager;
    private Transform  textManager;
    private int currentDay;
    private Image black;
    private List<int> choicesChosen = new List<int>();
    [SerializeField]
    PlayerController player;
    [SerializeField]
    private GameMenuManager gameMenuManager;
    private bool loadTextEnd = false;

    private enum judgeResult
    {
        goodHeaven = 0,
        badHeaven = 1,
        goodHell = 2,
        badHell = 3
    }
    private enum characterOrDay
    {
        character = 0,
        day = 1,
        end = 2
    }


    void Start()
    {
        gameMenuManager = GameObject.FindObjectOfType<GameMenuManager>();
        black = transform.Find("FadeBox").GetComponent<Image>();
        textManager = transform.Find("Text Manager");
        currentDay = 0;
        if (inkJSONDays != null)
        {
            if (inkJSONDays[currentDay] == null)
            {
                Debug.LogError("No Story Assigned to InkManager(Text Asset)");
                return;
            }
            else
            {
                story = new Story(inkJSONDays[currentDay].text);
            }    
        }
        //Destroys and then Creates UI elements to update them
        UIHandler();
        

    }
    private void FixedUpdate()
    {
    }
    public void UIHandler()
    {
        if (AnyNull(story, buttonPrefab, textPrefab, charaNameText, buttonManager))
        {
            if (story == null) Debug.LogError("No Story assigned to InkManager");
            if (buttonPrefab == null) Debug.LogError("No buttonPrefab assigned to InkManager");
            if (textPrefab == null) Debug.LogError("No textPrefab assigned to InkManager");
            if (charaNameText == null) Debug.LogError("No Character Name assigned to InkManager");
            if (buttonManager == null) Debug.LogError("No Button Manager assigned to InkManager(located in the Button Canvas Prefab)");
            return;
        }
        
        //deletes dialogue text and buttons
        EraseUI();
        //loads dialogue text and buttons
        RefreshUI();
    }
    void EraseUI()
    {
        //Destroys all buttons
        foreach(Transform child in buttonManager.transform)
        {
            Destroy(child.gameObject);
        }
        //Destroys all children to text manager
        textPrefab.text = null;
    }
    void RefreshUI()
    {
        bool addText = true;
        string currentText;
        string sceneName;
        List<string> currentTags;
        string charaName = "name:";
        //TextMeshProUGUI storyText = Instantiate(textPrefab, textManager);
        //loads dialogue text
        currentText = LoadStoryChunk();
        StartCoroutine(LoadText(currentText));
        //checks if text has any tags assigned to it
        if (story.currentTags.Count > 0)
        {
            currentTags = story.currentTags;
            //goes through all tags and sees if any start with "name:", if so it will be assigned to the character name text
            foreach (string tags in currentTags)
            {
                if (tags == "move:nextJSON")
                {
                    StartCoroutine(NextCharacter(characterOrDay.day, null, true));
                }
                if (tags == "ignore")
                {
                    addText = false;
                }
                if (tags.StartsWith("name:"))
                {
                    charaName = tags;
                }
                if (tags == ("changeState:Standing"))
                {
                    player.ChangePlayerState(player.standingState);
                }
                if (tags.StartsWith("moveNextScene:"))
                {
                    sceneName = tags;
                    sceneName = sceneName.Remove(0, 14);
                    StartCoroutine(gameMenuManager.loadScene(1, sceneName));
                }
            }
        }
        if (addText)
        {
            if (consoleDisplay == null){
                Debug.LogWarning("no consoleDisplay(ConsoleText) assigned to InkManager(found in TextCanvas)");
                return;
                    }
            if (charaName.StartsWith("name:"))
            {
                charaName = charaName.Remove(0, 5);
                consoleDisplay.AddText(charaName, true);
            }
            else
            {
                consoleDisplay.AddText(charaName, false);
            }
            charaNameText.text = charaName;
            consoleDisplay.AddText(currentText, false);
        }

        
        //storyText.transform.SetParent(textManager, false);
        //goes through each choice current in the current story position and creates buttons for them, questionNum is for choice distinction so it can be accessed from PlayerController
        foreach (Choice choice in story.currentChoices)
        {
            player.delayPassed = false;
            //bool to decide if a choice is a one time choice to be inactive after it's been selected
            bool oneTime = false;
            Button choiceButton = Instantiate(buttonPrefab, buttonManager);
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            int choiceIndex = choice.index;
            if (choice.tags != null)
            {
                
                //gets tags assigned to choice
                List<string> choiceTags = (choice.tags).ToList();
                foreach (string tag in choiceTags)
                {
                    //Greys out dialogue choices with the "Inactive" tag which is assigned to the choice when it's selected
                    if (tag == "Inactive")
                    {
                        choiceButton.interactable = false;
                    }
                    if (tag == "oneTime")
                    {
                        oneTime = true;
                    }
                    foreach (int choiceComparison in choicesChosen)
                    {
                        if (choiceComparison == choiceIndex && oneTime)
                        {
                            choiceButton.interactable = false;
                        }
                    }
                }
            }
            //sets the text on the button to match the text on the line of the choice
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(buttonManager, false);
            //assigns a trigger to the button we've created, the trigger selects the choice linked to the button
            choiceButton.onClick.AddListener(delegate
            {
                if (oneTime)
                {
                    choicesChosen.Add(choiceIndex);
                    
                }
                player.ChangePlayerState(player.dialogueState);
                ChooseStoryChoice(choice);
                
            });
        }
        consoleDisplay.UpdateText();
    }
    IEnumerator LoadText(string fullText)
    {
        scrollInProgress = true;
        foreach (char c in fullText)
        {
            if (!scrollInProgress)
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
            textPrefab.text +=c;
            if(c != ' ') AudioManager.Instance.PlaySubtitleAudio();
            
        }
        textPrefab.text = fullText;
        scrollInProgress = false;
    }
    void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        UIHandler();
    }
    string LoadStoryChunk()
    {
        //prevents story from going on if there're choices on screen, will return the current text on screen
        if (story.currentChoices.Count > 0)
        {
            return story.currentText;
        }
        //loads next set of dialogue, leaves an empty string if it has run out
        string text = "";
        //if the story can continue it'll load the next piece of text available
        if (story.canContinue)
        {
            text = story.Continue();
        }
        /*temporary judging system trigger until we attach it to the physical buttons, triggers when 
         * you're at the end of the dialogue for the character currently, also clears the text */
        else
        {
            /*
             true = heaven
             false = hell
             */
            return "";
        }
        return text;
    }
    //heaven or hell functionality
    public void Judge(bool sendHeaven)
    {
        if (AnyNull(player, story)) {
            if (story == null) Debug.LogError("No Story assigned to InkManager");
            if (player == null) Debug.LogError("No Player assigned to InkManager");
            return;
        };

        
        //gets isGood and nextCharacter variable from inkJSON
        judgeResult result = judgeResult.goodHeaven;
        string nextCharacter = "error";
        bool isGood = false;
        try
        {
            isGood = (bool)story.variablesState["isGood"];
        }
        catch(System.Exception ex)
        {
            
            Debug.LogException(ex);
            Debug.LogError("isGood Variable not set in ink, assumed bad");
        }
        try
        {
             nextCharacter = (string)story.variablesState["nextCharacter"];
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            Debug.LogError("nextCharacter not set, Judge() aborted");
            return;
        }

        if (sendHeaven && isGood) { result = judgeResult.goodHeaven; }
        else if (sendHeaven && !isGood) { result = judgeResult.badHeaven; }
        else if (!sendHeaven && isGood) { result = judgeResult.goodHell; }
        else if (!sendHeaven && !isGood) { result = judgeResult.badHell; }
        
        //will handle special events based on whether they were good or not
        EventManager(result, sendHeaven);
        //next character is set to the string "null" in Ink if we're on the last person for the day
        if (nextCharacter != "null")
        {
            //if it's not null it'll go to the knot referenced in nextCharacter, which will be our next character
            StartCoroutine(NextCharacter(characterOrDay.character, nextCharacter, sendHeaven));
        }
        //goes to the next inkJSON if we're through all characters in a day and there's still days left to go through
        else if (inkJSONDays.Count > currentDay+1)
        {
            StartCoroutine(NextCharacter(characterOrDay.day, null, sendHeaven));
        }
        else
        {
            StartCoroutine(NextCharacter(characterOrDay.end, null, sendHeaven));
        }
    }
    void EventManager(judgeResult sendState, bool sendHeaven)
    {
        //change eventTag in Ink script at the start of the character's dialogue to tie it to an event that's triggered on judging them
        string eventTag = (string)story.variablesState["eventTag"];
        if (eventTag == "null") { return; }
        else if (sendState == judgeResult.goodHeaven) 
        {
            //check for event tags here if the tag is tied to them being both good and sent to heaven
        }
        else if (sendState == judgeResult.badHeaven) {
            //check for event tags here if the tag is tied to them being both bad and sent to heaven
        }
        else if (sendState == judgeResult.goodHell) {
            //check for event tags here if the tag is tied to them being both good and sent to hell
        }
        else if (sendState == judgeResult.badHell) {
            //check for event tags here if the tag is tied to them being both bad and sent to hell
        }
    }
    //Might Need to move this to another script, where it's more applicable
    IEnumerator NextCharacter(characterOrDay next, string nextCharacter, bool sendHeaven)
    {
        choicesChosen = new List<int>();
        EraseUI();
        charaNameText.text = "Receptionist";
        player.ChangePlayerState(player.dialogueState);
        player.input.PlayerControls.Confirm.Disable();
        if (sendHeaven) StartCoroutine(LoadText((string)story.variablesState["endTextHeaven"]));
        if (!sendHeaven) StartCoroutine(LoadText((string)story.variablesState["endTextHell"]));
        yield return new WaitForSeconds(10f);
        player.ChangePlayerState(player.lookingState);
        Color currentAlpha = black.color;
        for (float alpha = 0f; alpha <= 1.01f; alpha += 0.05f)
        { 
            currentAlpha.a = alpha;
            black.color = currentAlpha;
            yield return new WaitForSeconds(.01f);
        }
        player.ChangePlayerState(player.dialogueState);
        
        yield return new WaitForSeconds(1f);
        
        /*yield return new WaitUntil(() => aInstance.IsInitialised);
         * about this line of code, we need a way to check if a batch of objects 
         * have loaded or basically whatever we want in the scene, IsInitialised will be set to true
        */
        if (next == characterOrDay.character)
        {
            story.ChoosePathString(nextCharacter);
            UIHandler();
            consoleDisplay.ClearText();
            consoleDisplay.AddText(story.currentText, false);
        }
        else if (next == characterOrDay.day) {
            currentDay++;
            story = new Story(inkJSONDays[currentDay].text);
            UIHandler();
            consoleDisplay.ClearText();
            consoleDisplay.AddText(story.currentText, false);
        }
        else if (next == characterOrDay.end)
        {
            story = new Story(inkEnd.text);
            UIHandler();
            consoleDisplay.ClearText();
            consoleDisplay.AddText(story.currentText, false);

        }
        player.ChangePlayerState(player.dialogueState);
        if(next != characterOrDay.end)
        {
            for (float alpha = 1f; alpha >= 0f; alpha -= 0.05f)
            {
                currentAlpha.a = alpha;
                black.color = currentAlpha;
                yield return new WaitForSeconds(.01f);
            }
            currentAlpha.a = 0;
        }
        
        black.color = currentAlpha;
        player.input.PlayerControls.Confirm.Enable();
    }
    bool AnyNull(params object[] objects)
    {
        return objects.Any(o => o == null);
    }
}
