using System.Collections;
using DG.Tweening;

public class CutsceneMoveToAction : CutsceneActionBehaviour
{
    protected override int ActionId => (int)CutsceneActionIds.MoveTo;
    
    protected override IEnumerator Execute(CutsceneState cutsceneState, CutsceneAction action)
    {
        var options = action.moveToOptions;
        var character = options.character;
        yield return character.MoveTo(options.moveToTarget.position,options.run).WaitForCompletion();
        character.FaceDirection(options.faceDirectionOnComplete);
    }
}
