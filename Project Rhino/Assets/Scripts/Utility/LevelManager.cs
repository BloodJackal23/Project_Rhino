using System.Collections.Generic;
using UnityEngine;

public partial class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelData[] gameLevelsData;
    private GameLevel[] gameLevels;
    private List<GameLevel> unlockedLevels = new List<GameLevel>();
    public GameLevel[] GameLevels { get => gameLevels; }
    public List<GameLevel> UnlockedLevels { get => unlockedLevels; }

    private void InitGameLevels()
    {
        gameLevels = new GameLevel[gameLevelsData.Length];
        gameLevels[0] = new GameLevel(gameLevelsData[0].LevelIndex, true);
        unlockedLevels.Add(gameLevels[0]);
        if (gameLevels.Length > 1)
        {
            for (int i = 1; i < gameLevelsData.Length; i++)
            {
                gameLevels[i] = new GameLevel(gameLevelsData[i].LevelIndex, false);
            }
        }
    }

    public void SetGameLevels()
    {
        SaveFile saveFile = SaveSystem.LoadFile(SaveSystem.Path);
        InitGameLevels();
        if (saveFile != null)
        {
            GetUnlockedLevelsFromSaveFile(saveFile);
        }
    }

    private void GetUnlockedLevelsFromSaveFile(SaveFile _file)
    {
        for(int i = 0; i < gameLevels.Length; i++)
        {
            for(int j = 0; j < _file.levelIndeces.Length; j++)
            {
                if(gameLevels[i].LevelIndex == _file.levelIndeces[j])
                {
                    AddToUnlockedLevels(gameLevels[i]);
                    break;
                }
            }
        }
    }

    public void AddToUnlockedLevels(GameLevel _level)
    {
        if(!_level.IsUnlocked)
        {
            _level.SetLevelLock(false);
            unlockedLevels.Add(_level);
        }
        else
        {
            Debug.LogWarning("Level scene of name " + _level.LevelIndex.ToString("00") + " is already accessible!");
        }
    }

    public GameLevel GetGameLevel(SceneLoadingSystem.GameScene _levelScene)
    {
        foreach(GameLevel level in gameLevels)
        {
            if("Level_" + level.LevelIndex.ToString("00") == _levelScene.ToString())
            {
                return level;
            }
        }
        return null;
    }
}
