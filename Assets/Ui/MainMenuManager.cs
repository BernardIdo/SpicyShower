using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public CanvasGroup scoreCanvasGroup;
    public float fadeDuration = 0.5f;

    private void Start()
    {
        scoreCanvasGroup.alpha = 0;
    }

    [ContextMenu("Fade")]
    public void StartGame()
    {
        CanvasGroup.DOFade(0, fadeDuration).onComplete = () => ToggleClick(false);
        scoreCanvasGroup.DOFade(1, fadeDuration);
        GameManager.instance.StartPlayer();
    }

    private void ToggleClick(bool canClick)
    {
        CanvasGroup.interactable = canClick;
    }

    [ContextMenu("Fade in")]
    public void Open()
    {
        ToggleClick(true);
        CanvasGroup.DOFade(1, fadeDuration);
        scoreCanvasGroup.DOFade(0, fadeDuration);
    }
}
