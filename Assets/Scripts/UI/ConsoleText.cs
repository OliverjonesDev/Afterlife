using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ConsoleText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI characterText;
    [SerializeField] private ScrollRect scrollBar;
    private string currentNameTag;
    private List<string> textChunk = new List<string>();
    private string currentText;
    private string topText;
    private RectTransform rectHeight;
    private float scrollAmount = 500;

    [SerializeField]
    private AudioClip sfx;
    // Start is called before the first frame update
    void Start()
    {
        rectHeight = gameObject.GetComponent<RectTransform>();
        if (characterText == null)
        {
            Debug.LogWarning("no characterText assigned to Console Text(drag this gameobject(Console Text) into the characterText");
            return;
        }
        currentText = "";
        characterText.text = "";
        topText = "";
        currentNameTag = "";
    }
    public void AddText(string textAdd, bool nameTag) {
        if (characterText == null)
        {
            Debug.LogWarning("no characterText assigned to Console Text(drag this gameobject(Console Text) into the characterText");
            return;
        }
        currentText = "";
        string newNameTag;
        string charaTemp = topText;
        if (nameTag)
        {
            if (textAdd == "")
            {
                textAdd = "You";
            }
            newNameTag = textAdd;
            if (newNameTag == currentNameTag)
            {
                return;
            }
            textAdd = "\n" + textAdd + ":\n";
            currentNameTag = newNameTag;
            textChunk.Add(charaTemp);
            charaTemp = "";
        }
        topText = charaTemp + textAdd;
       //for (int i = textChunk.Count-1;i>=0 && i>= textChunk.Count-3;i--)
       // {
       //     currentText = textChunk[i] + currentText;
       //}
       for (int i = textChunk.Count - 1; i >= 0; i--)
        {
            currentText = textChunk[i] + currentText;
        }
        currentText += topText;
    }
    public void UpdateText()
    {
        characterText.text = currentText;
    }
    public void ClearText()
    {
        characterText.text = "";
        currentNameTag = "";
        currentText = "";
        textChunk.Clear();
    }
    public void ScrollDown(bool up)
    {
        float fullHeight = rectHeight.rect.height;
        
        if(scrollBar == null)
        {
            Debug.LogWarning("no scrollRect(Scroll View, 3 parents above) assigned to ConsoleText");
            return;
        }
        if (up)
        {
            float scrollpos = scrollBar.verticalNormalizedPosition;
            Mathf.Clamp(scrollpos += scrollAmount / fullHeight, 0f, 1f);
            
            scrollBar.verticalNormalizedPosition = scrollpos;
        }
        if(!up)
        {
            float scrollpos = scrollBar.verticalNormalizedPosition;
            Mathf.Clamp(scrollpos -= scrollAmount/fullHeight, 0f, 1f);
            scrollBar.verticalNormalizedPosition = scrollpos;
        }

        AudioManager.Instance.PlaySFX(sfx);
    }
    public void Scroll(double axisVal)
    {
        float fullHeight = rectHeight.rect.height;

        if (scrollBar == null)
        {
            Debug.LogWarning("no scrollRect(Scroll View, 3 parents above) assigned to ConsoleText");
            return;
        }
        if (axisVal > 0)
        {
            float scrollpos = scrollBar.verticalNormalizedPosition;
            Mathf.Clamp(scrollpos += scrollAmount / fullHeight, 0f, 1f);

            scrollBar.verticalNormalizedPosition = scrollpos;
        }
        if (axisVal < 0)
        {
            float scrollpos = scrollBar.verticalNormalizedPosition;
            Mathf.Clamp(scrollpos -= scrollAmount / fullHeight, 0f, 1f);
            scrollBar.verticalNormalizedPosition = scrollpos;
        }
    }
}
