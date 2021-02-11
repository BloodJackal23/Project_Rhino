using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Serializable]
    public class GameLevel
    {
        [SerializeField] private LevelData.LevelScene levelScene = LevelData.LevelScene.Level_01;
        [SerializeField] private string levelName = "01";
        [SerializeField] private bool isCleared = false;
        public LevelData.LevelScene LevelScene { get => levelScene; }
        public string LevelName { get => levelName; }
        public bool IsCleared { get => isCleared; }

        public GameLevel(LevelData _levelData, bool _isAccessable, bool _isCleared)
        {
            levelScene = _levelData.LvlScene;
            levelName = _levelData.LevelName;
            isCleared = _isCleared;
        }

        public void SetLevelClearance(bool _value)
        {
            isCleared = _value;
        }
    }
    [SerializeField] private LevelData[] gameLevelsData;
    private GameLevel[] gameLevels;
    public GameLevel[] GameLevels { get => gameLevels; }
    [SerializeField] private List<GameLevel> accessibleLevels = new List<GameLevel>();
    public List<GameLevel> AccessibleLevels { get => accessibleLevels; }

    public void SetTrackerDefaults()
    {
        gameLevels = new GameLevel[gameLevelsData.Length];
        gameLevels[0] = new GameLevel(gameLevelsData[0], true, false);
        AddToAccessibleLevels(gameLevels[0]);
        if (gameLevels.Length > 1)
        {
            for(int i = 1; i < gameLevelsData.Length; i++)
            {
                gameLevels[i] = new GameLevel(gameLevelsData[i], false, false);
            }
        }
    }

    public bool IsLevelAccessible(GameLevel _level)
    {
        if (accessibleLevels.Contains(_level))
        {
            return true;
        }
        return false;
    }

    public void AddToAccessibleLevels(GameLevel _level)
    {
        if(!accessibleLevels.Contains(_level))
        {
            accessibleLevels.Add(_level);
        }
        else
        {
            Debug.LogWarning("Level scene of name " + _level.LevelName + " is already accessible!");
        }
    }
}
