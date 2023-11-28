using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TreeEvents
{
    public static EventHandler<OnDamageEventArgs> TreeDealtDamage;
    public static void OnTreeDealDamage(object sender, OnDamageEventArgs args) => TreeEvents.TreeDealtDamage?.Invoke(sender, args);
}

public class LogsFallDamageHandler : MonoBehaviour
{
    public static LogsFallDamageHandler Instance { get; private set; }
    public event EventHandler<OnDamageEventArgs> SendDamageToUI; 
    
    [Header("References")]
    [SerializeField] private List<TreeWorldObject> treesList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one LogsFallDamageHandler!");
            Destroy(this);
            return;
        }
        Instance = this;
        foreach (TreeWorldObject treeWorldObject in treesList)
        {
            treeWorldObject.OnTreeLogCreated += TreeWorldObject_OnTreeLogCreated;
        }
    }

    private void TreeWorldObject_OnTreeLogCreated(object sender, GameObject e)
    {
        if (e.TryGetComponent(out LogCollision logCollision))
        {
            logCollision.OnFallDamage += LogCollisionOnOnFallDamage;
        }
        else
        {
            Debug.LogError("There is no logCollision on TreeWorldObject!");
        }
    }

    private void LogCollisionOnOnFallDamage(object sender, OnDamageEventArgs e)
    {
        SendDamageToUI?.Invoke(this, new OnDamageEventArgs(e.damageNumber, e.contactPos));
    }

    public void RemoveTreeFromList(TreeWorldObject treeWorldObject)
    {
        if (treesList.Contains(treeWorldObject))
        {
            treesList.Remove(treeWorldObject);
        }
        else
        {
            Debug.LogError("Uninstall Unity you fucking dumbass.");
        }
    }
}
