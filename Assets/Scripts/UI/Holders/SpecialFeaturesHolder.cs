using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using CommonMaxTools.Attributes;

public class SpecialFeaturesHolder : MonoBehaviour
{
    public TMP_Text masteryBonusValueText;
    public TMP_Text passivePerceptionValueText;
    public TMP_Text initiativeValueText;
    public TMP_Text speedValueText;
    public TMP_InputField armorClassValueInput;

    [Separator]
    public Button hitsButton;
    public TMP_Text hitsValueText;

    [Separator]
    public Button hitDiceButton;
    public TMP_Text diceTypeText;
    public TMP_Text dicesValueText;

    [Separator]
    public Button deathSavesButton;
    public Toggle[] successToggles;
    public Toggle[] failureToggles;
}
