using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject LookAtPoint;
    [SerializeField] private PlayerController Player;
    [SerializeField] private Interactable PlayerAnim;
    [SerializeField] private Interactable fadeToBlack;
    [SerializeField] private float cameraZoomSpeed = 0.02f;
    bool enteredTrigger;
    private float timer;
    [SerializeField] float timerUntilCamZoom = 8;
    [SerializeField] float fovLerpedToo = 10;
    [SerializeField] float fovUntilLevelLoaded = 20;
    [SerializeField] private float delayForLevelLoad = 8;
    [SerializeField] private AudioClip clip;
    

    private void Update()
    {
        if (enteredTrigger)
        {

            timer += Time.deltaTime;
            Camera.main.transform.LookAt(LookAtPoint.transform);
            if (timer > timerUntilCamZoom)
                Player.cameraZoomWithSpeed(fovLerpedToo,cameraZoomSpeed);
            if ( Camera.main.fieldOfView <= fovUntilLevelLoaded)
            {
                StartCoroutine(loadNewScene());   
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.playerZoomed = true;
        Player.ChangePlayerState(new PlayerBaseState());
        PlayerAnim.interactedWith = true;

        //Trigger Audio
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PauseWalking();
        AudioManager.Instance.PlaySFX(clip);
        
        enteredTrigger = true;
    }
    IEnumerator loadNewScene()
    {
        fadeToBlack.interactedWith = true;
        yield return new WaitForSeconds(delayForLevelLoad);
        SceneManager.LoadScene("MainMenu");
    }
}
