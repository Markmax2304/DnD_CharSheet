using System;
using System.Collections.Generic;

[Serializable]
public class CharacterSheet
{
    public Guid Id { get; }

    public string PlayerName
    {
        get { return playerName; }
        set
        {
            if(playerName != value)
            {
                playerName = value;
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public int this[CharacteristicType type]
    {
        get
        {
            return characteristics[type];
        }
        set
        {
            characteristics[type] = value;
        }
    }

    public CharacterSheet(Guid id)
    {
        Id = id;

        playerName = "Никто";

        characteristics = new Dictionary<CharacteristicType, int>();
        foreach(CharacteristicType type in Enum.GetValues(typeof(CharacteristicType)))
        {
            characteristics.Add(type, 0);
        }
    }

    private string playerName;
    private Dictionary<CharacteristicType, int> characteristics;
}


