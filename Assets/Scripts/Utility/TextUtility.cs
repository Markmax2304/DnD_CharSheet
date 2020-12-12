using System;
using System.Text;

using UnityEngine;

public static class TextUtility
{
    public static string GetSignedValueString(int value, bool broad = false)
    {
        StringBuilder sb = new StringBuilder();

        if (broad)
            sb.Append(' ');
        sb.Append(value < 0 ? '-' : '+');
        if (broad)
            sb.Append(' ');
        sb.Append(Mathf.Abs(value));

        return sb.ToString();
    }

    public static string GetValueAndMaxString(int value, int max)
    {
        return String.Format("{0}/{1}", value.ToString(), max.ToString());
    }

    public static string GetDiceValueString(int diceCount, DiceType type, int bonus = 0)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(diceCount);
        sb.Append(type.ToString());
        if (bonus != 0)
            sb.Append(GetSignedValueString(bonus, true));

        return sb.ToString();
    }
}