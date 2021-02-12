using System;

[Serializable]
public class SaveFile
{
    #region Unlocked Levels
    public int[] levelIndeces;
    #endregion

    public SaveFile(GameLevel[] _unlockedLevels)
    {
        levelIndeces = new int[_unlockedLevels.Length];
        for(int i = 0; i < levelIndeces.Length; i++)
        {
            levelIndeces[i] = _unlockedLevels[i].LevelIndex;
        }
    }
}
