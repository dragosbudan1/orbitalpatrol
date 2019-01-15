using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShooterState {
  Start = 0,
  Shoot,
  Die
}

public class ShooterInput
{
  public float Speed { get; set; }
  public Vector3 StartPosition { get; set; }
  public Vector3 StopPosition { get; set; }
  public Vector3 ExitDirection { get; set;}
  public int TimeStopped { get; set; }
  public Vector2 Offset {get; set;}
}

public class Shooter : MonoBehaviour
{
  public float health = 50.0f;
  private bool readyToDie = false;
  private ShooterInput shooterInput = new ShooterInput();
  private ShooterState state = ShooterState.Start;
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
      shooterInput = input;
    }

    startPosition = shooterInput.StartPosition + new Vector3(shooterInput.Offset.x, 0.0f, shooterInput.Offset.y);
    transform.position = startPosition;
    stopPosition = shooterInput.StopPosition + new Vector3(shooterInput.Offset.x, 0.0f, shooterInput.Offset.y);
  } 
}

