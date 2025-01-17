using System;
using UnityEngine;

public class ProgressFlagEnabler : MonoBehaviour
{
    public enum Comparison
    {
        Equal,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }
    
    [SerializeField] private string flagName;
    [SerializeField] private Comparison compareType;
    [SerializeField] private float compareValue;
    
    private void Start()
    {
        GameManager.Instance.OnProgressFlagChanged += OnProgressFlagChanged;
        Refresh();
    }
    
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnProgressFlagChanged -= OnProgressFlagChanged;
        }
    }

    private void Refresh()
    {
        var currentValue = GameManager.Instance.GetProgressFlagValue(flagName);
        gameObject.SetActive(Compare(currentValue));
    }
    
    private bool Compare(float currentValue)
    {
        switch (compareType)
        {
            case Comparison.Equal:
                return Mathf.Approximately(currentValue, compareValue);
            case Comparison.GreaterThan:
                return currentValue > compareValue;
            case Comparison.LessThan:
                return currentValue < compareValue;
            case Comparison.GreaterThanOrEqual:
                return currentValue >= compareValue;
            case Comparison.LessThanOrEqual:
                return currentValue <= compareValue;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void OnProgressFlagChanged(string arg0, float arg1)
    {
        Refresh();
    }
}
