using UnityEngine;
using TMPro;

public class CharacterStatsHolder : MonoBehaviour
{
    // TODO: replace it
    public TMP_InputField playerNameInput;

    public TMP_InputField strengthInput;
    public TMP_InputField dexterityInput;
    public TMP_InputField constitutionInput;
    public TMP_InputField intelligenceInput;
    public TMP_InputField wisdomInput;
    public TMP_InputField charismaInput;

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
}
