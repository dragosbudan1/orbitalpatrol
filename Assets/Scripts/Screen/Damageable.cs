using System;
using UnityEngine;
using UnityEngine.Events;

public class DamageableInput {
  public int Damage { get; set; }
}

public class Damageable : MonoBehaviour {

  public delegate void UpdateScoreAction(int score);
  public static event UpdateScoreAction OnScoreUpdateEvent;
  public delegate void UpdateShipHealthAction(int health);
  public static event UpdateShipHealthAction OnShipHealthUpdateEvent;

  public int startingHealth = 1;
  private int currentHealth;

  void Awake() {
    currentHealth = startingHealth;
  }
  
  public void TakeDamage(DamageableInput di) {
    currentHealth -= di.Damage;

    if(transform.gameObject.layer == LayerMask.NameToLayer("Ship")) {
      OnShipHealthUpdateEvent.Invoke(currentHealth);
    }

    if(currentHealth <= 0) {
      if(transform.gameObject.layer == LayerMask.NameToLayer("Targets")) {
        OnScoreUpdateEvent.Invoke((int)di.Damage * 10);
      }
      Destroy(transform.gameObject);
    }
  }
}