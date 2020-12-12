using System;

using UnityEngine;
using UnityEngine.UI;

public class DeathSavesMessage : BaseModalMessage
{
    [SerializeField] private Toggle[] successToggles;
    [SerializeField] private Toggle[] failureToggles;
    [SerializeField] private Button successButton;
    [SerializeField] private Button failureButton;
    [SerializeField] private Button resetButton;

    public event Action OnSuccessPressed;
    public event Action OnFailurePressed;
    public event Action OnResetPressed;

    public void Show(int successCount, int failureCount)
    {
        for (int i = 0; i < successToggles.Length; i++)
            successToggles[i].isOn = i < successCount;
        
        for (int i = 0; i < failureToggles.Length; i++)
            failureToggles[i].isOn = i < failureCount;

        successButton.onClick.AddListener(() =>
        {
            OnSuccessPressed?.Invoke();
            Destroy(this.gameObject);
        });
        failureButton.onClick.AddListener(() =>
        {
            OnFailurePressed?.Invoke();
            Destroy(this.gameObject);
        });
        resetButton.onClick.AddListener(() =>
        {
            OnResetPressed?.Invoke();
            Destroy(this.gameObject);
        });
    }
}