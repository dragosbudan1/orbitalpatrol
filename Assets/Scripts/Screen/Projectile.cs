using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInput
{
  public float Speed { get; set; }
  public Vector3 Forward { get; set; }
  public float Damage {get; set;}
}

public class Projectile : MonoBehaviour  
{
  private ProjectileInput projectileInput; 

  void Awake()
  {
    projectileInput = new ProjectileInput()
    {
      Speed = 0.0f,
      Forward = Vector3.zero
    };

    transform.rotation = Quaternion.Euler(-90, 0, 0);
  }

  void Update()
  {
    transform.Translate(projectileInput.Forward * projectileInput.Speed, Space.World);
  }

  void Initialise(ProjectileInput input)
  {
    if(input == null)
    {
      Debug.Log("No projectile input");
      return;
    }

    projectileInput = input;
  }
}