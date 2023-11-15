using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EquipmentSlot
{
    public ObjectInformationSO objectInformationSO;
    public int currentStackValue;

    public EquipmentSlot(ObjectInformationSO objectInformationSO, int currentStackValue)
    {
        this.objectInformationSO = objectInformationSO;
        this.currentStackValue = currentStackValue;
    }
}
