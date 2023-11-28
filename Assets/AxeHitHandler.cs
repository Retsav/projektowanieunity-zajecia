using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHitHandler : MonoBehaviour
{
    public static event EventHandler<OnDamageEventArgs> OnDamage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerToolUse.Instance.GetIsSweeping())
        {
            if (other.TryGetComponent(out WorldObject worldObject))
            {
                Debug.Log("ENTER!");
                worldObject.DamageWorldObject(PlayerToolUse.Instance.GetDamage());
                OnDamage?.Invoke(this, new OnDamageEventArgs(PlayerToolUse.Instance.GetDamage(), other.ClosestPoint(transform.position)));
            }
        }
    }
}
