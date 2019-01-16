using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DamageableEvent : UnityEvent<Damager, Damageable> {}

[Serializable]
public class NonDamageableEvent : UnityEvent<Damager> {}

public class Damager : MonoBehaviour {
  public DamageableEvent OnDamageableHit;
  public NonDamageableEvent OnNoDamageableEvent;

  public float damage = 1.0f;

  private void OnTriggerEnter(Collider other) {

    Debug.Log("damage is: " + damage);

    if(other.gameObject.layer != LayerMask.NameToLayer("Ship")) {
      Destroy(transform.gameObject);
    }
    
    if(other.gameObject.layer == LayerMask.NameToLayer("Targets")) {
      var damageable = other.gameObject.GetComponent<Damageable>();
      if(damageable) {
        damageable.TakeDamage(this);
      }
    }
  }
}
