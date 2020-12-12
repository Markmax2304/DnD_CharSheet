
public class CharacterSavingThrowsWrapper : CharacterBaseWrapper<CharacterSavingThrowsHolder>
{
    public CharacterSavingThrowsWrapper(CharacterSavingThrowsHolder characterHeader, CharacterSheetController characterSheetController)
        : base(characterHeader, characterSheetController)
    {
    }

    public override void OnCharacterChanged()
    {
        var sheet = characterSheetController.Character;
        if (sheet == null)
            // TODO: disable all inputs and value holders
            return;

        SetAllSavingThrowModificators();

        sheet.OnCharacteristicChanged += (characteristic, value) => SetSavingThrowModificator(characteristic);
        sheet.OnCharacterClassChanged += (characterClass) => SetAllSavingThrowModificators();
        sheet.OnExpiriencePointsChanged += (points) => SetAllSavingThrowModificators();
    }

    private void SetAllSavingThrowModificators()
    {
        foreach (var savingThrow in characterHolder.savingThrows)
            SetSavingThrowModificator(savingThrow);
    }

    private void SetSavingThrowModificator(SavingThrowHolder savingThrow)
    {
        var sheet = characterSheetController.Character;

        int characteristicValue = sheet[savingThrow.characteristic];
        int modificator = CharacterValuesUtility.GetCharacteristicModificator(characteristicValue);

        var baseSavingThrows = CharacterUtility.GetSavingThrowsByClass(sheet.Type);
        bool isOwned = baseSavingThrows.Contains(savingThrow.characteristic);

        if (isOwned)
        {
            int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
            modificator += CharacterValuesUtility.GetMasteryBonus(level);
        }

        savingThrow.valueText.text = TextUtility.GetSignedValueString(modificator);
        savingThrow.toggle.isOn = isOwned;
    }

    private void SetSavingThrowModificator(CharacteristicType characteristic)
    {
        var savingThrow = characterHolder.GetSavingThrow(characteristic);
        SetSavingThrowModificator(savingThrow);
    }
}
