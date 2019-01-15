using System;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour {
  
  [Serializable]
  public class DamageableEvent : UnityEvent<Damager, Damageable> {}

  [Serializable]
  public class NonDamageableEvent : UnityEvent<Damager> {}

  public DamageableEvent OnDamageableHit;
  public NonDamageableEvent OnNoDamageableEvent;
}

public class Damageable : MonoBehaviour {}