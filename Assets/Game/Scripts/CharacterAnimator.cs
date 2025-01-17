using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class CharacterAnimator : ICharacterAnimator
{
    [SerializeField] private Animator animator;
    [AnimatorParam(nameof(animator))][SerializeField] private int horizontalAxis;
    [AnimatorParam(nameof(animator))][SerializeField] private int verticalAxis;
    [AnimatorParam(nameof(animator))][SerializeField] private int walking;
    [AnimatorParam(nameof(animator))][SerializeField] private int running;
    
    public void UpdateMoveAnimation(Vector2 moveInput, bool isRunning)
    {
        var isMoving = moveInput.sqrMagnitude > 0;
        SetBool(walking, isMoving);
        SetBool(running, isMoving && isRunning);
        if (isMoving)
        {
            SetFloat(horizontalAxis, moveInput.x);
            SetFloat(verticalAxis, moveInput.y);
        }
    }

    public void UpdateFacingDirection(Directions directions)
    {
        Vector2 dir = Vector2.zero;
        switch (directions)
        {
            case Directions.None:
                break;
            case Directions.Up:
                dir = Vector2.up;
                break;
            case Directions.Down:
                dir = Vector2.down;
                break;
            case Directions.Left:
                dir = Vector2.left;
                break;
            case Directions.Right:
                dir = Vector2.right;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(directions), directions, null);
        }
        SetFloat(horizontalAxis, dir.x);
        SetFloat(verticalAxis, dir.y);
    }

    private void SetBool(int property, bool value)
    {
        if (property == 0)
        {
            return;
        }
        animator.SetBool(property, value);
    }
    
    private void SetFloat(int property, float value)
    {
        if (property == 0)
        {
            return;
        }
        animator.SetFloat(property, value);
    }
    
}

public interface ICharacterAnimator
{
    void UpdateMoveAnimation(Vector2 moveInput, bool isRunning);
}
