using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetCluster
{
  public List<GameObject> GameObjects { get; set; }
  public bool Alive { get; set; }
}

public class Targets : MonoBehaviour
{
  private GameObject prefab;

  public int clusterCount = 1;
  private int maxCurrentTargetClusters = 1;
  private List<TargetCluster> activeTargetClusters;

  public float TargetSpeed = 2f;
  public int TimeStopped = 5;

  void Awake()
  {
    activeTargetClusters = new List<TargetCluster>();
    prefab = Resources.Load("TargetPrefab") as GameObject;
  }
  void Start()
  {
  }

  void Update()
  {
    var targets = GameObject.FindGameObjectsWithTag("TargetsTag").ToList();
    if(targets != null)
    {
      if(targets.Count == 0)
      {
        CreateTargetCluster();
      }       
    }      
  }

  void OnGUI()
  {
    GUI.Label(new Rect(10, 10, 100, 20), string.Format("Active Clusters: {0}", activeTargetClusters.Count));
  }

  private TargetCluster CreateTargetCluster()
  {
    var targetList = new List<GameObject>();

    var targetSpeed = GetTargetSpeed();
    var startPosition = GetStartPosition();
    var stopPosition = GetStopPosition();
    var exitDirection = GetExitDirection();
    var timeStopped = GetTimeStopped();

    for(int x = 0; x < clusterCount; x++)
    {
      for(int z = 0; z < clusterCount; z++)
      {
        var target = Instantiate(prefab, GetStartPosition(), Quaternion.identity);
        target.SendMessage("OnInitialise", new TargetInput 
        { 
          Speed = targetSpeed, 
          StartPosition = startPosition,
          StopPosition = stopPosition,
          Offset = GetOffset(x, z),
          ExitDirection = exitDirection,
          TimeStopped = timeStopped
        });   

        targetList.Add(target);  
      }
    }      
    return new TargetCluster { GameObjects = targetList, Alive = false };
  }

  private Vector3 GetStartPosition()
  {
    return new Vector3(UnityEngine.Random.Range(-75.0f, 75.0f), 0, -200);
  }

  private Vector3 GetStopPosition()
  {
    return Vector3.zero;
  }

  private Vector2 GetOffset(int x, int z)
  {
    return new Vector2(-(x * 10), -(z* 10)); 
  }

  private float GetTargetSpeed()
  {
    return TargetSpeed;
  }

  private Vector3 GetExitDirection()
  {
    var dir = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f));    
    return dir.normalized;
  }

  private int GetTimeStopped()
  {
    return TimeStopped;
  }
}
