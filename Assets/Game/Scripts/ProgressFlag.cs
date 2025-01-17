using System;
using UnityEngine;

[Serializable]
public class ProgressFlag
{
    [SerializeField] public string key;
    [SerializeField] public float value;

    public string Key => key;
    public float Value => value;
    
    public ProgressFlag()
    {
        key = string.Empty;
        value = 0;
    }
    
    public ProgressFlag(string key, float value)
    {
        this.key = key;
        this.value = value;
    }

    public void SetValue(float newValue)
    {
        value = newValue;
    }
}
