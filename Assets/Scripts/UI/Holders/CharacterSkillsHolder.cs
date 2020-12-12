using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class CharacterSkillsHolder : MonoBehaviour
{
    public SkillHolder[] skills;
}

[Serializable]
public class SkillHolder
{
    public SkillType type;
    public Toggle toggleButton;
    public TMP_Text valueText;
}
