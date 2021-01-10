using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingSystem
{
    public enum Scenes 
    { 
        MainMenu, 
        LoadingScene,
        Tutorial,
        Level_01,
        Level_02,
        Level_03,
        EndOfDemo
    }

    public delegate void OnLoadStart();
    public static OnLoadStart onLoadStart;

    public delegate void WhileLoading(float _progress);
    public static WhileLoading whileLoading;

    public delegate void OnLoadEnd();
    public static OnLoadEnd onLoadEnd;

    public static IEnumerator LoadNextScene(AsyncOperation _operation)
    {
        float progress = 0;
        while (!_operation.isDone)
        {
            progress = Mathf.Clamp01(_operation.progress / .9f);
            whileLoading?.Invoke(progress);
            yield return null;
        }
        onLoadEnd?.Invoke();
    }
}
