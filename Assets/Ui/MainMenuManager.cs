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
    public CanvasGroup pauseMenuCanvasGroup;
    public float fadeDuration = 0.5f;
    private bool _paused;

    private void Start()
    {
        scoreCanvasGroup.alpha = 0;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;
        pauseMenuCanvasGroup.alpha = 0;
    }

    [ContextMenu("Fade")]
    public void StartGame()
    {
        ToggleCanvasGroup(CanvasGroup, false);
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
        ToggleCanvasGroup(CanvasGroup, true);
    }

    public void TogglePause()
    {
        if (_paused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _paused = true;
        ToggleCanvasGroup(pauseMenuCanvasGroup, true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        _paused = false;
        ToggleCanvasGroup(pauseMenuCanvasGroup, false);
    }

    private void ToggleCanvasGroup(CanvasGroup canvasGroup, bool isOn)
    {
        if (isOn)
        {
            var tween =
                canvasGroup.DOFade(1, fadeDuration);

            tween.SetUpdate(true);
            tween.onComplete = () =>
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            };
            
            scoreCanvasGroup.DOFade(0, fadeDuration);

        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            scoreCanvasGroup.DOFade(1, fadeDuration).SetUpdate(true);
            canvasGroup.DOFade(0, fadeDuration);
        }
    }

    public void RestartGame()
    {
        UnPause();
        GameManager.instance.StartPlayer();
    }
}
