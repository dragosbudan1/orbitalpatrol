using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSettings : MonoBehaviour
{
  public int Framerate = 60;
  void Awake()
  {
    Application.targetFrameRate = Framerate;
  }
}