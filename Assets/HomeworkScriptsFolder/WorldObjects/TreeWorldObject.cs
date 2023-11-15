using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeWorldObject : WorldObject
{
    [Header("References")]
    [SerializeField] private Transform treeStumpPos;
    [SerializeField] private Transform treeLogPos;
    [SerializeField] private GameObject treeStumpPrefab;
    [SerializeField] private GameObject treeLogPrefab;

    protected override void DestroyWorldObject()
    {
        Instantiate(treeLogPrefab, treeLogPos.position, Quaternion.identity);
        Instantiate(treeStumpPrefab, treeStumpPos.position, Quaternion.identity);
        base.DestroyWorldObject();
    }
}
