using System.Collections;
using XNode;

public class InteractionCutsceneNode : InteractionNode, ICoroutineNode
{
    [Input] public InteractionNodeConnection previous;
    
    [Output(connectionType:ConnectionType.Override)]
    public InteractionNodeConnection next;

    public string cutsceneId;
    
    public override NodePort GetNextPort(Interactable interactable)
    {
        return GetPort(nameof(next));
    }

    public override NodePort GetPreviousPort()
    {
        return GetPort(nameof(previous));
    }

    public IEnumerator Execute(Interactable interactable)
    {
        yield return CutsceneManager.Instance.RunCutscene(cutsceneId);
    }
}
