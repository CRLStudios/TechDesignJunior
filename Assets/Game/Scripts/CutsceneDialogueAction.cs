using System.Collections;

public class CutsceneDialogueAction : CutsceneActionBehaviour
{
    protected override int ActionId => (int)CutsceneActionIds.Dialogue;
    
    protected override IEnumerator Execute(CutsceneState cutsceneState, CutsceneAction action)
    {
        yield return DialogueManager.Instance.RunDialogue(action.dialogueOptions);
    }
    
}
