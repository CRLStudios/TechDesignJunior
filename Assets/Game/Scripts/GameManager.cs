using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<ProgressFlag> progressFlags = new List<ProgressFlag>();
    [SerializeField] private string inventoryItem;

    public event UnityAction<string, float> OnProgressFlagChanged;
    public event UnityAction<string> OnInventoryChanged;

    public string InventoryItem
    {
        get => inventoryItem;
        set
        {
            inventoryItem = value;
            OnInventoryChanged?.Invoke(value);
        }
    }
    
    private void Awake()
    {
        Instance = this;
    }

    public void SetProgressFlag(string flagName, float value)
    {
        var flag = progressFlags.Find(f => f.Key == flagName);
        if (flag != null)
        {
            flag.SetValue(value);
        }
        else
        {
            progressFlags.Add(new ProgressFlag(flagName, value));
        }
        OnProgressFlagChanged?.Invoke(flagName, value);
    }

    public float GetProgressFlagValue(string flagName, float defaultValue = 0)
    {
        var flag = progressFlags.FirstOrDefault(f => f.Key == flagName);
        return flag?.Value ?? defaultValue;
    }

}
