using XNode;

public class HasItemNode : InteractionNode
{
    [Input] public InteractionNodeConnection previous;
    
    [Output(connectionType:ConnectionType.Override)]
    public InteractionNodeConnection @true;
    
    [Output(connectionType:ConnectionType.Override)]
    public InteractionNodeConnection @false;

    public string itemId;
    
    public override NodePort GetNextPort(Interactable interactable)
    {
        return GameManager.Instance.InventoryItem == itemId ? GetPort(nameof(@true)) : GetPort(nameof(@false));
    }

    public override NodePort GetPreviousPort()
    {
        return GetPort(nameof(previous));
    }
}