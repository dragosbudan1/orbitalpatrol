using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {
  public float startingHealth = 1;
  private float  currentHealth;

  void Awake() {
    currentHealth = startingHealth;
  }

  public void TakeDamage(Damager damager) {
    currentHealth -= damager.damage;

    if(currentHealth <= 0) {
      Destroy(transform.gameObject);
    }
  }
}