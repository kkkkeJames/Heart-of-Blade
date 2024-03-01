using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationPopupMenu : UI
{
    public static ConfirmationPopupMenu Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private ButtonUI confirmButton;
    [SerializeField] private ButtonUI cancelButton;

    protected override void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Hide();
    }

    public void ActivateMenu(string text, Action confirmAction, Action cancelAction)
    {
        displayText.text = text;
        Show();

        // note - this only removes listeners added through code
        confirmButton.RemoveAllListeners();
        cancelButton.RemoveAllListeners();

        confirmButton.AddListener(() => {
            Hide();
            confirmAction();
        });
        cancelButton.AddListener(() => {
            Hide();
            cancelAction();
        });
    }
}