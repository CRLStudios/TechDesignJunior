using UnityEngine;
using Random = UnityEngine.Random;

namespace Nocturne
{
  public class CloudMovement : MonoBehaviour
  {
    public GameObject startPos;
    public GameObject endPos;
    public float speed;
    
    private float currentSpeed;

    private void OnEnable()
    {
      RandomizeSpeed();
    }

    private void Update()
    {
      //Move the shadow via transform position
      var pos = transform.position;
      pos += new Vector3(currentSpeed * Time.deltaTime, 0, 0);

      //Move back to starting position if it moves past the end position, get new random speed
      if (pos.x >= endPos.transform.position.x)
      {
        RandomizeSpeed();
        pos = new Vector3(startPos.transform.position.x, pos.y, pos.z);
        transform.position = pos;
      }
      else
      {
        transform.position = pos;
      }
    }

    private void RandomizeSpeed()
    {
      currentSpeed = Random.Range(speed * 0.9f, speed * 1.1f);
    }
  } 
}