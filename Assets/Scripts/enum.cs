
using System;

public enum CharacteristicType { Strength = 0, Dexterity, Constitution, Intelligence, Wisdom, Charisma }

public enum CharacterType { Bard = 0, Cleric, Ranger, Fighter, Wizard, Warlock, Sorcerer, Monk, Druid, Paladin, Barbarian, Rogue }

public enum RaceType { Dragonborn = 0, Dwarf, Elf, Gnome, Half_Elf, Halfling, Half_Orc, Human, Tiefling }

[Flags]
public enum SkillType { 
    None = 0,
    Acrobatics = 1,
    AnimalHandling = 2,
    Arcana = 4,
    Athletics = 8,
    Deception = 16,
    History = 32,
    Insight = 64,
    Intimidation = 128,
    Investigation = 256,
    Medicine = 512,
    Nature = 1024,
    Perception = 2048,
    Performance = 4096,
    Persuasion = 8192,
    Religion = 16384,
    SleightOfHand = 32768,
    Stealth = 65536,
    Survival = 131072
}

public enum DiceType { k4 = 0, k6, k8, k10, k12, k20, k100 }