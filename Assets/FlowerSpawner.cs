using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nocturne
{
  public class FlowerSpawner : MonoBehaviour
  {
    public List<GameObject> flowers;

    public int spawnAmount;

    Vector3 spawnPosition;
    Collider2D spawnCollider;

    private void Awake()
    {
      spawnCollider = GetComponent<Collider2D>();
    }


    private void Start()
    {
      //get min and max x value
      //get min and max y value 
      //new vector thats random between the two value

      float minX = spawnCollider.bounds.min.x;
      float maxX = spawnCollider.bounds.max.x;
      float minY = spawnCollider.bounds.min.y;
      float maxY = spawnCollider.bounds.max.y;


      for (var i = 0; i < spawnAmount; i++)
      {
        spawnPosition = new Vector3(Mathf.Round(Random.Range(minX, maxX)), Mathf.Round(Random.Range(minY, maxY)), 0f);

        Instantiate(flowers[Random.Range(0, flowers.Count)],spawnPosition, Quaternion.identity, this.transform);
      }
    }
  }
}
