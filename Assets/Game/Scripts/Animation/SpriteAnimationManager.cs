using UnityEngine;
using UnityEngine.Events;

namespace Nocturne
{
  public class SpriteAnimationManager : MonoBehaviour
  {
    private static SpriteAnimationManager _instance;

    public static SpriteAnimationManager GetOrCreateInstance()
    {
      if (_instance == null)
      {
        var newGameObject = new GameObject("SpriteAnimationManager");
        DontDestroyOnLoad(newGameObject);
        _instance = newGameObject.AddComponent<SpriteAnimationManager>();
      }
      return _instance;
    }
    
    public static SpriteAnimationManager Instance
    {
      get => _instance;
      private set => _instance = value;
    }

    public UnityEvent UpdateEvent { get; } = new UnityEvent();
    
    private void Awake()
    {
      if ( _instance != null )
      {
        Destroy( gameObject );
        return;
      }
      _instance = this;
    }

    private void LateUpdate()
    {
      UpdateEvent.Invoke();
    }
  }
}