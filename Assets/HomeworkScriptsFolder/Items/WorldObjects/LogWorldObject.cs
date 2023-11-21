using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LogWorldObject : WorldObject
{
    [Header("References")] 
    [SerializeField] private GameObject woodPickableObjectPrefab;
    
    [Header("Settings")] 
    [SerializeField] private int woodPickableObjectDropMinNumber;
    [SerializeField] private int woodPickableObjectDropMaxNumber;

    protected override void DestroyWorldObject()
    {
        int randomWoodNumber = Random.Range(woodPickableObjectDropMinNumber, woodPickableObjectDropMaxNumber);
        for (int i = 0; i <= randomWoodNumber; i++)
        {
            Instantiate(woodPickableObjectPrefab, transform.position, quaternion.identity);
        }
        base.DestroyWorldObject();
    }
}