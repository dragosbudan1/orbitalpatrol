using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerSettings {
  public int DeviceId { get; set; }
  public Color ShipColor { get; set; }
}

public class AirConsoleManager : MonoBehaviour {

  public Dictionary<int, GameObject> ships;
  private GameObject shipPrefab;
  private List<Color> shipColors;

  void Awake() {
    AirConsole.instance.onReady += OnReady;
		AirConsole.instance.onMessage += OnMessage;
		AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onDisconnect += OnDisconnect;

    ships = new Dictionary<int, GameObject>();
    shipPrefab = Resources.Load("Ship") as GameObject;

    shipColors = new List<Color> {
      Color.red, 
      Color.blue, 
      Color.yellow, 
      Color.green
    };
  }

  	void OnReady (string code) {
    }

	void OnMessage (int deviceId, JToken data) {
    if(ships.ContainsKey(deviceId)) {
      try {
        ships[deviceId].SendMessage("OnMessage", data);
      } catch {
        ships.Remove(deviceId);
      }
    }
	}

	void OnConnect (int deviceId) {
    Debug.Log("Device Connected: " + deviceId);
    GameObject ship = Instantiate(shipPrefab, transform.position, Quaternion.identity) as GameObject;    
    if(ship != null) {
      ships.Add(deviceId, ship);
      ship.SendMessage("OnDeviceConnect", new PlayerSettings { 
        DeviceId = deviceId, 
        ShipColor = shipColors[ships.Count - 1]
      });
    }  
  }

	void OnDisconnect (int deviceId) {
		Debug.Log("Device Disconnected: " + deviceId);
    if(ships.ContainsKey(deviceId)) {
      Destroy(ships[deviceId]);
      ships.Remove(deviceId);
    }
	}
}