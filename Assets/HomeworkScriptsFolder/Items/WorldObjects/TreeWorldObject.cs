using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeWorldObject : WorldObject
{
    public event EventHandler<GameObject> OnTreeLogCreated;  


    [Header("References")]
    [SerializeField] private Transform treeStumpPos;
    [SerializeField] private Transform treeLogPos;
    [SerializeField] private GameObject treeStumpPrefab;
    [SerializeField] private GameObject treeLogPrefab;

    protected override void DestroyWorldObject()
    {
        GameObject treeLog = Instantiate(treeLogPrefab, treeLogPos.position, Quaternion.identity);
        OnTreeLogCreated?.Invoke(this, treeLog);
        Instantiate(treeStumpPrefab, treeStumpPos.position, Quaternion.identity);
        LogsFallDamageHandler.Instance.RemoveTreeFromList(this);
        base.DestroyWorldObject();
    }
}