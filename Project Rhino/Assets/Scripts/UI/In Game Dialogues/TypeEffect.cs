using UnityEngine;

public static class TypeEffect
{
    static float timer;

    public static void ResetTimer()
    {
        timer = 0;
    }

    public static void EndEffect(string _string)
    {
        timer = _string.Length;
    }

    /// <summary>
    /// Types text with specified speed. Setting the speed to 0 will type the text instantly
    /// </summary>
    /// <param name="_body"></param>
    /// <param name="_typeSpeed"></param>
    /// <returns></returns>
    public static string TypeText(string _body, float _typeSpeed)
    {
        string currentString = "";
        if (_typeSpeed > 0)
        {
            int charCounter = Mathf.Min((int)(timer * _typeSpeed), _body.Length);
            currentString = _body.Substring(0, charCounter);
            timer += Time.deltaTime;
        }
        else
        {
            currentString = _body;
        }
        return currentString;
    }

    public static bool StringComplete(string _currentString, string _fullString)
    {
        if (_currentString.Length < _fullString.Length)
            return false; //Hasn't finished typing yet
        return true; //Has finished typing
    }
}
