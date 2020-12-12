using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class CharacterSavingThrowsHolder : MonoBehaviour
{
    public SavingThrowHolder[] savingThrows;

    public SavingThrowHolder GetSavingThrow(CharacteristicType characteristic)
    {
        foreach(var item in savingThrows)
        {
            if (item.characteristic == characteristic)
                return item;
        }

        throw new Exception($"There isn't saving throw for {characteristic} characteristic");
    }
}

[Serializable]
public class SavingThrowHolder
{
    public CharacteristicType characteristic;
    public Toggle toggle;
    public TMP_Text valueText;
}
