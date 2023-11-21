using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogFall : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform logForcePos;
    [SerializeField] private Rigidbody rb;

    [Header("Settings")] 
    [SerializeField] private float forceMultiplier = 5f;

    private void OnEnable()
    {
        Debug.Log("Fall!");
        rb.AddForceAtPosition(PlayerDirection.Instance.GetPlayerDirection() * forceMultiplier, logForcePos.position, ForceMode.Force);
    }
}