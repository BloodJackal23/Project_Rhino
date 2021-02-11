using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelectionButton : MonoBehaviour
{
    #region Graphical Data
    [SerializeField] private Button m_button;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Color clearedColor = Color.green;
    [SerializeField] private Color unclearedColor = Color.red;
    #endregion

    public void InitButton(LevelManager.GameLevel _level)
    {
        buttonText.text = _level.LevelName;
        ColorBlock buttonColorBlock = m_button.colors;
        
        if(_level.IsCleared)
        {
            buttonColorBlock.normalColor = clearedColor;
        }
        else
        {
            buttonColorBlock.normalColor = unclearedColor;
        }
        m_button.colors = buttonColorBlock;
    }
}
