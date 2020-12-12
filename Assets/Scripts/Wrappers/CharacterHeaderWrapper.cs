using System;

using UnityEngine;

public class CharacterHeaderWrapper : CharacterBaseWrapper<CharacterHeaderHolder>
{
    public CharacterHeaderWrapper(CharacterHeaderHolder characterHeader, CharacterSheetController characterSheetController)
        : base(characterHeader, characterSheetController)
    {
        characterHeader.playerNameInput.onValueChanged.AddListener((input) => characterSheetController.Character.PlayerName = input);

        // class part
        characterHolder.characterTypeDropdown.FillOptionsByEnum<CharacterType>(CharacterUtility.GetCharacterTypeName);
        characterHolder.characterTypeDropdown.onValueChanged.AddListener((value) =>
        {
            characterSheetController.Character.Type = (CharacterType)value;
        });

        // race part
        characterHolder.characterRaceDropdown.FillOptionsByEnum<RaceType>(CharacterUtility.GetRaceTypeName);
        characterHolder.characterRaceDropdown.onValueChanged.AddListener((value) =>
        {
            characterSheetController.Character.Race = (RaceType)value;
        });

        // expirience part
        characterHolder.expirienceButton.onClick.AddListener(() =>
        {
            MessageBoxController.Instance.ShowInputMessage("Введите полученое кол-во опыта", "Опыт", HandleExpirienceInput);
        });
        characterHolder.levelButton.onClick.AddListener(() =>
        {
            MessageBoxController.Instance.ShowInputMessage("Введите полученое кол-во опыта", "Опыт", HandleExpirienceInput);
        });
    }

    public override void OnCharacterChanged()
    {
        var sheet = characterSheetController.Character;
        if (sheet == null)
            // TODO: disable all inputs and value holders
            return;

        characterHolder.playerNameInput.text = sheet.PlayerName;

        characterHolder.characterTypeDropdown.value = (int)sheet.Type;
        characterHolder.characterRaceDropdown.value = (int)sheet.Race;
        UpdateExpiriencePoints(sheet.ExpiriencePoints);
    }

    private void UpdateExpiriencePoints(int expPoints)
    {
        int level = CharacterValuesUtility.CalculateLevel(expPoints);
        characterHolder.levelText.text = level.ToString();
        characterHolder.expirienceProgress.value = CharacterValuesUtility.CalculateLevelProgress(expPoints);
        characterHolder.expirienceText.text = String.Format("{0}/{1}", expPoints.ToString(), CharacterValuesUtility.GetPointsByLevel(level + 1).ToString());
    }

    private void HandleExpirienceInput(string input)
    {
        if (!Int32.TryParse(input, out int addExpPoints))
            return;

        int expPoints = characterSheetController.Character.ExpiriencePoints + addExpPoints;
        expPoints = Mathf.Max(expPoints, 0);

        characterSheetController.Character.ExpiriencePoints = expPoints;

        UpdateExpiriencePoints(expPoints);
    }
}
