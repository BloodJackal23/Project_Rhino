using UnityEngine;
using UnityEngine.UI;

public class QuitConfirmation : UI_Panel
{
    GameManager gameManager;
    [SerializeField] Button confirmButton;

    protected override void Start()
    {
        base.Start();
        gameManager = GameManager.instance;
        confirmButton.onClick.AddListener(delegate { gameManager.QuitGame(); });
    }
}
