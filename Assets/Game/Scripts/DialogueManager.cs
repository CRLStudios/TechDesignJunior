using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DialogueStyle
{
    CharacterDialogue,
    NarratorDialogue
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private CharacterDialogueView characterDialogueView;
    [SerializeField] private NarratorDialogueView narratorDialogueView;
    [SerializeField] private InputActionReference continueAction;

    private bool _isRunning = false;
    public bool IsRunning => _isRunning;
    
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator RunDialogue(DialogueOptions options)
    {
        _isRunning = true;
        
        switch (options.style)
        {
            case DialogueStyle.CharacterDialogue:
                characterDialogueView.Show(options.character, options.text);
                break;
            case DialogueStyle.NarratorDialogue:
                narratorDialogueView.Show(options.text);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        yield return null;
        yield return new WaitUntil(Continue);
        
        switch (options.style)
        {
            case DialogueStyle.CharacterDialogue:
                characterDialogueView.Hide();
                break;
            case DialogueStyle.NarratorDialogue:
                narratorDialogueView.Hide();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _isRunning = false;
    }
    
    private bool Continue()
    {
        return continueAction.action.WasPressedThisFrame();
    }
}
