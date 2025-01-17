using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private bool activateOnTriggerEnter = false;
    [SerializeField] protected bool requirePlayerFacingThisDirection = true;
    
    protected static bool _isRunning = false;
    public static bool IsRunning => _isRunning;

    public static event UnityAction OnInteractableStart;
    public static event UnityAction OnInteractableEnd;
    
    public bool ActivateOnTriggerEnter => activateOnTriggerEnter;

    public abstract bool CanActivate(bool ignoreRunState = false);

    public abstract void Activate();

    protected void InvokeInteractableStart()
    {
        OnInteractableStart?.Invoke();
    }

    protected void InvokeInteractableEnd()
    {
        OnInteractableEnd?.Invoke();
    }
    
}