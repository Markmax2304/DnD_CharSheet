using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using static TMPro.TMP_InputField;

public class InputMessage : BaseModalMessage
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text inputPlaceholder;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    public event Action<string> OnOkPressed;
    public event Action<string> OnCancelPressed;

    public void Show(string message, string placeholderText, OnValidateInput onValidate)
    {
        messageText.text = message;
        inputPlaceholder.text = placeholderText;

        inputField.onValidateInput += onValidate;

        okButton.onClick.AddListener(() =>
        {
            OnOkPressed?.Invoke(inputField.text);
            Destroy(this.gameObject);
        });
        cancelButton.onClick.AddListener(() =>
        {
            OnCancelPressed?.Invoke(inputField.text);
            Destroy(this.gameObject);
        });
    }
}
