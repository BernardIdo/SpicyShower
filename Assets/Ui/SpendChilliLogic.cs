using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpendChilliLogic : MonoBehaviour
{
    private const string KCurrentStepIndexKey = "ChilliStep";
    public AudioSource audioSource;
    public TMP_Text costText;
    
    [SerializeField] private List<SpendChilliStep> steps;
    private int _currentStep;
    
    [Serializable]
    private struct SpendChilliStep
    {
        public float cost;
        public AudioClip interaction;
    }
    
    void Start()
    {
        _currentStep = PlayerPrefs.GetInt(KCurrentStepIndexKey, 0);
        costText.text = steps[_currentStep].cost.ToString("n0");
    }

    public void TrySpendChillies()
    {
        if (Wallet.instance.Currency >= steps[_currentStep].cost)
        {
            UpdateStep();
        }
    }

    private void UpdateStep()
    {
        if (_currentStep >= steps.Count)
        {
            _currentStep = 0;
        }
        else
        {
            _currentStep++;
        }
        PlayerPrefs.SetInt(KCurrentStepIndexKey, _currentStep);
        costText.text = steps[_currentStep].cost.ToString("n0");
        Wallet.instance.SpendCurrency(steps[_currentStep].cost);
        audioSource.Stop();
        audioSource.clip = steps[_currentStep].interaction;
        audioSource.Play();
    }
}
