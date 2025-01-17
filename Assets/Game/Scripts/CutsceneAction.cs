using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class CutsceneAction
{
    [SerializeField] private CutsceneActionIds actionId;
    public int ActionId => (int) actionId;
    
    [AllowNesting]
    [ShowIf(nameof(actionId), CutsceneActionIds.Dialogue)]
    public DialogueOptions dialogueOptions;

    [AllowNesting]
    [ShowIf(nameof(actionId), CutsceneActionIds.MoveTo)]
    public MoveToOptions moveToOptions;

    [AllowNesting]
    [SerializeField, ShowIf(nameof(actionId), CutsceneActionIds.GiveItem)] private string itemId;
    public string ItemId => itemId;
    
    public class Builder
    {
        private int _actionId;
        private DialogueOptions _dialogueOptions = null;
        private string _itemId = null;
        
        public Builder SetActionId(CutsceneActionIds actionId)
        {
            _actionId = (int) actionId;
            return this;
        }
        
        public Builder SetDialogueOptions(DialogueOptions dialogueOptions)
        {
            _dialogueOptions = dialogueOptions;
            return this;
        }

        public Builder SetItemId(string itemId)
        {
            _itemId = itemId;
            return this;
        }
        
        public CutsceneAction Build()
        {
            return new CutsceneAction()
            {
                actionId = (CutsceneActionIds) _actionId,
                itemId = _itemId,
                dialogueOptions = _dialogueOptions
            };
        }
    }
}

public enum CharacterName : int
{
    Unknown = -1,
    Karma = 0,
    //Satoru = 1
}

[Serializable]
public class DialogueOptions
{
    public DialogueStyle style;
    [AllowNesting]
    [ShowIf(nameof(style), DialogueStyle.CharacterDialogue)]
    public CharacterName character = CharacterName.Karma;
    [ResizableTextArea][AllowNesting]
    public string text;
}

[Serializable]
public class MoveToOptions
{
    public CharacterEntityController character;
    public Transform moveToTarget;
    public Directions faceDirectionOnComplete = Directions.None;
    public bool run = false;
}
