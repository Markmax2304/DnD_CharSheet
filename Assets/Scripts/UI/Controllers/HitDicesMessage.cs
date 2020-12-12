using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class HitDicesMessage : BaseModalMessage
{
    [SerializeField] private TMP_Text diceTypeText;
    [SerializeField] private TMP_Text diceCountValueText;
    [SerializeField] private Button okButton;
    [SerializeField] private Button useDiceButton;
    [SerializeField] private Button resetDicesButton;

    public event Action OnOkPressed;
    public event Action OnUseDicePressed;
    public event Action OnResetDicesPressed;

    public void Show(CharacterSheet sheet)
    {
        diceTypeText.text = TextUtility.GetDiceValueString(1, CharacterUtility.GetDiceTypeByClass(sheet.Type));
        SetHitDicesCount(sheet);

        okButton.onClick.AddListener(() =>
        {
            OnOkPressed?.Invoke();
            Destroy(this.gameObject);
        });
        useDiceButton.onClick.AddListener(() =>
        {
            OnUseDicePressed?.Invoke();
            SetHitDicesCount(sheet);
        });
        resetDicesButton.onClick.AddListener(() =>
        {
            OnResetDicesPressed?.Invoke();
            SetHitDicesCount(sheet);
        });
    }

    private void SetHitDicesCount(CharacterSheet sheet)
    {
        int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
        diceCountValueText.text = TextUtility.GetValueAndMaxString(sheet.HitDiceCount, level);
    }
}
