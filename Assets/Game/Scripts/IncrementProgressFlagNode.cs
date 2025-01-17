using UnityEngine;

[CreateNodeMenu("ProgressFlag/IncrementProgressFlag")]
public class IncrementProgressFlagNode : InteractionActionNode
{
    [SerializeField] private string flagName;
    [SerializeField] private int amount = 1;
    
    public override void Execute(Interactable interactable)
    {
        var flagValue = GameManager.Instance.GetProgressFlagValue(flagName, 0);
        flagValue += amount;
        GameManager.Instance.SetProgressFlag(flagName,flagValue);
    }
}
