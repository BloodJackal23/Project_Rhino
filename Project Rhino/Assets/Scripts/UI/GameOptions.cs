using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
    [SerializeField] GameObject mainOptionsMenu, mainSettingsMenu, videoSettingsMenu, audioSettingsMenu, quitConfirmationPanel;
    [Space]

    [SerializeField] Button returnToMainMenu;

    private void Start()
    {
        returnToMainMenu.onClick.AddListener(delegate { GameManager.instance.LoadMainMenuScene(); });
    }

    public void ShowMainOptions()
    {
        mainOptionsMenu.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        mainOptionsMenu.SetActive(false);
        mainSettingsMenu.SetActive(false);
        videoSettingsMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        quitConfirmationPanel.SetActive(false);
    }

    GameObject FindByChildName(GameObject _child, string _name)
    {
        if(!_child)
        {
            return transform.Find(_name).gameObject;
        }
        return _child;
    }
}
