using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class YesNoMessage : BaseModalMessage
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    public event Action OnYesPressed;
    public event Action OnNoPressed;

    public void Show(string message)
    {
        // TODO: add possibility to cancel message box if pressed outside of it

        messageText.text = message;

        yesButton.onClick.AddListener(() =>
        {
            OnYesPressed?.Invoke();
            Destroy(this.gameObject);
        });
        noButton.onClick.AddListener(() =>
        {
            OnNoPressed?.Invoke();
            Destroy(this.gameObject);
        });
    }
}
