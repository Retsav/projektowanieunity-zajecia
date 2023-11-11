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
     public EquipmentSlot equipObject;


     private void Start()
     {
          button.onClick.AddListener(HandlePassing);
     }


     private void HandlePassing()
     {
          if (equipObject == null)
          {
               Debug.Log("EquipObject was Null!!");
               TryPassMouseContainerToSlotButton();
          }
          else
          { 
               Debug.Log("EquipObject was not Null!!");
              TryPassObjectToMouseContainer();  
          }
     }
     
     public void TryPassObjectToMouseContainer()
     {
          //Jeżeli myszka jest zajęta
          if (MouseContainerUI.Instance.IsMouseContainerTaken())
          {
               if (MouseContainerUI.Instance.GetMouseEquipmentObjectID() ==
                   equipObject.baseObject.GetObjectInfoSO().objectID)
               {
                    equipObject.currentStackValue += MouseContainerUI.Instance.GetMouseEquipmentCurrentStackValue();
                    itemValueText.text = equipObject.currentStackValue.ToString();
                    MouseContainerUI.Instance.ClearMouseContainer();
               }
          }
          else
          {
               MouseContainerUI.Instance.TryToSetMouseContainerEquipObject(equipObject, itemImage.sprite, equipObject.currentStackValue);
               EquipmentSystem.Instance.RemoveEquipmentObjectFromList(equipObject);
               ClearEquipmentObject();    
          }
     }

     public void TryPassMouseContainerToSlotButton()
     {
          if (!MouseContainerUI.Instance.IsMouseContainerTaken()) return;
          SetSlotButton(
               MouseContainerUI.Instance.equipObject, 
               MouseContainerUI.Instance.itemImage.sprite, 
               MouseContainerUI.Instance.GetMouseEquipmentCurrentStackValue());
          EquipmentSystem.Instance.AddEquipmentObjectToList(equipObject);
          MouseContainerUI.Instance.ClearMouseContainer();
     }
     
     private void ClearEquipmentObject()
     {
          equipObject = null;
          itemImage.sprite = null;
          itemValueText.text = "";
     }

     public void SetSlotButton(EquipmentSlot equipObject, Sprite itemSprite, int currentStackValue)
     {
          this.equipObject = equipObject;
          itemImage.sprite = itemSprite;
          itemValueText.text = currentStackValue.ToString();
     }
}
