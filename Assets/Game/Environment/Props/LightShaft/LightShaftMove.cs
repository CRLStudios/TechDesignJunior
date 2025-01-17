using UnityEngine;
using DG.Tweening;


public class LightShaftMove : MonoBehaviour
{
  private float startPosX;
  private float lightIntensity;
  public float moveAmount;

  //public float intensityAmount; //Commenting out cuz not used. -Cory 10/13/2021

  public float durationMove;
  public float durationIntensity;

  private UnityEngine.Rendering.Universal.Light2D lightShaft;

  private Tweener moveTween;
  private Tweener intensityTween;

  private void Awake()
  {
    lightShaft = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
  }

  private void OnEnable()
  {
    startPosX = transform.position.x;
    lightIntensity = lightShaft.intensity;
    lightShaft.intensity = lightIntensity * 0.7f;

    moveTween = transform.DOMove(new Vector3(Random.Range(startPosX - moveAmount, startPosX + moveAmount), transform.position.y, transform.position.z),
        Random.Range(durationMove - 1, durationMove + 1))
      .SetLoops(-1, LoopType.Yoyo)
      .SetEase(Ease.InOutSine);

    intensityTween = DOTween.To(() => lightIntensity, x => lightIntensity = x, lightIntensity * 1.4f, durationIntensity)
      .SetLoops(-1, LoopType.Yoyo)
      .SetEase(Ease.InOutBack);
  }

  private void OnDisable()
  {
    moveTween?.Kill();
    moveTween = null;

    intensityTween?.Kill();
    intensityTween = null;
  }

  private void Update()
  {
    lightShaft.intensity = lightIntensity;
  }
}