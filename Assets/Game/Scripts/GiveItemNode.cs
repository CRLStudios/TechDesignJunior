using System.Collections;
using XNode;

public class GiveItemNode : InteractionNode, ICoroutineNode
{
    [Input] public InteractionNodeConnection previous;
    
    [Output(connectionType:ConnectionType.Override)]
    public InteractionNodeConnection next;

    public string itemId;
    
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
        GameManager.Instance.AddInventoryItem(itemId, 1);
        yield return DialogueManager.Instance.RunDialogue(new DialogueOptions()
        {
            style = DialogueStyle.NarratorDialogue,
            text = $"{itemId} Get!"
        });
    }
}
