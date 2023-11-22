using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToolUse : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform rayTransformPoint;
    [SerializeField] private LayerMask worldObjectLayer;
    [Header("Settings")]
    [SerializeField] private float raySphereDistance = 2f;
    [SerializeField] private float raySphereRadius = .1f;
    [SerializeField] private float toolDamage = 20f;


    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EquipmentSystemUI.Instance.IsEquipmentUIActive()) return;
        if (Physics.SphereCast(rayTransformPoint.position, raySphereRadius, transform.forward, out RaycastHit hit,
                raySphereDistance, worldObjectLayer))
        {
            if (hit.transform.TryGetComponent(out WorldObject worldObject))
            {
                worldObject.DamageWorldObject(toolDamage);
            }
            else
            {
                Debug.LogError("GameObject with WorldObject Layer does not have a WorldObject component!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(rayTransformPoint.position + transform.forward, raySphereRadius * raySphereDistance);
    }
}