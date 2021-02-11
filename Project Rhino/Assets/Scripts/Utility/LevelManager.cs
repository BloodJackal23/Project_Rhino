using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Serializable]
    public class GameLevel
    {
        [SerializeField] private int leveIndex = 1;
        [SerializeField] private bool isUnlocked = false;
        public int LevelIndex { get => leveIndex; }
        public bool IsUnlocked { get => isUnlocked; }

        public GameLevel(LevelData _levelData, bool _isUnlocked)
        {
            leveIndex = _levelData.LevelIndex;
            isUnlocked = _isUnlocked;
        }

        public void SetLevelLock(bool _value)
        {
            isUnlocked = !_value;
        }
    }
    [SerializeField] private LevelData[] gameLevelsData;
    private GameLevel[] gameLevels;
    private List<GameLevel> unlockedLevels = new List<GameLevel>();
    public GameLevel[] GameLevels { get => gameLevels; }
    public List<GameLevel> UnlockedLevels { get => unlockedLevels; }

    public void SetLevelsDefaultStatus()
    {
        gameLevels = new GameLevel[gameLevelsData.Length];
        gameLevels[0] = new GameLevel(gameLevelsData[0], true);
        unlockedLevels.Add(gameLevels[0]);
        if (gameLevels.Length > 1)
        {
            for(int i = 1; i < gameLevelsData.Length; i++)
            {
                gameLevels[i] = new GameLevel(gameLevelsData[i], false);
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
