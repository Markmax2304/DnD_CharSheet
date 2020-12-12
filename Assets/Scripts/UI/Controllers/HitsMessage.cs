using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using static TMPro.TMP_InputField;

public class HitsMessage : BaseModalMessage
{
    [SerializeField] private TMP_InputField currentHitsInputField;
    [SerializeField] private TMP_InputField maxHitsInputField;
    [SerializeField] private TMP_Text bonusHitsText;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    public event Action<string, string> OnOkPressed;
    public event Action<string, string> OnCancelPressed;

    public void Show(CharacterSheet sheet, OnValidateInput onValidate)
    {
        currentHitsInputField.text = sheet.CurrentHits.ToString();
        maxHitsInputField.text = sheet.MaxHits.ToString();

        int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
        int constitutionModificator = CharacterValuesUtility.GetCharacteristicModificator(sheet[CharacteristicType.Constitution]);
        bonusHitsText.text = TextUtility.GetSignedValueString(level * constitutionModificator);

        okButton.onClick.AddListener(() =>
        {
            OnOkPressed?.Invoke(currentHitsInputField.text, maxHitsInputField.text);
            Destroy(this.gameObject);
        });
        cancelButton.onClick.AddListener(() =>
        {
            OnCancelPressed?.Invoke(currentHitsInputField.text, maxHitsInputField.text);
            Destroy(this.gameObject);
        });
    }
}
