using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    protected float worldObjectMaxHealth = 100f;
    protected float worldObjectCurrentHealth;

    protected virtual void Awake()
    {
        worldObjectCurrentHealth = worldObjectMaxHealth;
    }

    public void DamageWorldObject(float damageVal)
    {
        worldObjectCurrentHealth -= damageVal;
        if (worldObjectCurrentHealth <= 0)
        {
           DestroyWorldObject(); 
        }
    }

    protected virtual void DestroyWorldObject()
    {
        Destroy(gameObject);
    }
}
