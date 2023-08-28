using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private const string KHighestScoreKey = "highestScore";
    public TMP_Text scoreText;
    public TMP_Text highestScoreText;
    public float heightToScoreRatio = 1;
    public float checkpointInterval = 50f;
    public float checkpointReward = 10;
    
    public float Score { get; private set; } = 0;
    private Transform _playerTransform;
    private bool _active;
    private float _highestY;
    private float _lastCheckPoint;

    private void Start()
    {
        _highestY = 0;
        _lastCheckPoint = 0;
        highestScoreText.text = PlayerPrefs.GetFloat(KHighestScoreKey, 0).ToString("N1");
        UpdateScore();
    }

    public void StartCounting(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _active = true;
        _highestY = 0;
        _lastCheckPoint = 0;
        UpdateScore();
    }

    public void StopCounting()
    {
        if (_highestY > PlayerPrefs.GetFloat(KHighestScoreKey, 0))
        {
            highestScoreText.text =_highestY.ToString("N1");
            PlayerPrefs.SetFloat(KHighestScoreKey, _highestY);
        }
        _active = false;
    }

    private void Update()
    {
        if (!_active)
        {
            return;
        }

        var currentY = _playerTransform.position.y;
        if (currentY > _highestY)
        {
            _highestY = currentY;
            UpdateScore();
            if (_highestY - _lastCheckPoint > checkpointInterval)
            {
                var rewardCount = (_highestY - _lastCheckPoint) % checkpointInterval;
                _lastCheckPoint = _highestY;
                Wallet.instance.AddCurrency(rewardCount * checkpointReward);
            }
        }
    }

    private void UpdateScore()
    {
        Score = heightToScoreRatio * _highestY;
        scoreText.text = Score.ToString("n0");
    }
}
