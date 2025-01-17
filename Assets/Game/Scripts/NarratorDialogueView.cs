using DG.Tweening;
using TMPro;
using UnityEngine;

public class NarratorDialogueView : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueTextLabel;
    [SerializeField] private CanvasGroup canvasGroup;
    
    public void Show(string optionsText)
    {
        dialogueTextLabel.text = optionsText;
        gameObject.SetActive(true);
        canvasGroup.DOKill();
        canvasGroup.DOFade(1, 0.2f);
    }

    public void Hide()
    {
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 0.2f).OnComplete(OnHideComplete);
    }

    private void OnHideComplete()
    {
        gameObject.SetActive(false);
    }
}
