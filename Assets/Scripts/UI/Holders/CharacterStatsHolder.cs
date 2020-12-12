using UnityEngine;

using TMPro;

public class CharacterStatsHolder : MonoBehaviour
{
    public TMP_InputField strengthInput;
    public TMP_Text strengthModificatorText;
    public TMP_InputField dexterityInput;
    public TMP_Text dexterityModificatorText;
    public TMP_InputField constitutionInput;
    public TMP_Text constitutionModificatorText;
    public TMP_InputField intelligenceInput;
    public TMP_Text intelligenceModificatorText;
    public TMP_InputField wisdomInput;
    public TMP_Text wisdomModificatorText;
    public TMP_InputField charismaInput;
    public TMP_Text charismaModificatorText;

    public TMP_InputField GetCharacteristicInput(CharacteristicType type)
    {
        switch (type)
        {
            case CharacteristicType.Strength:
                return strengthInput;
            case CharacteristicType.Dexterity:
                return dexterityInput;
            case CharacteristicType.Constitution:
                return constitutionInput;
            case CharacteristicType.Intelligence:
                return intelligenceInput;
            case CharacteristicType.Wisdom:
                return wisdomInput;
            case CharacteristicType.Charisma:
                return charismaInput;
            default:
                throw new System.Exception("Undefined charactiristic type " + type);
        }
    }

    public TMP_Text GetCharacteristicModificatorText(CharacteristicType type)
    {
        switch (type)
        {
            case CharacteristicType.Strength:
                return strengthModificatorText;
            case CharacteristicType.Dexterity:
                return dexterityModificatorText;
            case CharacteristicType.Constitution:
                return constitutionModificatorText;
            case CharacteristicType.Intelligence:
                return intelligenceModificatorText;
            case CharacteristicType.Wisdom:
                return wisdomModificatorText;
            case CharacteristicType.Charisma:
                return charismaModificatorText;
            default:
                throw new System.Exception("Undefined charactiristic type " + type);
        }
    }
}
