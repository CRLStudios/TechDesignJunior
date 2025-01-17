using UnityEngine;
using XNode;

[CreateAssetMenu]
public class InteractableGraph : NodeGraph
{
    public InteractionStartNode GetStartNode()
    {
        foreach (var node in nodes)
        {
            if (node is InteractionStartNode startNode)
            {
                return startNode;
            }
        }
        return null;
    }
}
