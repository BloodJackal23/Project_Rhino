using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Utility/Level Data")]
public class LevelData : ScriptableObject
{
    public enum LevelScene
    {
        Level_01,
        Level_02,
        Level_03,
        Level_04
    }

    [SerializeField] private LevelScene levelScene = LevelScene.Level_01;
    [SerializeField] private string levelName = "01";
    public LevelScene LvlScene { get => levelScene; }
    public string LevelName { get => levelName; }
}
