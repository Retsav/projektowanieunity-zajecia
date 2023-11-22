using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSystemUI : MonoBehaviour
{
    public static EquipmentSystemUI Instance { get; private set; }
    
    
    [Header("References")]
    [SerializeField] private GameObject equipmentSystemUI;
    [SerializeField] private GameObject slotGrid;
    
    
    
    private List<SlotButtonUI> slotButtons = new();
    private bool isActive;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one EquipmentSystemUI! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        HandleUIActivation();
        //Nadpisanie lockowania myszki z assetu
        HandleMouseLock();
        HandleObjectDropping();
    }

    private void HandleUIActivation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isActive = !isActive;
            equipmentSystemUI.SetActive(isActive);
        }
    }

    private void HandleObjectDropping()
    {
        if (!isActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (!MouseContainerUI.Instance.IsMouseContainerTaken()) return;
            GameObject droppedObject = Instantiate(
                MouseContainerUI.Instance.objectInformationSO.objectPrefab,
                Player.Instance.GetPlayerObjectDropTransform().position, 
                Quaternion.identity);
            if (droppedObject.TryGetComponent(out BaseObject droppedBaseObject))
            {
                droppedBaseObject.SetStackValue(MouseContainerUI.Instance.currentStackValue);
                MouseContainerUI.Instance.ClearMouseContainer();
            }
            else
            {
                Debug.LogError("Created dropped object does not have BaseObject component!");
            }
        }
    }

    private void HandleMouseLock()
    {
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
        int IdIndex = 0;
        foreach (Transform child in slotGrid.transform)
        {
            if (child.TryGetComponent(out SlotButtonUI slotButtonUI))
            {
                if(slotButtons.Contains(slotButtonUI)) continue;
                slotButtonUI.SetSlotID(IdIndex);
                IdIndex++;
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
            if (slotButton.objectInformationSO != null)
            {
                //Jeżeli tak, sprawdzamy czy obiekt jest taki sam jak na slocie
                if (slotButton.objectInformationSO.objectID ==
                    obj.objectInformationSO.objectID)
                {
                    //Jeżeli tak, zaktualizuj liczbę
                    slotButton.itemValueText.text = stackVal.ToString();
                    break;
                }
            }
            slotButton.SetSlotButton(obj.objectInformationSO, stackVal);
            break;
        }
    }

    public bool IsEquipmentUIActive()
    {
        return isActive;
    }
}