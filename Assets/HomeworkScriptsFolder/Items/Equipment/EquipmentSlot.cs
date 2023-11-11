using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentSlot
{
    public BaseObject baseObject;
    public int currentStackValue;

    public EquipmentSlot(BaseObject baseObject, int currentStackValue)
    {
        this.baseObject = baseObject;
        this.currentStackValue = currentStackValue;
    }
}
