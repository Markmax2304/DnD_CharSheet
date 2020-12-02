using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

[Serializable]
public static class CharacterSheetStorage
{
    private const string folderName = "DnD_characters";

    [SerializeField]
    public readonly static List<CharacterSheet> characters = new List<CharacterSheet>();

    public static event Action OnCharacterStorageChanged;

    private static string folderPath;

    static CharacterSheetStorage()
    {
        string[] characterFiles = Directory.GetFiles(GetCharacterFolderPath());
        for(int i = 0; i < characterFiles.Length; i++)
        {
            var character = LoadCharacter(characterFiles[i]);
            if (character != null)
                characters.Add(character);
        }
    }

    public static CharacterSheet CreateNewCharacter()
    {
        var character = new CharacterSheet(Guid.NewGuid());

        characters.Add(character);
        SaveCharacter(character);

        OnCharacterStorageChanged?.Invoke();

        return character;
    }

    public static void RemoveCharacter(Guid id)
    {
        var character = characters.Find(s => s.Id == id);
        characters.Remove(character);

        string filePath = GetCharacterPath(id);
        if (File.Exists(filePath))
            File.Delete(filePath);

        OnCharacterStorageChanged?.Invoke();
    }

    public static void SaveCharacter(Guid id)
    {
        var character = characters.Find(s => s.Id == id);
        SaveCharacter(character);
    }

    private static void SaveCharacter(CharacterSheet sheet)
    {
        string path = GetCharacterPath(sheet.Id);

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            formatter.Serialize(stream, sheet);
        }
    }

    private static CharacterSheet LoadCharacter(string path)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return formatter.Deserialize(stream) as CharacterSheet;
            }
        }
        catch
        {
            return null;
        }
    }

    private static string GetCharacterPath(Guid sheetId)
    {
        const string pattern = "{0}/{1}.data";
        return String.Format(pattern, GetCharacterFolderPath(), sheetId.ToString());
    }

    private static string GetCharacterFolderPath()
    {
        const string pattern = "{0}/{1}";

        if (folderPath == null)
            folderPath = String.Format(pattern, Application.persistentDataPath, folderName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        return folderPath;
    }
}
