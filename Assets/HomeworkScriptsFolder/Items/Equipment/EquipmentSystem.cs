using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    public event Action<EquipmentSlot, int> OnObjectListChanged;
    public static EquipmentSystem Instance { get; private set; }
    [SerializeField] private List<EquipmentSlot> equipmentObjectList = new();

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

    private void PlayerPickup_OnOnObjectPickedUp(BaseObject baseObject)
    {
        EquipmentSlot itemObject = new EquipmentSlot(baseObject, baseObject.GetStackValue());
        if (equipmentObjectList.Contains(itemObject)) return;
        if (equipmentObjectList.Count >= MAX_SLOTS_COUNT) return;
        if (equipmentObjectList.Count == 0)
        {
            equipmentObjectList.Add(itemObject);
            OnObjectListChanged?.Invoke(itemObject, itemObject.currentStackValue);
            return;
        }
        foreach (EquipmentSlot equipmentSlot in equipmentObjectList)
        {
            if (itemObject.baseObject.GetObjectInfoSO().objectID == equipmentSlot.baseObject.GetObjectInfoSO().objectID)
            {
                equipmentSlot.currentStackValue += itemObject.baseObject.GetStackValue();
                int totalStackValue = equipmentSlot.currentStackValue;
                OnObjectListChanged?.Invoke(itemObject, totalStackValue);
            }
            else
            {
                equipmentObjectList.Add(itemObject);
                OnObjectListChanged?.Invoke(itemObject, itemObject.currentStackValue);
            }
        }
    }

    public void RemoveEquipmentObjectFromList(EquipmentSlot equipmentItem)
    {
        if (equipmentObjectList.Contains(equipmentItem))
        {
            equipmentObjectList.Remove(equipmentItem);
        }
    }
    
    public void AddEquipmentObjectToList(EquipmentSlot equipmentItem)
    {
        equipmentObjectList.Add(equipmentItem);
    }
}
