using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHitHandler : MonoBehaviour
{
    public static AxeHitHandler Instance { get; private set; }
    
    
    public class OnDamageEventArgs : EventArgs
    {
        public float damageNumber;
        public Vector3 contactPos;

        public OnDamageEventArgs(float damageNumber, Vector3 contactPos)
        {
            this.damageNumber = damageNumber;
            this.contactPos = contactPos;
        }
    }


    public event EventHandler<OnDamageEventArgs> OnDamage;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one AxeHitHandler!");
            Destroy(this);
            return;
        }
        Instance = this;
    }

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
