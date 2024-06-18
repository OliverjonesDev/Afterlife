using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmed by Oliver Jones - Student Code
//3376
public class PlayerStandingState : PlayerBaseState
{
    
    private Vector3 playerPos = Vector3.zero;

    public override void EnterState(PlayerController player)
    {
        foreach (Transform child in player.inkController.transform)
        {
            if (child.name == "Crosshair") child.gameObject.SetActive(true);

        }
        player.eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.StartCoroutine(player.Delay(1));
        Debug.Log(("Standing State Entered;"));
        if (playerPos == Vector3.zero)
        {
            playerPos = player.transform.position;   
        }
    }
    public override void UpdateState(PlayerController player)
    {
        player.lookVector.y += player.camLook.ReadValue<Vector2>().x * player.sensitivityX;
        player.lookVector.x = Mathf.Clamp(player.lookVector.x - player.camLook.ReadValue<Vector2>().y * player.sensitivityY, -85, 90);
        player.playerCam.rotation = Quaternion.Euler(player.lookVector.x, player.lookVector.y, 0);
        player.transform.position = Vector3.Lerp(player.transform.position,new Vector3(player.transform.position.x,playerPos.y + 0.15f,player.transform.position.z), 5 * Time.deltaTime);
        float targetAngle = Mathf.Atan2(player.moveVector.x, player.moveVector.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        moveDirection = moveDirection.normalized;

        //move character and trigger walking audio
        if (player.moveVector != Vector2.zero)
        {
            player._CharacterController.Move( moveDirection * (player.speed * Time.deltaTime));
            AudioManager.Instance.isWalking = true;
        }
        else
        {
            AudioManager.Instance.isWalking = false;
        }
    }
    public override void ConfirmState(PlayerController player)
    {
        if (player.delayPassed == true) {
            player.objectSelector.Select();
            //player.delayPassed = false;
    }

    }
    public override void Pause(PlayerController player)
    {
        player.ChangePlayerState(player.pauseState);
    }
    public override void ExitState(PlayerController player)
    {
        foreach (Transform child in player.inkController.transform)
        {
            if (!(child.name == "FadeBox" || child.name == "Crosshair"))
            {
                child.gameObject.SetActive(false);
            }
            if (child.name == "Crosshair") child.gameObject.SetActive(false);

        }
        player.eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void PlayAudio(bool isMoving){
        if(isMoving == true) AudioManager.Instance.PlayWalking();
        if(isMoving == false) AudioManager.Instance.PauseWalking();
    }
}
