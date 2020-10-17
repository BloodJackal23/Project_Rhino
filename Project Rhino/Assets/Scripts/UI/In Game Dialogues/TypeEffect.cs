using UnityEngine;

public static class TypeEffect
{
    /// <summary>
    /// Types text with specified speed. Setting the speed to 0 will type the text instantly
    /// </summary>
    /// <param name="_body"></param>
    /// <param name="_typeSpeed"></param>
    /// <returns></returns>
    public static string TypeText(string _body, float _typeSpeed, float _timePassed)
    {
        string currentString = "";
        if (_typeSpeed > 0)
        {
            int charCounter = Mathf.Min((int)(_timePassed * _typeSpeed), _body.Length);
            currentString = _body.Substring(0, charCounter);
        }
        else
        {
            currentString = _body;
        }
        return currentString;
    }

    public static bool IsFinishedTyping(string _currentString, string _fullString)
    {
        if (_currentString.Length < _fullString.Length)
            return false; //Hasn't finished typing yet
        return true; //Has finished typing
    }
}
