using UnityEngine;

public  class PlayerBaseState : PlayerBaseStateAbstract
{
    public override void EnterState(PlayerController player)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void UpdateState(PlayerController player)
    {
    }
    public override void ConfirmState(PlayerController player)
    {

    }
    public override void Pause(PlayerController player)
    {

    }
    public override void ExitState(PlayerController player)
    {
        
    }
    public override void PlayAudio(bool isMoving){}
}
