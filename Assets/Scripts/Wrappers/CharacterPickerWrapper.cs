
public class CharacterPickerWrapper
{
    private readonly CharacterListController characterList;
    private readonly CharacterSheetController characterController;

    public CharacterPickerWrapper(CharacterListController characterList, CharacterSheetController characterController)
    {
        this.characterList = characterList;
        this.characterController = characterController;

        characterList.OnAddItemPressed += () =>
        {
            var sheet = CharacterSheetStorage.CreateNewCharacter();
            characterList.AddItem(sheet);
            characterController.OpenCharacter(sheet);
        };
        characterList.OnItemAdded += InitCharacterPreview;
        characterList.OnItemRemoved += RemoveCharacterPreview;
    }

    public void Initialize()
    {
        characterList.Clear();

        foreach(var character in CharacterSheetStorage.characters)
        {
            characterList.AddItem(character);
        }
    }

    private void InitCharacterPreview(CharacterPreviewHolder preview, CharacterSheet sheet)
    {
        preview.sheetId = sheet.Id;

        preview.characterButton.onClick.AddListener(() =>
        {
            characterController.OpenCharacter(sheet);
        });
        preview.removeButton.onClick.AddListener(() =>
        {
            characterList.RemoveItem(preview);
        });

        preview.playerNameText.text = sheet.PlayerName;
        // TODO: assign other preview data
    }

    private void RemoveCharacterPreview(CharacterPreviewHolder preview)
    {
        CharacterSheetStorage.RemoveCharacter(preview.sheetId);
    }
}
