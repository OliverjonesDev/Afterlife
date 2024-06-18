using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookingState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        foreach (Transform child in player.inkController.transform)
        {
            if (!(child.name == "FadeBox" || child.name == "Crosshair"))
            {
                child.gameObject.SetActive(false);
            }
            if (child.name == "Crosshair") child.gameObject.SetActive(true);

        }
        player.eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.StartCoroutine(player.Delay(1));
    }
    public override void UpdateState(PlayerController player)
    {
        player.lookVector.y = Mathf.Clamp(player.lookVector.y + player.camLook.ReadValue<Vector2>().x * player.sensitivityX, player.yClamp.x, player.yClamp.y);
        player.lookVector.x = Mathf.Clamp(player.lookVector.x - player.camLook.ReadValue<Vector2>().y * player.sensitivityY, -85, 90);
        player.playerCam.rotation = Quaternion.Euler(player.lookVector.x, player.lookVector.y, 0);
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
}
