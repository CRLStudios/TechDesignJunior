using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, ICharacterBehaviour
{
    private CharacterEntityController _characterController;
    private Vector2 _moveInput = Vector2.zero;
    private bool _isRunning = false;
    
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference runAction;
    [SerializeField] private InputActionReference interactAction;

    private Interactor _interactor;
    
    private void Awake()
    {
        _interactor = GetComponent<Interactor>();
        _characterController = GetComponent<CharacterEntityController>();
        _characterController.RegisterBehaviour(this);
    }

    public void BehaviourUpdate()
    {
        if (!CanHandleInput())
        {
            _moveInput = Vector2.zero;
            _isRunning = false;
            _characterController.UpdateMoveAnimation(_moveInput, _isRunning);
            return;
        }
        
        if (interactAction.action.WasPressedThisFrame())
        {
            if (_interactor.TryActivate())
            {
                return;
            }
        }
        
        _moveInput = moveAction.action.ReadValue<Vector2>();
        _isRunning = runAction.action.IsPressed();
        _characterController.UpdateMoveAnimation(_moveInput, _isRunning);
    }
    
    public void BehaviourFixedUpdate()
    {
        // Set velocity
        _characterController.SetVelocity(_moveInput * (_isRunning ? _characterController.RunSpeed : _characterController.WalkSpeed));
    }

    public void OnStartCutsceneAction()
    {
        _characterController.Rigidbody.isKinematic = true;
    }

    public void OnCompleteCutsceneAction()
    {        
        _characterController.Rigidbody.isKinematic = false;
    }

    private bool CanHandleInput() => !CutsceneManager.Instance.IsBusy && !DialogueManager.Instance.IsRunning;
    
}
