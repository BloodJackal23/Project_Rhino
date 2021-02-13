using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public delegate void OnGameSaved();
    public static OnGameSaved onGameSaved;

    public delegate void OnSaveFileLoaded();
    public static OnSaveFileLoaded onSaveFileLoaded;

    public delegate void OnSaveFileDeleted();
    public static OnSaveFileDeleted onSaveFileDeleted;

    private static string dirPath = Application.persistentDataPath + "/Saves";
    private static string path = Application.persistentDataPath + "/Saves/savefile.kmp";
    public static string Path { get => path; }

    public static void SaveGame(GameLevel[] _unlockedLevels)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Debug.Log(path);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        SaveFile saveFile = new SaveFile(_unlockedLevels);
        formatter.Serialize(fileStream, saveFile);
        fileStream.Close();
        onGameSaved?.Invoke();
    }

    public static void CreateSaveFolder()
    {
        if(!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }

    public static SaveFile LoadFile(string _path)
    {
        if(File.Exists(_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(_path, FileMode.Open);
            SaveFile loadedFile = (SaveFile)formatter.Deserialize(fileStream);
            fileStream.Close();
            onSaveFileLoaded?.Invoke();
            return loadedFile;
        }
        Debug.LogWarning("No save file found in " + _path);
        return null;
    }

    public static void DeleteSaveFile(string _path)
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
            onSaveFileDeleted?.Invoke();
        }
        else
        {
            Debug.LogError("Save file not found in " + _path);
        }
    }
}
