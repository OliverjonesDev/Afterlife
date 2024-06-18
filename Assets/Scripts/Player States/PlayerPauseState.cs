using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseState : PlayerBaseState
{
    private GameMenuManager _gameMenuManager;

    public override void EnterState(PlayerController player)
    {
        _gameMenuManager = GameObject.FindObjectOfType<GameMenuManager>();
        _gameMenuManager.gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public override void UpdateState(PlayerController player)
    {
    }

    public override void Pause(PlayerController player)
    {
        player.ChangePlayerState(player.lookingState);
    }

    public override void ExitState(PlayerController player)
    {

        _gameMenuManager.gamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
