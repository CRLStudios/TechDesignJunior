using UnityEngine;

public class PickupItem : Interactable
{
    public override bool CanActivate(bool ignoreRunState = false)
    {
        return true;
    }

    public override void Activate()
    {
        Debug.Log("Picked up item");
    }
}
