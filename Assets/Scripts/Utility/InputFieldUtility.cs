using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class InputFieldUtility
{
    public static char ValidateSignedNumberValue(string text, int charIndex, char addedChar)
    {
        if (charIndex == 0 && (addedChar == '+' || addedChar == '-'))
            return addedChar;

        return ValidateUnsignedNumberValue(text, charIndex, addedChar);
    }

    public static char ValidateUnsignedNumberValue(string text, int charIndex, char addedChar)
    {
        // TODO: add checking of max string length
        if (Char.IsNumber(addedChar))
            return addedChar;

        return '\0';
    }
}