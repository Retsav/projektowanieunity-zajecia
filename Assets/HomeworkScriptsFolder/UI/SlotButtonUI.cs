using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotButtonUI : MonoBehaviour
{
     [Header("References")] 
     [SerializeField] private Button button;
     public Image itemImage;
     public TextMeshProUGUI itemValueText;
     private int slotID;

     public ObjectInformationSO objectInformationSO;
     public int currentStackValue;


     private void Start()
     {
          button.onClick.AddListener(HandlePassing);
     }

     
     private void HandlePassing()
     {
          if (objectInformationSO == null)
          {
               TryPassMouseContainerToSlotButton();
          }
          else
          { 
              TryPassObjectToMouseContainer();  
          }
     }
     
     public void TryPassObjectToMouseContainer()
     {
          //Jeżeli myszka jest zajęta
          if (MouseContainerUI.Instance.IsMouseContainerTaken())
          {
               if (MouseContainerUI.Instance.GetMouseEquipmentObjectID() ==
                   objectInformationSO.objectID)
               {
                    currentStackValue += MouseContainerUI.Instance.GetMouseEquipmentCurrentStackValue();
                    EquipmentSystem.Instance.UpdateEquipmentObjectStackValueInArray(slotID, currentStackValue);
                    itemValueText.text = currentStackValue.ToString();
                    MouseContainerUI.Instance.ClearMouseContainer();
               }
          }
          else
          {
               MouseContainerUI.Instance.TryToSetMouseContainerEquipObject(objectInformationSO, EquipmentSystem.Instance.GetEquipmentObjectStackValue(slotID));
               EquipmentSystem.Instance.RemoveEquipmentObjectFromArray(slotID);
               ClearEquipmentObject();    
          }
     }

     public void TryPassMouseContainerToSlotButton()
     {
          if (!MouseContainerUI.Instance.IsMouseContainerTaken()) return;
          SetSlotButton(
               MouseContainerUI.Instance.objectInformationSO, 
               MouseContainerUI.Instance.GetMouseEquipmentCurrentStackValue());
          EquipmentSystem.Instance.AddEquipmentObjectToList(objectInformationSO, currentStackValue, slotID);
          MouseContainerUI.Instance.ClearMouseContainer();
     }
     
     private void ClearEquipmentObject()
     {
          objectInformationSO = null;
          itemImage.sprite = null;
          itemValueText.text = "";
     }

     public void SetSlotButton(ObjectInformationSO objectInformationSO, int currentStackValue)
     {
          this.objectInformationSO = objectInformationSO;
          this.currentStackValue = currentStackValue;
          itemImage.sprite = objectInformationSO.objectImage;
          itemValueText.text = currentStackValue.ToString();
     }

     public void SetSlotID(int id)
     {
          slotID = id;
     }
}
