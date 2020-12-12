using UnityEngine;

public static class CharacterValuesUtility
{
    private static int[] expirienceLevels;

    static CharacterValuesUtility()
    {
        expirienceLevels = new int[20] 
        { 
            0, 300, 900, 2700, 6500, 14000, 23000, 34000, 48000, 64000, 85000,
            100000, 120000, 140000, 165000, 195000, 225000, 265000, 305000, 355000 
        };
    }

    public static int CalculateLevel(int points)
    {
        if (points < 0)
            throw new System.Exception("Can't calculate level for negative amount of expirience points");

        for(int i = expirienceLevels.Length - 1; i >= 0; i--)
        {
            if (points >= expirienceLevels[i])
                return i + 1;
        }

        return 0;
    }

    public static int GetPointsByLevel(int level)
    {
        level = Mathf.Clamp(level, 0, 20);
        return expirienceLevels[level - 1];
    }

    public static float CalculateLevelProgress(int points)
    {
        int currentLevel = CalculateLevel(points);
        int nextLevel = currentLevel + 1;

        int pointsOnLevel = points - GetPointsByLevel(currentLevel);
        return (float)pointsOnLevel / (GetPointsByLevel(nextLevel) - GetPointsByLevel(currentLevel));
    }

    public static int GetCharacteristicModificator(int statValue)
    {
        return Mathf.FloorToInt((statValue - 10) / 2f);
    }

    public static int GetMasteryBonus(int level)
    {
        return Mathf.FloorToInt((level - 1) / 4f) + 2;
    }
}
