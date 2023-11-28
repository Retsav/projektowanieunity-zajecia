using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextUI : MonoBehaviour
{
    private RectTransform rectTransfom;
    private TextMeshProUGUI text;
    private Tween moveUp;
    private Tween fade; 

    [Header("Settings")]
    [SerializeField] private float tweenFlyingDuration = 4f;
    [SerializeField] private float tweenNumberFlyOffset = 100f;
    [SerializeField] private float tweenFadeDuration = 2f;
    [SerializeField] private float tweenDoColorDuration = 1f;
    
    private void OnEnable()
    {
        rectTransfom = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
        moveUp = rectTransfom.DOAnchorPosY(rectTransfom.rect.y + tweenNumberFlyOffset, tweenFlyingDuration);
        fade = text.DOFade(0f, tweenFadeDuration);
        text.DOColor(Color.gray, tweenDoColorDuration).OnComplete(() => CleanUp());
    }

    private void CleanUp()
    {
        fade.Kill();
        moveUp.Kill();
        Destroy(gameObject);
    }
}
