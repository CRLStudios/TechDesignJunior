using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    private PlayerController _playerController;
    
    [SerializeField] private float minX = -100;
    [SerializeField] private float maxX = 100;
    [SerializeField] private float minY = -100;
    [SerializeField] private float maxY = 100;

    private void Start()
    {
        _playerController = PlayerController.Instance;
    }
    
    private void LateUpdate()
    {
        if (_playerController == null)
        {
            return;
        }
        
        var targetPosition = _playerController.transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }
}
