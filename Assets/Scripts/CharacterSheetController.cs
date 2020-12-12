using UnityEngine;
using UnityEngine.UI;

using CommonMaxTools.Attributes;

public class CharacterSheetController : MonoBehaviour
{
    [Foldout("Character Screen", true)]
    [SerializeField] private Transform characterScreen;
    [SerializeField] private Button backToCharacterPickerButton;
    [SerializeField] private CharacterHeaderHolder characterHeader;
    [SerializeField] private CharacterStatsHolder characterStats;
    [SerializeField] private CharacterSkillsHolder characterSkills;
    [SerializeField] private SpecialFeaturesHolder specialFeatures;
    [SerializeField] private CharacterSavingThrowsHolder characterSavingThrows;

    [Foldout("Character Picker Screen", true)]
    [SerializeField] private Transform characterPickerScreen;
    [SerializeField] private CharacterListController characterListController;

    private CharacterPickerWrapper characterPickerWrapper;
    private CharacterHeaderWrapper characterHeaderWrapper;
    private CharacterStatsWrapper characterStatsWrapper;
    private CharacterSkillsWrapper characterSkillsWrapper;
    private SpecialFeaturesWrapper specialFeaturesWrapper;
    private CharacterSavingThrowsWrapper characterSavingThrowsWrapper;

    public CharacterSheet Character { get; private set; }

    private void Start()
    {
        // picker part
        characterPickerWrapper = new CharacterPickerWrapper(characterListController, this);
        characterPickerWrapper.Initialize();

        // sheet part
        characterHeaderWrapper = new CharacterHeaderWrapper(characterHeader, this);
        characterStatsWrapper = new CharacterStatsWrapper(characterStats, this);
        characterSkillsWrapper = new CharacterSkillsWrapper(characterSkills, this);
        specialFeaturesWrapper = new SpecialFeaturesWrapper(specialFeatures, this);
        characterSavingThrowsWrapper = new CharacterSavingThrowsWrapper(characterSavingThrows, this);

        backToCharacterPickerButton.onClick.AddListener(() =>
        {
            BackToCharacterPicker();
        });
    }

    public void OpenCharacter(CharacterSheet character)
    {
        Character?.Dispose();
        Character = character;

        characterPickerScreen.gameObject.SetActive(false);
        characterScreen.gameObject.SetActive(true);

        // TODO: optimize it by one interface
        characterHeaderWrapper.OnCharacterChanged();
        characterStatsWrapper.OnCharacterChanged();
        characterSkillsWrapper.OnCharacterChanged();
        specialFeaturesWrapper.OnCharacterChanged();
        characterSavingThrowsWrapper.OnCharacterChanged();
    }

    private void BackToCharacterPicker()
    {
        characterScreen.gameObject.SetActive(false);
        characterPickerScreen.gameObject.SetActive(true);

        characterPickerWrapper.Initialize();
    }
}
