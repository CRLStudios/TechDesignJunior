using XNode;

[NodeTint(0.5f, 0f, 0f)]
public class InteractionExitNode : InteractionNode 
{
    [Input] public InteractionNodeConnection previous;
    
    /// <summary>
    /// Cutscene exit node always ends the cutscene so there is no next node
    /// </summary>
    /// <returns>Always null</returns>
    public override InteractionNode GetNextNode(Interactable interactable)
    {
        return null;
    }
    
    public override NodePort GetPreviousPort()
    {
        return GetPort(nameof(previous));
    }

    public override NodePort GetNextPort(Interactable interactable)
    {
        return null;
    }
}
