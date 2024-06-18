using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogueState : PlayerBaseState
{
    
    public override void EnterState(PlayerController player) {
        foreach (Transform child in player.inkController.transform)
        {
            if (!(child.name == "FadeBox" || child.name == "Crosshair"))
            {
                child.gameObject.SetActive(true);
            }
            if(child.name == "Crosshair") child.gameObject.SetActive(false);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public override void UpdateState(PlayerController player) {
        player.lookVector.y = Mathf.Clamp(player.lookVector.y + player.camLook.ReadValue<Vector2>().x * player.sensitivityX, player.yClamp.x, player.yClamp.y);
        player.lookVector.x = Mathf.Clamp(player.lookVector.x - player.camLook.ReadValue<Vector2>().y * player.sensitivityY, -85, 90);
        player.playerCam.rotation = Quaternion.Euler(player.lookVector.x, player.lookVector.y, 0);
    }
    public override void ConfirmState(PlayerController player)
    {
        if(player.inkController.story == null)
        {
            Debug.LogError("No Story Assigned to InkManager/InkManager Not assigned to Player");
            return;
        }
        if (player.inkController.story.currentChoices.Count == 0 && player.inkController.scrollInProgress == false) { 
            player.inkController.UIHandler();
            player.delayPassed = false;
        }
        else if (player.inkController.scrollInProgress == true)  player.inkController.scrollInProgress = false; 
        else if (player.inkController.story.currentChoices.Count > 0)  player.ChangePlayerState(player.lookingState); 

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
