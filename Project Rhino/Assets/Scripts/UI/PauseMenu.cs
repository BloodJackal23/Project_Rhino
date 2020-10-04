﻿using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region Buttons
    [Header("Buttons")]
    [SerializeField] Button resumeGameButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button backToMainButton;
    [SerializeField] Button quitGameButton;
    [Space]
    #endregion

    #region Other panels
    [Header("Other Panels")]
    [SerializeField] MainSettings mainSettings;
    [SerializeField] AudioSettings audioSettings;
    [SerializeField] VideoSettings videoSettings;
    [SerializeField] QuitConfirmation quitConfirmation;
    #endregion

    private void Start()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.gamePausedDelegate += OnGamePaused;
        gameManager.gameResumedDelegate += OnGameResumed;
        AddToResumegameButton(gameManager);
        gameObject.SetActive(gameManager.gamePaused);
    }

    private void OnDestroy()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.gamePausedDelegate = null;
        gameManager.gameResumedDelegate = null;
        //resumeGameButton.onClick.RemoveAllListeners();
    }

    public void OnGamePaused()
    {
        gameObject.SetActive(true);
    }

    public void OnGameResumed()
    {
        gameObject.SetActive(false);
        mainSettings.gameObject.SetActive(false);
        audioSettings.gameObject.SetActive(false);
        videoSettings.gameObject.SetActive(false);
        quitConfirmation.gameObject.SetActive(false);
    }

    void AddToResumegameButton(GameManager _gameManager)
    {
        resumeGameButton.onClick.AddListener(delegate { OnGameResumed(); });
        resumeGameButton.onClick.AddListener(delegate { _gameManager.PauseGameToggle(); });
    }
}
