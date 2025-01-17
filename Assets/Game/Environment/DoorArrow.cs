using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

namespace Nocturne
{
  public class DoorArrow : MonoBehaviour
  {
    public SpriteRenderer arrow;
    public Light2D lightGlow;

    public float arrowOpaqueAmount = 0.5f;
    public float glowIntensityAmount = 0.3f;

    public float fadeTime = 0.3f;

    private Tweener intensityTween;

    private void Start()
    {
      arrow.DOFade(0f, 0f);
      DOTween.To(() => lightGlow.intensity, x => lightGlow.intensity = x, 0f, 0f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.CompareTag("Walks"))
      {
        //Player0 or Player1 layers
        if (other.gameObject.layer == 13 || other.gameObject.layer == 20)
        {
          arrow.DOFade(arrowOpaqueAmount, fadeTime);
          intensityTween = DOTween.To(() => lightGlow.intensity, x => lightGlow.intensity = x, glowIntensityAmount, 1f).SetLoops(-1, LoopType.Yoyo);
        }
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.gameObject.CompareTag("Walks"))
      {
        //Player0 or Player1 layers
        if (other.gameObject.layer == 13 || other.gameObject.layer == 20)
        {
          arrow.DOFade(0f, fadeTime);

          intensityTween.Kill();
          intensityTween = DOTween.To(() => lightGlow.intensity, x => lightGlow.intensity = x, 0f, fadeTime);
        }
      }
    }
  }

}
