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
    
    public ObjectInformationSO objectInformationSO;
    public int currentStackValue;

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

    public void TryToSetMouseContainerEquipObject(ObjectInformationSO objectInformationSO, int currentStackValue)
    {
        if (this.objectInformationSO != null) return;
        this.objectInformationSO = objectInformationSO;
        this.currentStackValue = currentStackValue;
        isTaken = true;
        itemImage.enabled = true;
        itemImage.sprite = objectInformationSO.objectImage;
        itemValueText.text = currentStackValue.ToString();
    }

    public void ClearMouseContainer()
    {
        objectInformationSO = null;
        currentStackValue = 0;
        isTaken = false;
        itemImage.enabled = false;
        itemImage.sprite = null;
        itemValueText.text = "";
    }

    public int GetMouseEquipmentObjectID()
    {
        if(objectInformationSO == null) Debug.LogError("Called GetMouseEquipmentObjectID() without objectInformationSO!");
        return objectInformationSO.objectID;
    }
    
    public int GetMouseEquipmentCurrentStackValue()
    {
        if(objectInformationSO == null) Debug.LogError("Called GetMouseEquipmentCurrentStackValue() without objectInformationSO!");
        return currentStackValue;
    }
    
    public bool IsMouseContainerTaken()
    {
        return isTaken;
    }
}