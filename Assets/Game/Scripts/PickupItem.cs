using System;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    [SerializeField] private string itemId;

    private void Start()
    {
        GameManager.Instance.OnInventoryChanged += OnInventoryChanged;
        if (GameManager.Instance.InventoryItem == itemId)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnInventoryChanged -= OnInventoryChanged;
        }
    }

    private void OnInventoryChanged(string heldItemId)
    {
        gameObject.SetActive(heldItemId != itemId);
    }

    public override bool CanActivate(bool ignoreRunState = false)
    {
        return !string.IsNullOrWhiteSpace(itemId) && GameManager.Instance.InventoryItem != itemId;
    }

    public override void Activate()
    {
        var actionList = new List<CutsceneAction>();
        
        actionList.Add(new CutsceneAction.Builder()
            .SetActionId(CutsceneActionIds.GiveItem)
            .SetItemId(itemId)
            .Build());

        var previousItem = GameManager.Instance.InventoryItem;
        if (!string.IsNullOrWhiteSpace(previousItem))
        {
            actionList.Add(new CutsceneAction.Builder()
                .SetActionId(CutsceneActionIds.Dialogue)
                .SetDialogueOptions(new DialogueOptions()
                {
                    style = DialogueStyle.NarratorDialogue,
                    text = $"{previousItem} was put back in its original place."
                })
                .Build());
        }
        
        CutsceneManager.Instance.RunCutscene(new PickupCutscene($"pickup_{itemId}", actionList));
    }

    private class PickupCutscene : ICutscene
    {
        public PickupCutscene(string cutsceneId, List<CutsceneAction> actions)
        {
            CutsceneId = cutsceneId;
            Actions = actions;
        }
        
        public string CutsceneId { get; }
        public List<CutsceneAction> Actions { get; }
    }
}
