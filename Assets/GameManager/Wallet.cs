using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet instance;
    public float Currency { get; private set; }
    private const string KCurrencyKey = "currency";
    
    public TMP_Text currencyText;


    private void Awake()
    {
        instance = this;
        Currency = PlayerPrefs.GetFloat(KCurrencyKey, 0);
        currencyText.text = Currency.ToString();
    }

    public void AddCurrency(float amount)
    {
        var securedAmount = Mathf.Abs(amount);
        UpdateCurrency(securedAmount);
    }
    
    public void SpendCurrency(float amount)
    {
        var securedAmount = Mathf.Abs(amount);
        UpdateCurrency(- securedAmount);
    }

    private void UpdateCurrency(float delta)
    {
        Currency += delta;
        PlayerPrefs.SetFloat(KCurrencyKey, Currency);
        currencyText.text = Currency.ToString("n1");
    }
}
