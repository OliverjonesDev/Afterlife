
using UnityEngine;

public abstract class PlayerBaseStateAbstract
{
    public abstract void EnterState(PlayerController player);
    public abstract void UpdateState(PlayerController player);

    public abstract void ConfirmState(PlayerController player);
    public abstract void Pause(PlayerController player);
    public abstract void ExitState(PlayerController player);
    public abstract void PlayAudio(bool isMoving);
}
