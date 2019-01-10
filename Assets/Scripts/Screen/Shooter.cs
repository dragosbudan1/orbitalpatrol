using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShooterState {
  Start = 0,
  Shoot,
  Die
}
public class Shooter : MonoBehaviour
{
  public float health { get; set; } = 50.0f;
  private bool readyToDie = false;
  private TargetInput targetInput = new TargetInput();
  private TargetState state = ShooterState.Start;
  private int secondsStopped = 0;
  private Vector3 startPosition = new Vector3();
  private Vector3 stopPosition = new Vector3();
  private DateTime stopTime = new DateTime();

  void Update() {
    switch(state) {
      case ShooterState.Start: {
        break;
      }

      case ShooterState.Shoot: {
        OnShootAction();
        break;
      }

      case ShooterState.Die: {
        break;
      }
    }
  }

  void OnShootAction() {

  }

  private void OnTriggerEnter(Collider other)
  {
    if(readyToDie)
    {
      if(other.gameObject.layer == LayerMask.NameToLayer("Ship"))
      {
        health
      }      
    }
  }

  private void OnTriggerExit(Collider other)
  {
    readyToDie = true;
    
    if(gameObject.GetComponent<Rigidbody>() as Rigidbody != null)
    {
      var rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
      if(rb != null)
      {
        rb.useGravity = false;  
      }    
    }    
  }

  void OnInitialise(ShooterInput input)
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

