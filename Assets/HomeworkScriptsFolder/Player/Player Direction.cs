using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    //Te logike wrzuciłbym do skryptu który odpowiada za poruszanie się postaci, ale z racji że pochodzi z zewnętrznego
    //assetu to w niej nie grzebałem, żebyś miał łatwiej sprawdzić :)
    public static PlayerDirection Instance { get; private set; }
    private Vector3 playerDirection;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerDirection Instance!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        playerDirection = transform.forward;
    }

    public Vector3 GetPlayerDirection()
    {
        return playerDirection;
    }
}
