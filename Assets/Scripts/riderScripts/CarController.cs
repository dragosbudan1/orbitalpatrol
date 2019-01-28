using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class CarController : MonoBehaviour {

	bool move = false;
	bool isGrounded = false;

	public Rigidbody2D rb;

	public float speed = 20f;
	public float rotationSpeed = 2f;
  private float deviceSpeed;
  public float speedModifier;
  public float deviceLimit;
  public bool deviceUpdate = false;
	private bool Landed = false;
  public float rotationModifier;

  void Start() {
		AirConsole.instance.onMessage += OnMessage;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			move = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			move = false;
		}
	}
	private void FixedUpdate()
	{
			if(transform.position.y < -10) {
				transform.position = new Vector3(-7.7f, 1.65f, 0.0f);
			}
			if (isGrounded)
			{
				rb.AddForce(transform.right * deviceSpeed * Time.fixedDeltaTime * 100f, ForceMode2D.Force);
			} else
			{
				if(Landed) {
					rb.AddTorque(deviceSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
				}
			}
	}

	private void OnCollisionEnter2D()
	{
		isGrounded = true;
		Landed = true;
	}

	private void OnCollisionExit2D()
	{
		isGrounded = false;
	}

	private void OnMessage(int from, JToken data) 
  {
      var action = (string)(data["a"]);
      // AirConsole.instance.Message(from, "Full of pixels!");
      if(data != null &&  action != null)
      {
        switch(action)
        {
          case "m":
            OnMoveAction(data);
            break;
        }        
      } 
      else 
      {
        Debug.Log("no device data or action");              
      }
  }

	private void OnMoveAction(JToken data) {
		var deviceInput = (float)(data["p"]["x"]);

		if(deviceUpdate) { 
			if(isGrounded) {
				deviceSpeed = (deviceInput + deviceLimit) * speedModifier;
			} else {
				deviceSpeed = deviceInput * rotationModifier;
			}
		}
	}

}
