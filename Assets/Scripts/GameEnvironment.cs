using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public sealed class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> checkPoints = new List<GameObject>();
    private Transform safeZone;

    public List<GameObject> CheckPoints { get => checkPoints; }
    public Transform SafeZone { get => safeZone; }

    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
                instance.CheckPoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint").OrderBy(wayPoint => wayPoint.name).ToList());
                instance.safeZone = GameObject.FindGameObjectWithTag("Safe").transform;
            }
            return instance;
        }
    }        
}
