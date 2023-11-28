using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageCounterUI : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TextMeshProUGUI damageCounterGameObject;
    private void Start()
    {
        AxeHitHandler.OnDamage += OnDamage;
        LogsFallDamageHandler.Instance.SendDamageToUI += OnDamage;
        TreeEvents.TreeDealtDamage += OnDamage;
    }

    private void OnDamage(object sender, OnDamageEventArgs e)
    {
        Vector3 position = GetCamera.Instance.GetMainCamera().WorldToScreenPoint(e.contactPos);
        TextMeshProUGUI damageCounterText = Instantiate(damageCounterGameObject, position, Quaternion.identity, transform);
        damageCounterText.text = e.damageNumber.ToString();
    }
}
