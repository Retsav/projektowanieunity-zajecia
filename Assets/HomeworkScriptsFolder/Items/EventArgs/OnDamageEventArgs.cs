using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
