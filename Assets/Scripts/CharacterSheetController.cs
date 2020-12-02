using UnityEngine;
using UnityEngine.UI;

using CommonMaxTools.Attributes;

public class CharacterSheetController : MonoBehaviour
{
    [Foldout("Character Screen", true)]
    [SerializeField] private Transform characterScreen;
    [SerializeField] private Button backToCharacterPickerButton;
    [SerializeField] private CharacterStatsHolder characterStats;

    [Foldout("Character Picker Screen", true)]
    [SerializeField] private Transform characterPickerScreen;
    [SerializeField] private CharacterListController characterListController;

    private CharacterPickerWrapper characterPickerWrapper;
    private CharacterStatsWrapper characterStatsWrapper;

    public CharacterSheet CharacterSheet { get; private set; }

    private void Start()
    {
        // picker part
        characterPickerWrapper = new CharacterPickerWrapper(characterListController, this);
        characterPickerWrapper.Initialize();

        // sheet part
        characterStatsWrapper = new CharacterStatsWrapper(characterStats, this);

        backToCharacterPickerButton.onClick.AddListener(() =>
        {
            BackToCharacterPicker();
        });
    }

    public void OpenCharacter(CharacterSheet character)
    {
        CharacterSheet = character;

        characterPickerScreen.gameObject.SetActive(false);
        characterScreen.gameObject.SetActive(true);

        characterStatsWrapper.OnCharacterChanged();
    }

    private void BackToCharacterPicker()
    {
        characterScreen.gameObject.SetActive(false);
        characterPickerScreen.gameObject.SetActive(true);

        characterPickerWrapper.Initialize();
    }
}
