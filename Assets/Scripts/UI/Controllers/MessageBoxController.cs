using System;

using CommonMaxTools.Tools;

public class MessageBoxController : SingletonBase<MessageBoxController>
{
    public YesNoMessage yesNoMessagePrefab;
    public InputMessage inputMessagePrefab;
    public HitsMessage hitMessagePrefab;
    public HitDicesMessage hitDiceMessagePrefab;
    public DeathSavesMessage deathSavesMessagePrefab;

    public void ShowYesNoMessage(string message, Action onYesAction, Action onNoAction = null)
    {
        YesNoMessage messageBox = Instantiate(yesNoMessagePrefab, transform);
        messageBox.OnYesPressed += onYesAction;
        messageBox.OnNoPressed += onNoAction;
        messageBox.Show(message);

        // TODO: maybe should force placing it in front of all
    }

    public void ShowInputMessage(string message, string placeholderText, Action<string> onSuccessAction, Action<string> onCancelAction = null)
    {
        InputMessage messageBox = Instantiate(inputMessagePrefab, transform);
        messageBox.OnOkPressed += onSuccessAction;
        messageBox.OnCancelPressed += onCancelAction;
        messageBox.Show(message, placeholderText, InputFieldUtility.ValidateSignedNumberValue);
    }

    public void ShowHitMessage(CharacterSheet sheet, Action<string, string> onSuccessAction, Action<string, string> onCancelAction = null)
    {
        HitsMessage messageBox = Instantiate(hitMessagePrefab, transform);
        messageBox.OnOkPressed += onSuccessAction;
        messageBox.OnCancelPressed += onCancelAction;
        messageBox.Show(sheet, InputFieldUtility.ValidateUnsignedNumberValue);
    }

    public void ShowHitDiceMessage(CharacterSheet sheet, Action onUseDiceAction, Action onResetDicesAction, Action onSuccessAction = null)
    {
        HitDicesMessage messageBox = Instantiate(hitDiceMessagePrefab, transform);
        messageBox.OnOkPressed += onSuccessAction;
        messageBox.OnUseDicePressed += onUseDiceAction;
        messageBox.OnResetDicesPressed += onResetDicesAction;
        messageBox.Show(sheet);
    }

    public void ShowDeathSavesMessage(int successCount, int failureCount, Action onSuccessAction, Action onFailureAction, Action onResetAction = null)
    {
        DeathSavesMessage messageBox = Instantiate(deathSavesMessagePrefab, transform);
        messageBox.OnSuccessPressed += onSuccessAction;
        messageBox.OnFailurePressed += onFailureAction;
        messageBox.OnResetPressed += onResetAction;
        messageBox.Show(successCount, failureCount);
    }
}
