using UnityEngine;
using UnityEngine.UI;

public class QuitConfirmation : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Button confirmButton;

    private void Start()
    {
        gameManager = GameManager.instance;
        confirmButton.onClick.AddListener(delegate { gameManager.QuitGame(); });
    }
}
