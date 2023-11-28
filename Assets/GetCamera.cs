using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCamera : MonoBehaviour
{
    public static GetCamera Instance { get; private set; }
    private Camera mainCam;
    
    private void Awake()
    {
        mainCam = GetComponent<Camera>();
        if (Instance != null)
        {
            Debug.LogError("There is more than one GetCamera Instance!");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public Camera GetMainCamera()
    {
        return mainCam;
    }
}
