using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

//By Oliver Jones
public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject blurBackground;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private AnimatableXYZ uiInteraction;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private Interactable fadeToBlack;
    private Material blurMaterial;
    public bool gamePaused;
 
    [SerializeField]
    private int blurAmount = 7 ;
    [SerializeField]
    private int blurTimer = 3 ;
    

    private void Awake()
    {
        if (blurBackground)
        {
            blurMaterial = blurBackground.gameObject.GetComponent<SpriteRenderer>().material;
        }
    }

    private void Update()
    { 
        PauseGame();   
    }
    public void Continue()
    {
        playerController.ChangePlayerState(playerController.lookingState);
        gamePaused = false;
    }
    public void Options()
    {
        optionsMenu.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }
    public void Quit()
    { 
        StartCoroutine(loadScene(1,mainMenuScene));
    }
    public IEnumerator loadScene(float loadSceneDelay,string sceneToLoad)
    {
        fadeToBlack.interactedWith = true;
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        Debug.Log("New Scene Loaded");
    }

    private void PauseGame()
    {
            if (gamePaused)
            {
                blurMaterial.SetFloat("_Size", Mathf.Lerp(blurMaterial.GetFloat("_Size"), blurAmount, blurTimer * Time.unscaledDeltaTime));
                uiInteraction.InteractionBehaviour();
                uiInteraction.exitInteraction = false;
                uiInteraction.interactedWith = true;
            }
            else
            {
                blurMaterial.SetFloat("_Size", Mathf.Lerp(blurMaterial.GetFloat("_Size"), -1, blurTimer * Time.unscaledDeltaTime));
                uiInteraction.InteractionExitBehaviour();
                uiInteraction.exitInteraction = true;
                uiInteraction.interactedWith = false;
                CloseOptions();
            }   
            if (blurMaterial.GetFloat("_Size") < .025)
            {
                blurBackground.SetActive(false);
            }
            else
            {
                blurBackground.SetActive(true);
            }
    }
}
