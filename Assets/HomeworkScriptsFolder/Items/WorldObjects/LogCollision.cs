using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WorldObject))]
public class LogCollision : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float damageVelocityMultiplier = 10f;


    private LogWorldObject logWorldObject;
    
    public event EventHandler<OnDamageEventArgs> OnFallDamage;


    private void Awake()
    {
        logWorldObject = GetComponent<LogWorldObject>();
    }

    private void OnCollisionEnter(Collision other)
    {
        float fallDamage = other.relativeVelocity.magnitude * damageVelocityMultiplier;
        logWorldObject.DamageWorldObject(fallDamage);
        OnFallDamage?.Invoke(this, new OnDamageEventArgs(fallDamage, other.contacts[0].point));
    }
}
