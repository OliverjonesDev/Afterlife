using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
 *  Simple script to play sounds when user interacts with buttons.
 *  AudioManager must be an instance in the scene.
 */
public class triggerButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound, hoverSound;

    void Start()
    {
        EventTrigger trigger    = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;
        //EventTrigger trigger = GetComponent<EventTrigger>();

        //create the hover entry
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((data) => {OnPointerEnterDelegate((PointerEventData)data); });
        trigger.triggers.Add(hoverEntry);
        
        //create the hover entry
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((data) => {OnPointerClickDelegate((PointerEventData)data); });
        trigger.triggers.Add(clickEntry);
    }

    public void OnPointerEnterDelegate(PointerEventData data)
    {
        Debug.Log("OnPointerHoverDelegate called.");
        AudioManager.Instance.PlayUI(hoverSound);
    }


    public void OnPointerClickDelegate(PointerEventData data){
        Debug.Log("OnPointerClickDelegate called.");
        AudioManager.Instance.PlayUI(clickSound);
    }
}
