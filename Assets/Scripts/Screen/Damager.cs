using System;
using UnityEngine;
using UnityEngine.Events;


public class Damager : MonoBehaviour {
  public int damage = 1;
  private bool isForwardSet = false;
  private Vector3 startPosition;
  private Vector3 forward;
  public float raycastLength;

  void Start() {
    startPosition = transform.position;
    raycastLength = transform.localScale.x;
  }

  void Update() {
    if(!isForwardSet && startPosition != transform.position) {
      forward = (transform.position - startPosition).normalized;
      isForwardSet = true;
    } else {
     CheckHits();
    }           
  }

  void CheckHits() {
    var raycastStart = transform.position - (forward * (raycastLength / 2));
    var ray = new Ray(raycastStart, forward);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, raycastLength)) {
      var go = hit.transform.gameObject;
      var damageable =  go.GetComponent<Damageable>();
      if(damageable) {
        damageable.TakeDamage(new DamageableInput { Damage = damage });
      }
      Destroy(transform.gameObject); 
    }
  }
}
