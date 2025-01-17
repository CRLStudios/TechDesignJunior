using System.Collections;

public class CutsceneGiveItemAction : CutsceneActionBehaviour
{
    protected override int ActionId => (int)CutsceneActionIds.GiveItem;
    
    protected override IEnumerator Execute(CutsceneState cutsceneState, CutsceneAction action)
    {
        GameManager.Instance.InventoryItem = action.ItemId;
        yield return DialogueManager.Instance.RunDialogue(new DialogueOptions()
        {
            style = DialogueStyle.NarratorDialogue,
            text = $"{action.ItemId} Get!"
        });
    }
}