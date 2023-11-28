using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform playerObjectDropTransform;
    
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player Instance!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Transform GetPlayerObjectDropTransform()
    {
        return playerObjectDropTransform;
    }
}
