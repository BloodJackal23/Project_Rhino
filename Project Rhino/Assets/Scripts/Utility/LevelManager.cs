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
        LevelData levelData = gameLevelsData[0];
        gameLevels[0] = new GameLevel(levelData.LevelScene, levelData.LevelIndex, true);
        unlockedLevels.Add(gameLevels[0]);
        if (gameLevels.Length > 1)
        {
            for (int i = 1; i < gameLevelsData.Length; i++)
            {
                levelData = gameLevelsData[i];
                gameLevels[i] = new GameLevel(levelData.LevelScene, levelData.LevelIndex, false);
            }
        }
    }

    public void GetUnlockedLevelsFromSaveFile()
    {
        SaveFile saveFile = SaveSystem.LoadFile(SaveSystem.Path);
        InitGameLevels();
        if (saveFile != null)
        {
            for (int i = 0; i < gameLevels.Length; i++)
            {
                for (int j = 0; j < saveFile.levelIndeces.Length; j++)
                {
                    if (gameLevels[i].LevelIndex == saveFile.levelIndeces[j])
                    {
                        AddToUnlockedLevels(gameLevels[i]);
                        break;
                    }
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

    public GameLevel GetGameLevel(SceneSystem.GameScene _levelScene)
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
