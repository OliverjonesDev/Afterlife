using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
//Coded by Oliver Jones
public class FadeToBlack : Interactable{

    [SerializeField]private UnityEngine.UI.Image image;
    [SerializeField] private float fadeSpeed;
    public override void InteractionBehaviour()
    {
        StartCoroutine(Fade(fadeSpeed,true));
    }

    private IEnumerator Fade(float fadeSpeed,bool fadeTo)
    {
        float alpha;
        Color objectColour = image.color;
        if (fadeTo)
        {
            while (objectColour.a < 1)
            {
                alpha = objectColour.a + (fadeSpeed * Time.deltaTime);
                objectColour = new Color(objectColour.r, objectColour.g, objectColour.b, alpha);
                image.color = objectColour;
                yield return null;
            }
        }
        else
        {
            while (objectColour.a >0)
            {
                alpha = objectColour.a - (fadeSpeed * Time.deltaTime);
                objectColour = new Color(objectColour.r, objectColour.g, objectColour.b, alpha);
                image.color = objectColour;
                yield return null;
            }
        }

    }
    private void Awake()
    {
        image.color = Color.black;
        StartCoroutine(Fade(fadeSpeed, false));
    }
}
