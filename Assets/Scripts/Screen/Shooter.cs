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
  private ShooterState state;
  public float health { get; set; }

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

  OnShootAction() {

  }
}

