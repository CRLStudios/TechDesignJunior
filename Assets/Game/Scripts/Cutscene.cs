using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public interface ICutscene
{
    public string CutsceneId { get; }
    public List<CutsceneAction> Actions { get; }
}

public class Cutscene : MonoBehaviour, ICutscene
{
    [ShowNativeProperty]
    public string CutsceneId => name;
    
    [SerializeField] private List<CutsceneAction> actions;
    public List<CutsceneAction> Actions => actions;

    private void Start()
    {
        CutsceneManager.Instance.RegisterCutscene(this);
    }
    
}
