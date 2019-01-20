using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

  private int playerScore = 0;
  private int shipHealth = 99;
  void Awake() {
    Damageable.OnScoreUpdateEvent += UpdateScore;
    Damageable.OnShipHealthUpdateEvent += UpdateHealth;
    var ships = GameObject.FindGameObjectsWithTag("Player").ToList();
    if(ships != null) {
      shipHealth = ships.First().GetComponent<Damageable>().startingHealth;
    }
  }

  void Start() {
  }

  void OnGUI() {
      GUI.Label(new Rect(10, 10, 100, 20), "Level: ");
      GUI.Label(new Rect(10, 20, 100, 20), string.Format("Player Health: {0}", shipHealth));
      GUI.Label(new Rect(10, 30, 100, 20), string.Format("Player Score: {0}", playerScore));
      GUI.Label(new Rect(10, 40, 100, 20), "Enemies Alive: ");
  }

  public void UpdateScore(int score) {
    playerScore += score;
  }

  public void UpdateHealth(int currentHealth) {
    shipHealth = currentHealth;
  }
}