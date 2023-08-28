using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public float fadeDuration = 0.5f;
    
    [ContextMenu("Fade")]
    public void StartGame()
    {
        CanvasGroup.DOFade(0, fadeDuration).onComplete = () => ToggleClick(false);
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
    }
}
