using XNode;

[Node.NodeTint(0,0.5f,0f), Node.DisallowMultipleNodes]
public class InteractionStartNode : InteractionNode
{
    [Output(connectionType = ConnectionType.Override)] public InteractionNodeConnection next;

    public override InteractionNode GetNextNode(Interactable interactable)
    {
        return GetConnectedNode(nameof(next));
    }
    
    public override NodePort GetNextPort(Interactable interactable)
    {
        return GetPort(nameof(next));
    }

    public override NodePort GetPreviousPort()
    {
        return null;
    }
}
