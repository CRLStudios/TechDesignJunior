using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<ProgressFlag> progressFlags = new List<ProgressFlag>();
    [SerializeField] private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public event UnityAction<string, float> OnProgressFlagChanged;
    
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

    public bool HasInventoryItem(string itemId)
    {
        return inventoryItems.Exists(x => x.name == itemId && x.quantity > 0);
    }

    public void AddInventoryItem(string itemId, int amount = 1)
    {
        var item = inventoryItems.FirstOrDefault(x => x.name == itemId);
        if (item != null)
        {
            item.quantity += amount;
        }
        else
        {
            inventoryItems.Add(new InventoryItem
            {
                name = itemId,
                quantity = amount
            });
        }
    }

    public bool TryRemoveInventoryItem(string itemId, int amount = 1)
    {
        var item = inventoryItems.FirstOrDefault(x => x.name == itemId);
        if (item != null && item.quantity >= amount)
        {
            item.quantity -= amount;
            return true;
        }
        
        return false;
    }
    
}
