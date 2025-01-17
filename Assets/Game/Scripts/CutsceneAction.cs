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
