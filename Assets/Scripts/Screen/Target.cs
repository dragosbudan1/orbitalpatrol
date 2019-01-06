using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInput
{
  public float Speed { get; set; }
  public Vector3 StartPosition { get; set; }
  public Vector3 StopPosition { get; set; }
  public Vector3 ExitDirection { get; set;}
  public int TimeStopped { get; set; }
  public Vector2 Offset {get; set;}
}

public enum TargetState
{
  Start = 0,
  Stop = 1,
  Exit = 2
}

public class Target : MonoBehaviour
{    
  private bool readyToDie = false;
  private TargetInput targetInput = new TargetInput();
  private TargetState state = TargetState.Start;
  private int secondsStopped = 0;
  private Vector3 startPosition = new Vector3();
  private Vector3 stopPosition = new Vector3();
  private DateTime stopTime = new DateTime();

  void Awake(){

  }
 
  void Update()
  { 
    switch(state)
    {
      case TargetState.Start:
      {
        if(Vector3.Distance(transform.position, stopPosition) > 30)
        {
          transform.position = Vector3.Lerp(transform.position, stopPosition, targetInput.Speed);
        } else {
          state = TargetState.Stop;
          stopTime = DateTime.Now;
        }
      }
      break;

      case TargetState.Stop:
      {
        secondsStopped = (DateTime.Now - stopTime).Seconds;
        //count down seconds
        if(secondsStopped >= targetInput.TimeStopped)
        {
          state = TargetState.Exit;
        }
      }
      break;

      case TargetState.Exit:
      {
        transform.Translate(targetInput.ExitDirection * targetInput.Speed * 10, Space.World);
      }
      break;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if(readyToDie)
    {
      if(other.gameObject.layer != LayerMask.NameToLayer("Targets"))
      {
        Destroy(transform.gameObject);
      }      
    }
  }

  private void OnTriggerExit(Collider other)
  {
    readyToDie = true;
    
    var rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
    if(rb != null)
    {
      rb.useGravity = false;  
    }    
  }

  void OnInitialise(TargetInput input)
  {
    if(input != null)
    {
      targetInput = input;
    }

    startPosition = targetInput.StartPosition + new Vector3(targetInput.Offset.x, 0.0f, targetInput.Offset.y);
    transform.position = startPosition;
    stopPosition = targetInput.StopPosition + new Vector3(targetInput.Offset.x, 0.0f, targetInput.Offset.y);
  } 
}