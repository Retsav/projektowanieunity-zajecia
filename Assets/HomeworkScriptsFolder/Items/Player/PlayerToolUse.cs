using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerToolUse : MonoBehaviour
{
    public static PlayerToolUse Instance { get; private set; }
    
    
    [Header("References")]
    [SerializeField] private Animator animator;
    [Header("Settings")]
    [SerializeField] private float toolDamage = 20f;


    private const string ATTACK_TRIGGER = "Attack";
    private bool isSweeping;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerToolUse Instance!");
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EquipmentSystemUI.Instance.IsEquipmentUIActive()) return;
        animator.SetTrigger(ATTACK_TRIGGER);
    }
    public void ChangeSphereActive(int isSweeping)
    {
        this.isSweeping = Convert.ToBoolean(isSweeping);
    }

    public bool GetIsSweeping()
    {
        return isSweeping;
    }

    public float GetDamage()
    {
        return toolDamage;
    }
}