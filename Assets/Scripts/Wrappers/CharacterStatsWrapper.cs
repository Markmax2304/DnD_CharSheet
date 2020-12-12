using System;

using UnityEngine;

public class CharacterStatsWrapper : CharacterBaseWrapper<CharacterStatsHolder>
{
    public CharacterStatsWrapper(CharacterStatsHolder characterStats, CharacterSheetController characterSheetController)
        : base(characterStats, characterSheetController)
    {
        foreach(CharacteristicType type in Enum.GetValues(typeof(CharacteristicType)))
        {
            var inputField = characterStats.GetCharacteristicInput(type);
            inputField.onValueChanged.AddListener((text) =>
            {
                if (!Int32.TryParse(text, out int statValue))
                    return;

                if (statValue < 1 || statValue > 30)
                {
                    statValue = Mathf.Clamp(statValue, 1, 30);
                    inputField.text = statValue.ToString();
                }
                characterSheetController.Character[type] = statValue;
            });
            inputField.onValidateInput += InputFieldUtility.ValidateUnsignedNumberValue;
        }
    }

    public override void OnCharacterChanged()
    {
        var sheet = characterSheetController.Character;
        if (sheet == null)
            // TODO: disable all inputs and value holders
            return;

        foreach (CharacteristicType type in Enum.GetValues(typeof(CharacteristicType)))
        {
            SetCharacteristic(type, sheet[type]);
        }

        characterSheetController.Character.OnCharacteristicChanged += SetCharacteristic;
    }

    private void SetCharacteristic(CharacteristicType type, int statValue)
    {
        characterHolder.GetCharacteristicInput(type).text = statValue.ToString();
        int modificator = CharacterValuesUtility.GetCharacteristicModificator(statValue);
        characterHolder.GetCharacteristicModificatorText(type).text = TextUtility.GetSignedValueString(modificator);
    }
}
