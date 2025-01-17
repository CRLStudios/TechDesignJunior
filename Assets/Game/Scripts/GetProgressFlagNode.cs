using XNode;

[CreateNodeMenu("ProgressFlag/GetProgressFlag")]
public class GetProgressFlagNode : InteractionNode
{
    [Input] public InteractionNodeConnection previous;
    [Output(connectionType = ConnectionType.Override)] public float value;
    
    public string flagName;
    
    public override NodePort GetNextPort(Interactable interactable)
    {
        return GetPort(nameof(value));
    }

    public override NodePort GetPreviousPort()
    {
        return GetPort(nameof(previous));
    }

    public override object GetValue(NodePort port)
    {
        if ( GameManager.Instance == null )
        {
            return 0;
        }
        return GameManager.Instance.GetProgressFlagValue(flagName, 0);
    }
}
