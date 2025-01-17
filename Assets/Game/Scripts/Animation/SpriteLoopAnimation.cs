using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nocturne
{
    public class SpriteLoopAnimation : MonoBehaviour
    {
      [SerializeField] private SpriteRenderer _spriteRenderer;

      [SerializeField] private SpriteAnimationTimeMode timeMode = SpriteAnimationTimeMode.FramesPerSecond;
      [SerializeField] private int fps = 12;
      [SerializeField] private float interval = 0.1f;
      [SerializeField] private bool randomStartFrame = true;
      [SerializeField] private float speed = 1f;

      [SerializeField] private float effectiveFps;
      
      [SerializeField] private Sprite[] frames;

      private float frame = 0;
      
      public enum SpriteAnimationTimeMode
      {
        FramesPerSecond,
        Interval,
      }
      
      private void OnEnable()
      {
        SpriteAnimationManager.GetOrCreateInstance().UpdateEvent.AddListener(UpdateAnimation);
        if (randomStartFrame)
        {
          frame = Random.Range(0, frames.Length);
        }
      }

      private void OnDisable()
      {
        SpriteAnimationManager.Instance?.UpdateEvent.RemoveListener(UpdateAnimation);
      }

      private void UpdateAnimation()
      {
        //Do nothing if we have no frames
        if (frames.Length < 1)
        {
          return;
        }
        
        try
        {
          frame += Time.unscaledDeltaTime * speed * effectiveFps;
          var currentFrame = Mathf.FloorToInt(frame);
          while (currentFrame >= frames.Length)
          {
            frame -= frames.Length;
            currentFrame = Mathf.FloorToInt(frame);
            //frame = Mathf.Clamp(frame - frames.Length, 0, frames.Length - 1);
          }
          _spriteRenderer.sprite = frames[currentFrame];
        }
        catch (Exception e)
        {
          Debug.LogException(e,gameObject);
          Debug.LogError($"CurrentFrame: {frame} floor={Mathf.FloorToInt(frame)} totalFrames={frames.Length}");
        }
      }

      private void OnValidate()
      {
        if (_spriteRenderer == null)
        {
          _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (speed < 0)
        {
          speed = 0;
        }
        
        switch (timeMode)
        {
          case SpriteAnimationTimeMode.FramesPerSecond:
            if (fps < 0)
            {
              fps = 0;
            }
            effectiveFps = fps;
            break;
          case SpriteAnimationTimeMode.Interval:
            if (interval < 0.01f)
            {
              interval = 0.01f;
            }
            effectiveFps = 1f / interval;
            break;
        }
      }
    }
}
