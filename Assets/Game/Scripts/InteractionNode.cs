using System.Collections;
using UnityEngine;
using XNode;

public abstract class InteractionNode : Node
{
    public virtual InteractionNode GetNextNode(Interactable interactable)
    {
        return GetNextPort(interactable)?.Connection?.node as InteractionNode;
    }
    
    public abstract NodePort GetNextPort(Interactable interactable);
    public abstract NodePort GetPreviousPort();
    
    public override object GetValue(NodePort port)
    {
        return InteractionNodeConnection.Empty;
    }

    protected InteractionNode GetConnectedNode(string portName)
    {
        var port = GetPort(portName);
        return port?.Connection?.node as InteractionNode;
    }
    
    public virtual Color GetTint()
    {
        return Color.white;
    }
}

public interface IActionNode
{
    public void Execute(Interactable interactable);
}

public interface ICoroutineNode
{
    public IEnumerator Execute(Interactable interactable);
}
