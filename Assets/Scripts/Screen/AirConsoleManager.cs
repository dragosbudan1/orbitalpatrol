using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleManager : MonoBehaviour {

  public List<GameObject> ships;
  private GameObject shipPrefab;
  
  void Awake() {
    AirConsole.instance.onReady += OnReady;
		AirConsole.instance.onMessage += OnMessage;
		AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onDisconnect += OnDisconnect;

    ships = new List<GameObject>();
    shipPrefab = Resources.Load("Ship") as GameObject;
  }

  	void OnReady (string code) {
    }

	void OnMessage (int from, JToken data) {
		Debug.Log(string.Format("Message from {0}", from));
	}

	void OnConnect (int deviceId) {
    Debug.Log("Device Connected: " + deviceId);
    GameObject ship = Instantiate(shipPrefab, transform.position, Quaternion.identity) as GameObject;    
    if(ship != null) {
      ships.Add(ship);
      ship.SendMessage("OnDeviceConnect", deviceId);
    }  
  }

	void OnDisconnect (int device_id) {
		Debug.Log("Device Disconnected: " + device_id);
	}

  void CreateShip(int deviceId) {

  }
}