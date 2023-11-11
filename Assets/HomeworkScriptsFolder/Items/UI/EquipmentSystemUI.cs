using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject equipmentSystemUI;
    [SerializeField] private GameObject slotGrid;
    
    private List<SlotButtonUI> slotButtons = new();
    private bool isActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isActive = !isActive;
            equipmentSystemUI.SetActive(isActive);
        }
        //Nadpisanie lockowania myszki z assetu
        if (isActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    private void Start()
    {
        EquipmentSystem.Instance.OnObjectListChanged += EquipmentSystem_OnObjectListChanged;
        foreach (Transform child in slotGrid.transform)
        {
            if (child.TryGetComponent(out SlotButtonUI slotButtonUI))
            {
                if(slotButtons.Contains(slotButtonUI)) continue;
                slotButtons.Add(slotButtonUI);
            }
            else
            {
                Debug.LogError("Couldn't find slotButtonUI in Container transform. Is there a wrong gameobject?");
            }
        }
    }

    private void EquipmentSystem_OnObjectListChanged(EquipmentSlot obj, int stackVal)
    {
        foreach (SlotButtonUI slotButton in slotButtons)
        {
            //Sprawdzamy czy slot jest zajęty
            if (slotButton.equipObject != null)
            {
                //Jeżeli tak, sprawdzamy czy obiekt jest taki sam jak na slocie
                if (slotButton.equipObject.baseObject.GetObjectInfoSO().objectID ==
                    obj.baseObject.GetObjectInfoSO().objectID)
                {
                    //Jeżeli tak, zaktualizuj liczbę
                    slotButton.itemValueText.text = stackVal.ToString();
                    break;
                }
            }
            slotButton.SetSlotButton(obj, obj.baseObject.GetObjectInfoSO().objectImage, stackVal);
            break;
        }
    }
}
