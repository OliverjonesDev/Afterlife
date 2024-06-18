using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInspectState : PlayerBaseState
{
    public override void Pause(PlayerController player)
    {
        player.ChangePlayerState(player.lookingState);
    }
    public override void ConfirmState(PlayerController player)
    {
        player.objectSelector.Select();
        player.ChangePlayerState(player.lookingState);
    }
}
