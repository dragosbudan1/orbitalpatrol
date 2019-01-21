using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ShipInput
{
  public float Throttle { get; set; }
  public float Steering { get; set; }
}

public static class ShipSteeringLimit
{
  public const float MaxLeft = -90;
  public const float MinLeft = -5;
  public const float Zero = 0;
  public const float MinRight = 5;
  public const float MaxRight = 90;
}

public static class ShipThrottleLimit
{
  public const float Min = -15;
  public const float Max = 20;
  public const float Neutral = 0.0f;
}

public class Ship : MonoBehaviour 
{  
  private ShipInput shipInput;
  private Rigidbody rb;
  private float colliderRadius;
  private GameObject projectilePrefab;
  public bool shootingActive = false;
  private bool isSpawning = false;

  public float steeringModifier = 0.25f;
  public float speedModifier = 0.025f;
  public float projectileSpeedModifier = 5f;
  public float bulletDelay = 0.1f;

  public float throttleMinAngle = -25.0f;
  public float throttleMaxAngle = 20.0f;

  public float RotationModifier = 0.2f;
  
  void Start() 
  {
      shipInput = new ShipInput
      {
        Throttle = 0.0f,
        Steering = 0.0f
      };

      rb = GetComponent<Rigidbody>();      
      colliderRadius = (GetComponent<Collider>() as CapsuleCollider).radius;
      projectilePrefab = Resources.Load("ProjectileQuadPrefab") as GameObject;

      AirConsole.instance.onMessage += OnMessage;
  } 

  void FixedUpdate()
  {
    rb.velocity = Vector3.zero;
    if(shootingActive && !isSpawning)
    {
      StartCoroutine(OnShootAction());
    }    
  }

  void Update() 
  {
    var target = Quaternion.Euler(0, shipInput.Steering, 0);
    transform.rotation = Quaternion.Slerp(transform.rotation, target, RotationModifier);
    transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    transform.Translate(transform.forward * shipInput.Throttle, Space.World);
  }

  private void OnMessage(int from, JToken data) 
  {
      var action = (string)(data["action"]);
      if(data != null && action != null)
      {
        switch(action)
        {
          case "move":
            OnMoveAction(data);
            break;
          case "shootDown":   
            shootingActive = true;         
            break;
          case "shootUp":   
            shootingActive = false;         
            break;
        }        
      } 
      else 
      {
        Debug.Log("no device data or action");              
      }
  }

  private void OnMoveAction(JToken data)
  {
    var deviceSteering = (float)(data["packet"]["aroundX"]);
    var deviceThrottle = (float)(data["packet"]["aroundY"]);

    shipInput.Steering += GetSteering(deviceSteering);

    if(shipInput.Steering > 360 || shipInput.Steering < -360)
    {
      shipInput.Steering = 0.0f; 
    }

    shipInput.Throttle = GetThrottle(deviceThrottle);
  }

  private IEnumerator OnShootAction()
  {
    isSpawning = true;

    yield return new WaitForSeconds(bulletDelay);
    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;    
    var projectileInput = new ProjectileInput(){ Speed = projectileSpeedModifier, Forward = transform.forward };
    if(projectileInput != null)
      projectile.SendMessage("Initialise", projectileInput);

    isSpawning = false;      
  }
  
  private float MapDeviceThrottle(float deviceThrottle)
  {
    return (ShipThrottleLimit.Min - deviceThrottle) * (-100 / (System.Math.Abs(ShipThrottleLimit.Max) + System.Math.Abs(ShipThrottleLimit.Min))); 
  }

  private float GetThrottle(float deviceThrottle)
  {
    if(deviceThrottle < ShipThrottleLimit.Min)
    {
      return ShipThrottleLimit.Neutral;
    }
    else if (deviceThrottle > ShipThrottleLimit.Max)
    {
      return MapDeviceThrottle(ShipThrottleLimit.Max) * speedModifier;  
    }
    else
    {
      return MapDeviceThrottle(deviceThrottle) * speedModifier;  
    }
  }

  private float GetSteering(float deviceSteering) {
    if(deviceSteering < ShipSteeringLimit.MaxLeft) { 
      deviceSteering = ShipSteeringLimit.MaxLeft; 
    } else if(deviceSteering > ShipSteeringLimit.MaxRight) { 
      deviceSteering = ShipSteeringLimit.MaxRight; 
    } else if(deviceSteering >= ShipSteeringLimit.MinLeft && deviceSteering <= ShipSteeringLimit.MinRight ) {
      deviceSteering = ShipSteeringLimit.Zero;
    }

    if(deviceSteering != ShipSteeringLimit.Zero) {
      if(deviceSteering < ShipSteeringLimit.MinLeft) {
        deviceSteering += ShipSteeringLimit.MinRight;
      } else {
        deviceSteering += ShipSteeringLimit.MinLeft;
      }
    }
    
    return deviceSteering * steeringModifier;
  }
}
