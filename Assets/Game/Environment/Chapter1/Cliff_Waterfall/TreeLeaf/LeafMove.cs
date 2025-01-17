using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class LeafMove : MonoBehaviour
{
  private float startPosX;
  private float startPosY;
  public float moveAmount = 2.8f;
  public float duration = 3.8f;

  private Tweener tween;

  private void OnEnable()
  {
    var position = transform.position;
    startPosX = position.x;
    startPosY = position.y;

    tween = transform.DOMove(new Vector3(Random.Range(startPosX - moveAmount, startPosX + moveAmount), Random.Range(startPosY - moveAmount, startPosY + moveAmount), position.z),
        Random.Range(duration - 1, duration + 1))
      .SetLoops(-1, LoopType.Yoyo)
      .SetEase(Ease.InOutSine);
  }

  private void OnDisable()
  {
    tween?.Kill();
    tween = null;
  }
}