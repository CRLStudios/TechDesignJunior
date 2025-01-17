using System.Collections;
using UnityEngine;

public abstract class CutsceneActionBehaviour : MonoBehaviour
{
    protected abstract int ActionId { get; }

    protected virtual void Start()
    {
        CutsceneManager.Instance.RegisterActionBehaviour(ActionId, Execute);
    }

    protected abstract IEnumerator Execute(CutsceneState cutsceneState, CutsceneAction action);
}
