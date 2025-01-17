using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEntityController : MonoBehaviour
{
    [SerializeField] private CharacterAnimator characterAnimator;
    [SerializeField] private Rigidbody2D characterRigidbody;
    [SerializeField] private Directions facingDirection = Directions.Down;
    [SerializeField] private float walkSpeed = 50f;
    [SerializeField] private float runSpeed = 100f;

    public Rigidbody2D Rigidbody => characterRigidbody;
    public float RunSpeed => runSpeed;
    public float WalkSpeed => walkSpeed;
    public Directions CurrentDirection => facingDirection;
    
    private ICharacterBehaviour _currentBehaviour = null;
    private bool runningAction = false;

    public event UnityAction FacingDirectionChanged;
    
    public void RegisterBehaviour(ICharacterBehaviour behaviour)
    {
        _currentBehaviour = behaviour;
    }

    protected virtual void Awake()
    {
    }

    private void Start()
    {
        characterAnimator.UpdateFacingDirection(facingDirection);
    }

    private void Update()
    {
        if (runningAction)
        {
            return;
        }
        _currentBehaviour?.BehaviourUpdate();
    }
    
    private void FixedUpdate()
    {
        if (runningAction)
        {
            return;
        }
        _currentBehaviour?.BehaviourFixedUpdate();
    }

    public void SetVelocity(Vector2 velocity)
    {
        // Set velocity
        characterRigidbody.velocity = velocity;
    }
    
    public Tween MoveTo(Vector3 position, bool run = false)
    {
        StartCutsceneAction();
        var direction = position - transform.position;
        var duration = direction.magnitude / (run ? runSpeed : walkSpeed);
        characterAnimator.UpdateMoveAnimation(direction.normalized, run);
        return transform.DOMove(position, duration).SetEase(Ease.Linear).OnComplete(MoveToComplete);
    }

    private void MoveToComplete()
    {
        characterAnimator.UpdateMoveAnimation(Vector2.zero, false);
        CompleteCutsceneAction();
    }

    public void UpdateMoveAnimation(Vector2 moveInput, bool isRunning = false)
    {
        var dir = moveInput.ToDirection();
        if (dir != Directions.None)
        {
            _internalSetFacingDirection(dir);
        }
        characterAnimator.UpdateMoveAnimation(moveInput,isRunning);
    }
    
    public void FaceDirection(Directions direction)
    {
        characterAnimator.UpdateFacingDirection(direction);
        _internalSetFacingDirection(direction);
    }

    private void StartCutsceneAction()
    {
        runningAction = true;
        _currentBehaviour?.OnStartCutsceneAction();
        characterRigidbody.velocity = Vector2.zero;
    }
    
    private void CompleteCutsceneAction()
    {
        _currentBehaviour?.OnCompleteCutsceneAction();
        runningAction = false;
    }

    private void _internalSetFacingDirection(Directions direction)
    {
        if (facingDirection == direction)
        {
            return;
        }
        facingDirection = direction;
        FacingDirectionChanged?.Invoke();
    }

}
