using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    protected float worldObjectMaxHealth = 100f;
    protected float worldObjectCurrentHealth;
    private bool beenHit;
  
    

    protected virtual void Awake()
    {
        worldObjectCurrentHealth = worldObjectMaxHealth;
    }

    public void DamageWorldObject(float damageVal)
    {
        if (beenHit) return;
        beenHit = true;
        StartCoroutine(ResetHit());
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

    IEnumerator ResetHit()
    {
        yield return new WaitUntil(() => !PlayerToolUse.Instance.GetIsSweeping());
        beenHit = false;
    }
}