using System;

using UnityEngine;

public class SpecialFeaturesWrapper : CharacterBaseWrapper<SpecialFeaturesHolder>
{
    public SpecialFeaturesWrapper(SpecialFeaturesHolder characterSkills, CharacterSheetController characterSheetController)
        : base(characterSkills, characterSheetController)
    {
        characterHolder.armorClassValueInput.onValidateInput += InputFieldUtility.ValidateUnsignedNumberValue;
        characterHolder.armorClassValueInput.onValueChanged.AddListener((input) =>
        {
            if (!Int32.TryParse(input, out int result))
                result = 0;

            characterSheetController.Character.ArmorClass = result;
        });

        characterHolder.hitsButton.onClick.AddListener(() =>
        {
            MessageBoxController.Instance.ShowHitMessage(characterSheetController.Character, HandleHitsInputs);
        });

        characterHolder.hitDiceButton.onClick.AddListener(() =>
        {
            MessageBoxController.Instance.ShowHitDiceMessage(characterSheetController.Character, UseHitDice, ResetHitDices, SetHitDices);
        });

        characterHolder.deathSavesButton.onClick.AddListener(() =>
        {
            var sheet = characterSheetController.Character;
            MessageBoxController.Instance.ShowDeathSavesMessage(sheet.DeathSuccessCount, sheet.DeathFailureCount, AddSuccessOnDeath, AddFailureOnDeath, ResetDeathSaves);
        });
    }

    public override void OnCharacterChanged()
    {
        var sheet = characterSheetController.Character;
        if (sheet == null)
            // TODO: disable all inputs and value holders
            return;

        // mastery bonus
        SetMasteryBonus(sheet.ExpiriencePoints);
        sheet.OnExpiriencePointsChanged += SetMasteryBonus;

        // passive perception and initiative
        SetPassivePerception();
        SetInitiative();

        sheet.OnExpiriencePointsChanged += (expPoints) => SetPassivePerception();
        sheet.OnPersonalSkillsChanged += (skills) => SetPassivePerception();
        sheet.OnCharacteristicChanged += (characteristic, value) =>
        {
            if (characteristic == CharacteristicType.Wisdom)
                SetPassivePerception();
            if (characteristic == CharacteristicType.Dexterity)
                SetInitiative();
        };

        // speed
        SetSpeed();
        sheet.OnCharacterRaceChanged += (race) => SetSpeed();

        // armor
        characterHolder.armorClassValueInput.text = sheet.ArmorClass.ToString();

        // hits
        SetHits();
        sheet.OnExpiriencePointsChanged += (expPoints) => SetHits();
        sheet.OnCharacteristicChanged += (characteristic, value) =>
        {
            if (characteristic == CharacteristicType.Constitution)
                SetHits();
        };

        // hit dices
        SetHitDices();
        sheet.OnExpiriencePointsChanged += (expPoints) => SetHitDices();
        sheet.OnCharacterClassChanged += (type) => SetHitDices();

        // death saves
        SetDeathSaves();
    }

    private void SetMasteryBonus(int expPoints)
    {
        int level = CharacterValuesUtility.CalculateLevel(expPoints);
        characterHolder.masteryBonusValueText.text = TextUtility.GetSignedValueString(CharacterValuesUtility.GetMasteryBonus(level));
    }

    private void SetPassivePerception()
    {
        var sheet = characterSheetController.Character;
        int result = 10 + CharacterValuesUtility.GetCharacteristicModificator(sheet[CharacteristicType.Wisdom]);

        if ((sheet.PersonalSkills & SkillType.Perception) == SkillType.Perception)
        {
            int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
            result += CharacterValuesUtility.GetMasteryBonus(level);
        }

        characterHolder.passivePerceptionValueText.text = result.ToString();
    }

    private void SetInitiative()
    {
        var sheet = characterSheetController.Character;
        int initiativeModificator = CharacterValuesUtility.GetCharacteristicModificator(sheet[CharacteristicType.Dexterity]);
        characterHolder.initiativeValueText.text = TextUtility.GetSignedValueString(initiativeModificator);
    }

    private void SetSpeed()
    {
        var sheet = characterSheetController.Character;
        characterHolder.speedValueText.text = CharacterUtility.GetSpeedByRace(sheet.Race).ToString();
    }

    private void HandleHitsInputs(string currentHitsText, string maxHitsText)
    {
        var sheet = characterSheetController.Character;

        if (!Int32.TryParse(currentHitsText, out int currentHits))
            currentHits = sheet.CurrentHits;
        if (!Int32.TryParse(maxHitsText, out int maxHits))
            maxHits = sheet.MaxHits;

        int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
        int constitutionModificator = CharacterValuesUtility.GetCharacteristicModificator(sheet[CharacteristicType.Constitution]);
        int wholeMaxHits = maxHits + level * constitutionModificator;

        currentHits = Mathf.Min(currentHits, wholeMaxHits);
        sheet.CurrentHits = currentHits;
        sheet.MaxHits = maxHits;

        // update new value on UI
        characterHolder.hitsValueText.text = TextUtility.GetValueAndMaxString(currentHits, wholeMaxHits);
    }

    private void SetHits()
    {
        var sheet = characterSheetController.Character;

        int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
        int constitutionModificator = CharacterValuesUtility.GetCharacteristicModificator(sheet[CharacteristicType.Constitution]);
        int wholeMaxHits = sheet.MaxHits + level * constitutionModificator;
        characterHolder.hitsValueText.text = TextUtility.GetValueAndMaxString(sheet.CurrentHits, wholeMaxHits);
    }

    private void SetHitDices()
    {
        var sheet = characterSheetController.Character;

        characterHolder.diceTypeText.text = TextUtility.GetDiceValueString(1, CharacterUtility.GetDiceTypeByClass(sheet.Type));

        int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
        if (sheet.HitDiceCount > level)
            sheet.HitDiceCount = level;
        characterHolder.dicesValueText.text = TextUtility.GetValueAndMaxString(sheet.HitDiceCount, level);
    }

    private void UseHitDice()
    {
        var sheet = characterSheetController.Character;

        if (sheet.HitDiceCount > 0)
            sheet.HitDiceCount -= 1;
    }

    private void ResetHitDices()
    {
        var sheet = characterSheetController.Character;
        int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);

        int restoredDiceCount = Mathf.CeilToInt(level / 2f);
        int diceCount = sheet.HitDiceCount + restoredDiceCount;
        sheet.HitDiceCount = Mathf.Min(level, diceCount);
    }

    private void SetDeathSaves()
    {
        var sheet = characterSheetController.Character;

        for(int i = 0; i < characterHolder.successToggles.Length; i++)
            characterHolder.successToggles[i].isOn = i < sheet.DeathSuccessCount;
        
        for (int i = 0; i < characterHolder.failureToggles.Length; i++)
            characterHolder.failureToggles[i].isOn = i < sheet.DeathFailureCount;
    }

    private void AddSuccessOnDeath()
    {
        var sheet = characterSheetController.Character;
        sheet.DeathSuccessCount = Mathf.Min(sheet.DeathSuccessCount + 1, 3);

        SetDeathSaves();
    }

    private void AddFailureOnDeath()
    {
        var sheet = characterSheetController.Character;
        sheet.DeathFailureCount = Mathf.Min(sheet.DeathFailureCount + 1, 3);

        SetDeathSaves();
    }

    private void ResetDeathSaves()
    {
        var sheet = characterSheetController.Character;
        sheet.DeathSuccessCount = 0;
        sheet.DeathFailureCount = 0;

        SetDeathSaves();
    }
}