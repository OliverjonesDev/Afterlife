using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SetAmbienceTrigger : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    //When player enters the trigger, ensure there is an audio clip and change room tones.
    private void OnTriggerEnter(Collider other)
    {
        Assert.IsTrue(clip);
        AudioManager.Instance.SwitchRoomTone(clip);
    }
}
