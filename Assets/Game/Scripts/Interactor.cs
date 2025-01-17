using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private readonly List<Interactable> canActivate = new List<Interactable>();
    private readonly List<Interactable> overlapping = new List<Interactable>();

    [SerializeField] private CharacterEntityController characterEntityController;
    [SerializeField] private Transform interactionAlert;
    private bool isShowingAlert = false;
    
    private void OnEnable()
    {
        interactionAlert.gameObject.SetActive(true);
        Interactable.OnInteractableStart += Refresh;
        Interactable.OnInteractableEnd += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        Interactable.OnInteractableStart -= Refresh;
        Interactable.OnInteractableEnd -= Refresh;
    }

    private void ShowInteractionAlert()
    {
        isShowingAlert = true;
        interactionAlert.gameObject.SetActive(true);
    }
    
    private void HideInteractionAlert()
    {
        isShowingAlert = false;
        interactionAlert.gameObject.SetActive(false);
    }

    private void Update()
    {
        canActivate.Clear();
        foreach ( var interactable in overlapping)
        {
            if (interactable.CanActivate(true))
            {
                canActivate.Add(interactable);
            }
        }
        Refresh();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Interactable interactable))
        {
            return;
        }

        var canBeActivated = interactable.CanActivate();
        if (canBeActivated)
        {
            canActivate.Add(interactable);
        }
        overlapping.Add(interactable);
        Refresh();
        
        if (interactable.ActivateOnTriggerEnter)
        {
            interactable.Activate();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Interactable interactable))
        {
            return;
        }

        canActivate.Remove(interactable);
        overlapping.Remove(interactable);
        Refresh();
    }

    public bool TryActivate()
    {
        foreach (var interactable in canActivate)
        {
            if (!interactable.CanActivate())
            {
                continue;
            }

            interactable.Activate();
            return true;
        }
        return false;
    }

    private void Refresh()
    {
        if (!isShowingAlert && canActivate.Count > 0 && !Interactable.IsRunning)
        {
            ShowInteractionAlert();
        }
        else if (isShowingAlert && canActivate.Count == 0)
        {
            HideInteractionAlert();
        }
    }
    
    /*private void OnProgressFlagChanged(string arg0, float arg1)
    {
        canActivate.Clear();
        foreach ( var interactable in overlapping)
        {
            if (interactable.CanActivate(true))
            {
                canActivate.Add(interactable);
            }
        }
        Refresh();
    }
    
    private void OnPlayerDirectionChanged()
    {
        canActivate.Clear();
        foreach ( var interactable in overlapping)
        {
            if (interactable.CanActivate(true))
            {
                canActivate.Add(interactable);
            }
        }
        Refresh();
    }*/
    
}
