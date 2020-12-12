
public class CharacterSkillsWrapper : CharacterBaseWrapper<CharacterSkillsHolder>
{
    public CharacterSkillsWrapper(CharacterSkillsHolder characterSkills, CharacterSheetController characterSheetController) 
        : base(characterSkills, characterSheetController)
    {
        foreach (var skillHolder in characterHolder.skills)
        {
            skillHolder.toggleButton.onValueChanged.AddListener((isOn) =>
            {
                var personalSkills = characterSheetController.Character.PersonalSkills;

                if (isOn)
                    personalSkills |= skillHolder.type;
                else
                    personalSkills &= (~skillHolder.type);

                characterSheetController.Character.PersonalSkills = personalSkills;
            });
        }
    }

    public override void OnCharacterChanged()
    {
        var sheet = characterSheetController.Character;
        if (sheet == null)
            // TODO: disable all inputs and value holders
            return;

        SetAllSkillModificators();

        sheet.OnCharacteristicChanged += SetSkillModificators;
        sheet.OnPersonalSkillsChanged += (skills) => SetAllSkillModificators();
        sheet.OnExpiriencePointsChanged += (points) => SetAllSkillModificators();
    }

    private void SetAllSkillModificators()
    {
        foreach (var skillHolder in characterHolder.skills)
            SetSkillModificator(skillHolder);
    }

    private void SetSkillModificator(SkillHolder skillHolder)
    {
        var sheet = characterSheetController.Character;

        var skillCharacteristic = CharacterUtility.GetCharacteristicBySkill(skillHolder.type);
        int characteristicValue = sheet[skillCharacteristic];
        bool isOwned = (sheet.PersonalSkills & skillHolder.type) == skillHolder.type;
        int modificator = CharacterValuesUtility.GetCharacteristicModificator(characteristicValue);

        if (isOwned)
        {
            int level = CharacterValuesUtility.CalculateLevel(sheet.ExpiriencePoints);
            modificator += CharacterValuesUtility.GetMasteryBonus(level);
        }

        skillHolder.valueText.text = TextUtility.GetSignedValueString(modificator);
        skillHolder.toggleButton.isOn = isOwned;
    }

    private void SetSkillModificators(CharacteristicType characteristic, int value)
    {
        SkillType skills = CharacterUtility.GetSkillsByCharacteristic(characteristic);

        foreach (var skillHolder in characterHolder.skills)
        {
            if((skills & skillHolder.type) == skillHolder.type)
                SetSkillModificator(skillHolder);
        }
    }
}