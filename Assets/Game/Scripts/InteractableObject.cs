using System.Collections;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : Interactable
{
    [SerializeField] private InteractableGraph interactableGraph;
    
    private bool HasGraph => interactableGraph != null;
    
#if UNITY_EDITOR
    [Button, ShowIf(nameof(HasGraph))]
    private void OpenGraph()
    {
        if (interactableGraph != null)
        {
            XNodeEditor.NodeEditorWindow.Open(interactableGraph);
        }
    }
#endif
    
    public override bool CanActivate(bool ignoreRunState = false)
    {
        if (interactableGraph == null || (_isRunning && !ignoreRunState)) 
        {
            return false;
        }

        if (requirePlayerFacingThisDirection)
        {
            var direction = PlayerController.Instance.CurrentDirection;
            var vectorDirection = (transform.position - PlayerController.Instance.transform.position);
            var directionToThis = vectorDirection.ToDirection();
            if (direction != directionToThis)
            {
                return false;
            }
        }
        
        // Check if there is an action node or coroutine node in the graph's active path
        InteractionNode current = interactableGraph.GetStartNode();
        var canActivate = true;
        var visitedSet = HashSetPool<InteractionNode>.New();
        while (!visitedSet.Contains(current) && current != null)
        {
            visitedSet.Add(current);
            
            if ( current is IActionNode)
            {
                canActivate = true;
                break;
            }

            if (current is ICoroutineNode)
            {
                canActivate = true;
                break;
            }
            
            current = current.GetNextNode(this);
        }
        HashSetPool<InteractionNode>.Free(visitedSet);

        return canActivate;
    }

    public override void Activate()
    {
        if (_isRunning)
        {
            Debug.LogError("Interactable is already running");
            return;
        }
        StartCoroutine(RunGraph());
    }

    private IEnumerator RunGraph()
    {
        _isRunning = true;
        InvokeInteractableStart();
        InteractionNode current = interactableGraph.GetStartNode();
        var visitedSet = HashSetPool<InteractionNode>.New();
        while (!visitedSet.Contains(current) && current != null)
        {
            visitedSet.Add(current);

            if ( current is IActionNode actionNode)
            {
                actionNode.Execute(this);
            }
            else if (current is ICoroutineNode coroutineNode)
            {
                yield return coroutineNode.Execute(this);
            }
            
            current = current.GetNextNode(this);
        }
        HashSetPool<InteractionNode>.Free(visitedSet);
        _isRunning = false;
        InvokeInteractableEnd();
    }

}
