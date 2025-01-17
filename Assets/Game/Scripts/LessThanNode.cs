using XNode;

[CreateNodeMenu("Branching/LessThan")]
public class LessThanNode : InteractionNode
{
    [Input(connectionType = ConnectionType.Override)] public float input;
    [Output(connectionType = ConnectionType.Override)] public InteractionNodeConnection @true;
    [Output(connectionType = ConnectionType.Override)] public InteractionNodeConnection @false;

    public float value;
    
    public override NodePort GetNextPort(Interactable interactable)
    {
        var inputValue = GetInputValue<float>(nameof(input));
        if (inputValue < value)
        {
            return GetPort(nameof(@true));
        }
        return GetPort(nameof(@false));
    }

    public override NodePort GetPreviousPort()
    {
        return GetPort(nameof(input));
    }
}
