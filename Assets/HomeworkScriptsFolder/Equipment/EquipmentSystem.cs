using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    public event Action<EquipmentSlot, int> OnObjectListChanged;
    public static EquipmentSystem Instance { get; private set; }
    [SerializeField] private EquipmentSlot[] equipmentObjectArray = new EquipmentSlot[20];

    private const int MAX_SLOTS_COUNT = 20;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one EquipmentSystem!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        PlayerPickup.Instance.OnObjectPickedUp += PlayerPickup_OnOnObjectPickedUp;
    }

    private void PlayerPickup_OnOnObjectPickedUp(ObjectInformationSO objectInfoSO, int objectStackValue)
    {
        EquipmentSlot itemObject = new EquipmentSlot(objectInfoSO, objectStackValue);
        if (equipmentObjectArray.Contains(itemObject)) return;
        for (int i = 0; i <= equipmentObjectArray.Length; i++)
        {
            if (equipmentObjectArray[i].objectInformationSO == null)
            {
                equipmentObjectArray[i].objectInformationSO = objectInfoSO;
                equipmentObjectArray[i].currentStackValue = objectStackValue;
                OnObjectListChanged?.Invoke(itemObject, itemObject.currentStackValue);
                break;
            }
            if(itemObject.objectInformationSO.objectID == equipmentObjectArray[i].objectInformationSO.objectID)
            {
                equipmentObjectArray[i].currentStackValue += objectStackValue;
                int totalStackValue = equipmentObjectArray[i].currentStackValue;
                OnObjectListChanged?.Invoke(itemObject, totalStackValue);
                break;
            }
        }
    }

    public void RemoveEquipmentObjectFromArray(int slotID)
    {
        equipmentObjectArray[slotID].objectInformationSO = null;
        equipmentObjectArray[slotID].currentStackValue = 0;
    }
    
    public void AddEquipmentObjectToList(ObjectInformationSO objectInformationSOToAdd, int currentStackValue, int slotID)
    {
        equipmentObjectArray[slotID].objectInformationSO = objectInformationSOToAdd;
        equipmentObjectArray[slotID].currentStackValue = currentStackValue;
    }

    public void UpdateEquipmentObjectStackValueInArray(int slotID, int stackValue)
    {
        if (equipmentObjectArray[slotID].objectInformationSO == null) Debug.LogError("ObjectInfoSO at " + slotID + " was empty!");
        equipmentObjectArray[slotID].currentStackValue = stackValue;
    }

    public int GetEquipmentObjectStackValue(int slotID)
    {
        return equipmentObjectArray[slotID].currentStackValue;
    }
}
