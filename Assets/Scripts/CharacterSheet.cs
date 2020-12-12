using System;
using System.Collections.Generic;

[Serializable]
public class CharacterSheet : IDisposable
{
    [field: NonSerialized]
    public event Action<CharacteristicType, int> OnCharacteristicChanged;
    [field: NonSerialized]
    public event Action<int> OnExpiriencePointsChanged;
    [field: NonSerialized]
    public event Action<SkillType> OnPersonalSkillsChanged;
    [field: NonSerialized]
    public event Action<CharacterType> OnCharacterClassChanged;
    [field: NonSerialized]
    public event Action<RaceType> OnCharacterRaceChanged;

    public Guid Id { get; }
    public DateTime CreatedDate { get; }

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
        get { return characteristics[type]; }
        set
        {
            characteristics[type] = value;
            OnCharacteristicChanged?.Invoke(type, value);
            CharacterSheetStorage.SaveCharacter(Id);
        }
    }

    public CharacterType Type
    {
        get { return type; }
        set
        {
            if(type != value)
            {
                type = value;
                OnCharacterClassChanged?.Invoke(value);
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public RaceType Race
    {
        get { return race; }
        set
        {
            if(race != value)
            {
                race = value;
                OnCharacterRaceChanged?.Invoke(value);
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public int ExpiriencePoints
    {
        get { return expiriencePoints; }
        set
        {
            if(expiriencePoints != value)
            {
                expiriencePoints = value;
                OnExpiriencePointsChanged?.Invoke(value);
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public SkillType PersonalSkills
    {
        get { return personalSkills; }
        set
        {
            if(personalSkills != value)
            {
                personalSkills = value;
                OnPersonalSkillsChanged?.Invoke(value);
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    // TODO: remove it after weapon will be implemented
    public int ArmorClass
    {
        get { return armorClass; }
        set
        {
            if(armorClass != value)
            {
                armorClass = value;
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public int CurrentHits
    {
        get { return currentHits; }
        set
        {
            if(currentHits != value)
            {
                currentHits = value;
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public int MaxHits
    {
        get { return maxHits; }
        set
        {
            if (maxHits != value)
            {
                maxHits = value;
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public int HitDiceCount
    {
        get { return hitDiceCount; }
        set
        {
            if (hitDiceCount != value)
            {
                hitDiceCount = value;
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public int DeathSuccessCount
    {
        get { return deathSuccessCount; }
        set
        {
            if (deathSuccessCount != value)
            {
                deathSuccessCount = value;
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public int DeathFailureCount
    {
        get { return deathFailureCount; }
        set
        {
            if (deathFailureCount != value)
            {
                deathFailureCount = value;
                CharacterSheetStorage.SaveCharacter(Id);
            }
        }
    }

    public CharacterSheet(Guid id)
    {
        Id = id;
        CreatedDate = DateTime.Now;

        playerName = defaultName;

        characteristics = new Dictionary<CharacteristicType, int>();
        foreach(CharacteristicType type in Enum.GetValues(typeof(CharacteristicType)))
        {
            characteristics.Add(type, 0);
        }

        type = CharacterType.Fighter;
        race = RaceType.Human;
        expiriencePoints = 0;
        personalSkills = SkillType.None;
        armorClass = 0;
        currentHits = 0;
        maxHits = 0;
        hitDiceCount = CharacterValuesUtility.CalculateLevel(expiriencePoints);
        deathSuccessCount = 0;
        deathFailureCount = 0;
    }

    public void Dispose()
    {
        OnCharacteristicChanged = null;
        OnExpiriencePointsChanged = null;
        OnPersonalSkillsChanged = null;
        OnCharacterClassChanged = null;
        OnCharacterRaceChanged = null;
    }

    private const string defaultName = "Никто";

    private string playerName;
    private Dictionary<CharacteristicType, int> characteristics;
    private CharacterType type;
    private RaceType race;
    private int expiriencePoints;
    private SkillType personalSkills;
    private int armorClass;
    private int currentHits;
    private int maxHits;
    private int hitDiceCount;
    private int deathSuccessCount;
    private int deathFailureCount;
}


