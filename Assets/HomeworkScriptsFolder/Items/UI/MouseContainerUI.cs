using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseContainerUI : MonoBehaviour
{
    public static MouseContainerUI Instance { get; private set; }
    
    [Header("References")]
    public Image itemImage;
    public TextMeshProUGUI itemValueText;
    public EquipmentSlot equipObject;

    private Vector3 mouseContainerOffset = new Vector3(35, -30, 0);
    private bool isTaken;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one MouseContainerUI!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        transform.position = Input.mousePosition + mouseContainerOffset;
    }

    public void TryToSetMouseContainerEquipObject(EquipmentSlot equipObject, Sprite itemSprite, int currentStackValue)
    {
        if (this.equipObject != null) return;
        this.equipObject = equipObject;
        isTaken = true;
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;
        itemValueText.text = currentStackValue.ToString();
    }

    public void ClearMouseContainer()
    {
        equipObject = null;
        isTaken = false;
        itemImage.enabled = false;
        itemImage.sprite = null;
        itemValueText.text = "";
    }

    public int GetMouseEquipmentObjectID()
    {
        if(equipObject == null) Debug.LogError("Called GetMouseEquipmentObjectID() without equipObject!");
        return equipObject.baseObject.GetObjectInfoSO().objectID;
    }
    
    public int GetMouseEquipmentCurrentStackValue()
    {
        if(equipObject == null) Debug.LogError("Called GetMouseEquipmentCurrentStackValue() without equipObject!");
        return equipObject.currentStackValue;
    }
    
    public bool IsMouseContainerTaken()
    {
        return isTaken;
    }
}
