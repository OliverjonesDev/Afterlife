using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Coded by Oliver Jones

public class MainMenuUIFunctions : MonoBehaviour
{
    [SerializeField]private string sceneToLoad;
    [SerializeField] private GameObject betaNotice;
    [SerializeField]private Camera camera;
    [SerializeField] private Interactable[] PlayButtonInteractions;
    [SerializeField] private float loadSceneDelay = 1;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseBetaNotice()
    {
        betaNotice.SetActive(false);
    }
    
    public void StartGame()
    {
        for (int i = 0; i < PlayButtonInteractions.Length; i++)
        {
            PlayButtonInteractions[i].interactedWith = true;            
        }

        StartCoroutine(loadScene());

    }
    public void OpenOptions()
    {
        camera.transform.Rotate(Vector3.up,90);
    }
    public void BackToMainMenu()
    {
        camera.transform.Rotate(Vector3.up,-90);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(sceneToLoad,LoadSceneMode.Single);
        Debug.Log("New Scene Loaded");
    }
}
