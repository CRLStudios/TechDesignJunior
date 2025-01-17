using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>

public class LightFlicker : MonoBehaviour
{
  UnityEngine.Rendering.Universal.Light2D pointLight;
  //float startingRadius;
  //float radius;
  float startingIntensity;
  public float deltaIntensity;
  float minIntensity = 0f;
  float maxIntensity = 1f;
  [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
  [Range(1, 50)]
  public int smoothing = 5;
  

  // Continuous average calculation via FIFO queue
  // Saves us iterating every time we update, we just change by the delta
  Queue<float> smoothQueue;
  float lastSum = 0;

  bool coroutineStarted = false;
  bool flickerLight = false;

  bool canFlicker;
  bool canOff;

  float onTime = 10f;
  float flickerTime = 0.7f;
  float offTime = 0.8f;

  float onTimeDelta = 3f;
  float flickerTimeDelta = 0.4f;
  float offTimeDelta = 0.04f;

  public enum State
  {
    On,
    Off,
    Flicker
  }

  public State currentState;

  /// <summary>
  /// Reset the randomness and start again. You usually don't need to call
  /// this, deactivating/reactivating is usually fine but if you want a strict
  /// restart you can do.
  /// </summary>
  public void Reset()
  {
    smoothQueue.Clear();
    lastSum = 0;
  }

  private void Awake()
  {
    pointLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();

  }
  void Start()
  {
    smoothQueue = new Queue<float>(smoothing);


    startingIntensity = pointLight.intensity;
    //startingRadius = pointLight.pointLightOuterRadius;
    minIntensity = startingIntensity - deltaIntensity;
    maxIntensity = startingIntensity + deltaIntensity;

    currentState = (State) Random.Range(0, 3);
  }

  void Update()
  {
    ChangeState();

    if (flickerLight)
    {
      FlickerLight();
    }

  }

  void ChangeState()
  {
    switch (currentState)
    {
      case State.On:
        if (!coroutineStarted)
        {
          StartCoroutine(OnCoroutine());
        }
        break;

      case State.Off:
        if (!coroutineStarted)
        {
          //lower off chance
          if (Random.Range(0,100) > 92)
          {
            StartCoroutine(OffCoroutine());
          }
          else
          {
            StartCoroutine(FlickerCoroutine());
          }

        }
        break;

      case State.Flicker:
        if (!coroutineStarted)
        {
          StartCoroutine(FlickerCoroutine());
        }
        break;


    }
  }

  IEnumerator OnCoroutine()
  {
    coroutineStarted = true;
    //Set light to starting intensity
    pointLight.intensity = startingIntensity;

    yield return new WaitForSeconds(Random.Range(onTime - onTimeDelta, onTime + onTimeDelta));


    //currentState = (State)Random.Range(1,3);
    coroutineStarted = false;
    currentState = (State) Random.Range(1, 3);
  }

  IEnumerator OffCoroutine()
  {
    //PauseLight.Post(transform.parent.gameObject);
    coroutineStarted = true;
    //Turn off light
    pointLight.intensity = 0f;

    yield return new WaitForSeconds(Random.Range(offTime - offTimeDelta, offTime + offTimeDelta));

    //ResumeLight.Post(transform.parent.gameObject);
    coroutineStarted = false;
    currentState = State.On;

    //currentState = State.Off;
  }
  IEnumerator FlickerCoroutine()
  {
    coroutineStarted = true;
    //Flicker light
    flickerLight = true;

    yield return new WaitForSeconds(Random.Range(flickerTime - flickerTimeDelta, flickerTime + flickerTimeDelta));

    flickerLight = false;
    coroutineStarted = false;
    currentState = (State) Random.Range(0, 2);

    //currentState = State.Off;
  }

  void FlickerLight()
  {
    // pop off an item if too big
    while (smoothQueue.Count >= smoothing)
    {
      lastSum -= smoothQueue.Dequeue();
    }

    // Generate random new item, calculate new average
    float newVal = Random.Range(minIntensity, maxIntensity);
    smoothQueue.Enqueue(newVal);
    lastSum += newVal;

    // Calculate new smoothed average
    pointLight.intensity = lastSum / (float) smoothQueue.Count;
  }
}
