using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectionButtonPrefab;
    [SerializeField] private Transform content;

    private void OnDisable()
    {
        ClearLevelButtons();
    }

    private void DisplayGameLevels() //Called by the animator
    {
        LevelManager levelManager = LevelManager.instance;
        for(int i = 0; i < levelManager.GameLevels.Length; i++)
        {
            CreateLevelButton(levelManager.GameLevels[i]);
        }
    }

    private void CreateLevelButton(LevelManager.GameLevel _gameLevel)
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
