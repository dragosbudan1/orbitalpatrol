using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    public GameObject projectile;
     
    void Start() {
      projectile = GameObject.Find("Ship");
    }  
    void LateUpdate() {
        transform.position = new Vector3(projectile.transform.position.x, 
            projectile.transform.position.y + 15, 
            projectile.transform.position.z -17.0f);
        transform.LookAt(projectile.transform.position);
    }
}