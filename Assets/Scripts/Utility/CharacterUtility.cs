using System;
using System.Collections.Generic;

public static class CharacterUtility
{
    private readonly static Dictionary<CharacterType, CharacterTypeData> characterTypes;
    private readonly static Dictionary<RaceType, string> raceTypes;
    private readonly static Dictionary<CharacteristicType, SkillType> skillsByCharacteristics;
    private readonly static Dictionary<RaceType, int> speedByRaces;

    static CharacterUtility()
    {
        // TODO: redesign it to scriptable object or serializable values to edit from inspector
        characterTypes = new Dictionary<CharacterType, CharacterTypeData>()
        {
            {CharacterType.Bard, new CharacterTypeData(){ Name = "Бард", DiceType = DiceType.k8,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Dexterity, CharacteristicType.Charisma } } },
            {CharacterType.Cleric, new CharacterTypeData(){ Name = "Жрец", DiceType = DiceType.k8,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Wisdom, CharacteristicType.Charisma } } },
            {CharacterType.Ranger, new CharacterTypeData(){ Name = "Следопыт", DiceType = DiceType.k10,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Strength, CharacteristicType.Dexterity } } },
            {CharacterType.Fighter, new CharacterTypeData(){ Name = "Воин", DiceType = DiceType.k10,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Strength, CharacteristicType.Constitution } } },
            {CharacterType.Wizard, new CharacterTypeData(){ Name = "Волшебник", DiceType = DiceType.k6,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Intelligence, CharacteristicType.Wisdom } } },
            {CharacterType.Warlock, new CharacterTypeData(){ Name = "Колдун", DiceType = DiceType.k8,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Wisdom, CharacteristicType.Charisma } } },
            {CharacterType.Sorcerer, new CharacterTypeData(){ Name = "Чародей", DiceType = DiceType.k6,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Constitution, CharacteristicType.Charisma } } },
            {CharacterType.Monk, new CharacterTypeData(){ Name = "Монах", DiceType = DiceType.k8,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Strength, CharacteristicType.Dexterity } } },
            {CharacterType.Druid, new CharacterTypeData(){ Name = "Друид", DiceType = DiceType.k8,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Intelligence, CharacteristicType.Wisdom } } },
            {CharacterType.Paladin, new CharacterTypeData(){ Name = "Паладин", DiceType = DiceType.k10,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Wisdom, CharacteristicType.Charisma } } },
            {CharacterType.Barbarian, new CharacterTypeData(){ Name = "Варвар", DiceType = DiceType.k12,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Strength, CharacteristicType.Constitution } } },
            {CharacterType.Rogue, new CharacterTypeData(){ Name = "Плут", DiceType = DiceType.k8,
                SavingThrows = new List<CharacteristicType>(){ CharacteristicType.Dexterity, CharacteristicType.Intelligence } } },
        };

        raceTypes = new Dictionary<RaceType, string>()
        {
            {RaceType.Dragonborn, "Драконорождённый" },
            {RaceType.Dwarf, "Дварф" },
            {RaceType.Elf, "Эльф" },
            {RaceType.Gnome, "Гном" },
            {RaceType.Half_Elf, "Полуэльф" },
            {RaceType.Halfling, "Полурослик" },
            {RaceType.Half_Orc, "Полуорк" },
            {RaceType.Human, "Человек" },
            {RaceType.Tiefling, "Тифлинг" }
        };

        skillsByCharacteristics = new Dictionary<CharacteristicType, SkillType>()
        {
            {CharacteristicType.Strength, SkillType.Athletics },
            {CharacteristicType.Dexterity, SkillType.Acrobatics | SkillType.SleightOfHand | SkillType.Stealth },
            {CharacteristicType.Constitution, SkillType.None },
            {CharacteristicType.Intelligence, SkillType.Arcana | SkillType.History | SkillType.Investigation | SkillType.Nature | SkillType.Religion },
            {CharacteristicType.Wisdom, SkillType.AnimalHandling | SkillType.Insight | SkillType.Medicine | SkillType.Perception | SkillType.Survival },
            {CharacteristicType.Charisma, SkillType.Deception | SkillType.Intimidation | SkillType.Performance | SkillType.Persuasion }
        };

        speedByRaces = new Dictionary<RaceType, int>()
        {
            {RaceType.Dragonborn, 30 },
            {RaceType.Dwarf, 25 },
            {RaceType.Elf, 30 },
            {RaceType.Gnome, 25 },
            {RaceType.Half_Elf, 30 },
            {RaceType.Halfling, 25 },
            {RaceType.Half_Orc, 30 },
            {RaceType.Human, 30 },
            {RaceType.Tiefling, 30 }
        };
    }

    public static string GetCharacterTypeName(CharacterType type)
    {
        return characterTypes[type].Name;
    }

    public static string GetCharacterTypeName(int typeIndex)
    {
        return GetCharacterTypeName((CharacterType)typeIndex);
    }

    public static string GetRaceTypeName(RaceType type)
    {
        return raceTypes[type];
    }

    public static string GetRaceTypeName(int raceIndex)
    {
        return GetRaceTypeName((RaceType)raceIndex);
    }

    public static CharacteristicType GetCharacteristicBySkill(SkillType skill)
    {
        foreach(var item in skillsByCharacteristics)
        {
            if ((item.Value & skill) == skill)
                return item.Key;
        }

        throw new Exception($"There isn't Skill type like {skill}");
    }

    public static SkillType GetSkillsByCharacteristic(CharacteristicType characteristic)
    {
        return skillsByCharacteristics[characteristic];
    }

    // TODO: redesign it when character class support will be implemented
    public static List<CharacteristicType> GetSavingThrowsByClass(CharacterType type)
    {
        return characterTypes[type].SavingThrows;
    }

    public static int GetSpeedByRace(RaceType type)
    {
        return speedByRaces[type];
    }

    public static DiceType GetDiceTypeByClass(CharacterType type)
    {
        return characterTypes[type].DiceType;
    }
}

public class CharacterTypeData
{
    public string Name { get; set; }
    public List<CharacteristicType> SavingThrows { get; set; }
    public DiceType DiceType { get; set; }
}