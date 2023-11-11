using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public static PlayerPickup Instance { get; private set; }
    public event Action<BaseObject> OnObjectPickedUp; 
    
    [SerializeField] private float playerItemDetectionRadius = 1f;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerPickup Singleton!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, playerItemDetectionRadius);
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerItemDetectionRadius, layerMask);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out BaseObject baseObject))
            {
                OnObjectPickedUp?.Invoke(baseObject);
                Destroy(baseObject.gameObject);
            }
        }
    }
}
