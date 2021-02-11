using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelectionButton : MonoBehaviour
{
    #region Graphical Data
    [SerializeField] private Button m_button;
    [SerializeField] private TMP_Text buttonText;
    #endregion

    public void InitButton(LevelManager.GameLevel _level)
    {
        buttonText.text = _level.LevelIndex.ToString("00");
        ColorBlock buttonColorBlock = m_button.colors;
        m_button.interactable = _level.IsUnlocked;
        m_button.colors = buttonColorBlock;
        LoadSelectedLevel(_level);
    }

    private void LoadSelectedLevel(LevelManager.GameLevel _level)
    {
        GameManager gameManager = GameManager.instance;
        m_button.onClick.AddListener(delegate { gameManager.LoadScene("Level_" + _level.LevelIndex.ToString("00")); });
    }
}
