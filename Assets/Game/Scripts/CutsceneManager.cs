using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate IEnumerator CutsceneActionDelegate(CutsceneState cutsceneState, CutsceneAction action);

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance { get; private set; }

    private readonly List<CutsceneState> _cutsceneStates = new List<CutsceneState>();
    
    public event UnityAction OnCutsceneStarted;
    public event UnityAction OnCutsceneFinished;
    public event UnityAction OnAllCutscenesFinished;
    
    private readonly Dictionary<int,CutsceneActionDelegate> _actionDictionary = new Dictionary<int, CutsceneActionDelegate>();
    private readonly Dictionary<string,ICutscene> _cutscenes = new Dictionary<string, ICutscene>();
    
    public bool IsBusy => _cutsceneStates.Count > 0;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void RegisterActionBehaviour(int actionId, CutsceneActionDelegate action)
    {
        _actionDictionary.Add(actionId, action);
    }
    
    public void UnregisterActionBehaviour(int actionId)
    {
        _actionDictionary.Remove(actionId);
    }
    
    public Coroutine RunCutscene(ICutscene cutscene)
    {
        var cutsceneState = new CutsceneState
        {
            cutscene = cutscene,
            currentActionIndex = 0
        };
        _cutsceneStates.Add(cutsceneState);
        return StartCoroutine(InternalRunCutscene(cutsceneState));
    }
    
    public Coroutine RunCutscene(string cutsceneId)
    {
        if (_cutscenes.TryGetValue(cutsceneId, out var cutscene))
        {
            return RunCutscene(cutscene);
        }
        Debug.LogError($"Cutscene with id {cutsceneId} not found");
        return null;
    }

    private void StartCutscene(CutsceneState cutsceneState)
    {
        OnCutsceneStarted?.Invoke();
    }
    
    private void FinishCutscene(CutsceneState cutsceneState)
    {
        _cutsceneStates.Remove(cutsceneState);
        OnCutsceneFinished?.Invoke();
        if (_cutsceneStates.Count == 0)
        {
            OnAllCutscenesFinished?.Invoke();
        }
    }
    
    private IEnumerator InternalRunCutscene(CutsceneState cutsceneState)
    {
        StartCutscene(cutsceneState);
        while (cutsceneState.currentActionIndex < cutsceneState.cutscene.Actions.Count)
        {
            var action = cutsceneState.cutscene.Actions[cutsceneState.currentActionIndex];
            if (_actionDictionary.TryGetValue(action.ActionId, out var actionDelegate))
            {
                yield return actionDelegate(cutsceneState, action);
            }
            else
            {
                Debug.LogError($"No action delegate found for action id {action.ActionId}");
            }
            cutsceneState.currentActionIndex++;
        }
        FinishCutscene(cutsceneState);
    }

    public void RegisterCutscene(ICutscene cutscene)
    {
        Debug.Assert(cutscene != null, "Cutscene is null");
        if (!_cutscenes.TryAdd(cutscene.CutsceneId, cutscene))
        {
            Debug.LogError($"Cutscene with the same id {cutscene.CutsceneId} already exists");
        }
    }
    
}
