using UnityEngine;

public abstract class CharacterBaseWrapper<T> where T : MonoBehaviour
{
    protected readonly T characterHolder;
    protected readonly CharacterSheetController characterSheetController;

    public CharacterBaseWrapper(T characterHolder, CharacterSheetController characterSheetController)
    {
        this.characterHolder = characterHolder;
        this.characterSheetController = characterSheetController;
    }

    public abstract void OnCharacterChanged();
}
