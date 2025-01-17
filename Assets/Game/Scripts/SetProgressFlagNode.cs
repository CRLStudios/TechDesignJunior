using UnityEngine;

[CreateNodeMenu("ProgressFlag/SetProgressFlag")]
public class SetProgressFlagNode : InteractionActionNode
{
    [SerializeField] private string flagName;
    [SerializeField] private float value;
    
    public override void Execute(Interactable interactable)
    {
        GameManager.Instance.SetProgressFlag(flagName,value);
    }
}
