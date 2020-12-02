using System;

public class CharacterStatsWrapper
{
    private readonly CharacterStatsHolder characterStats;
    private readonly CharacterSheetController characterSheetController;

    public CharacterStatsWrapper(CharacterStatsHolder characterStats, CharacterSheetController characterSheetController)
    {
        this.characterStats = characterStats;
        this.characterSheetController = characterSheetController;

        characterStats.playerNameInput.onValueChanged.AddListener((input) => characterSheetController.CharacterSheet.PlayerName = input);

        foreach(CharacteristicType type in Enum.GetValues(typeof(CharacteristicType)))
        {
            characterStats.GetCharacteristicInput(type).onValueChanged.AddListener((text) =>
            {
                characterSheetController.CharacterSheet[type] = Convert.ToInt32(text);
            });
        }
    }

    public void OnCharacterChanged()
    {
        var sheet = characterSheetController.CharacterSheet;
        if (sheet == null)
            // TODO: disable all inputs and value holders
            return;

        characterStats.playerNameInput.text = sheet.PlayerName;

        foreach (CharacteristicType type in Enum.GetValues(typeof(CharacteristicType)))
        {
            characterStats.GetCharacteristicInput(type).text = sheet[type].ToString();
        }
    }
}
