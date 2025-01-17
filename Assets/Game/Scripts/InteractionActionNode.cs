using XNode;

public abstract class InteractionActionNode : InteractionNode, IActionNode
{
    [Input]
    public InteractionNodeConnection previous;
    
    [Output(connectionType:ConnectionType.Override)]
    public InteractionNodeConnection next;
    
    public override NodePort GetNextPort(Interactable interactable) => GetPort(nameof(next));
    
    public override NodePort GetPreviousPort() => GetPort(nameof(previous));
    
    public abstract void Execute(Interactable interactable);
    
}
