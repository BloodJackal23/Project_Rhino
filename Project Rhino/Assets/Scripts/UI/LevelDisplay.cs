using UnityEngine;

public class LevelDisplay : UI_Panel
{
    [SerializeField] private GameObject levelSelectionButtonPrefab;
    [SerializeField] private Transform content;

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        DisplayGameLevels();
    }

    private void OnDisable()
    {
        ClearLevelButtons();
    }

    private void DisplayGameLevels()
    {
        LevelManager levelManager = LevelManager.instance;
        for(int i = 0; i < levelManager.GameLevels.Length; i++)
        {
            CreateLevelButton(levelManager.GameLevels[i]);
        }
    }

    private void CreateLevelButton(GameLevel _gameLevel)
    {
        LevelSelectionButton selectionButton = Instantiate(levelSelectionButtonPrefab, content).GetComponent<LevelSelectionButton>();
        selectionButton.InitButton(_gameLevel);
    }

    private void ClearLevelButtons()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
