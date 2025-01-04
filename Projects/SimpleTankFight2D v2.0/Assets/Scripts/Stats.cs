using UnityEngine;
using System;

public static class Stats
{
    public static Int32 Level { get; private set; } = 1;
    private static Int32 _score = 0;
    public static event Action LvLChanged;
    
    public static Int32 Score
    {
        get => _score;
        set
        {
            _score = value;

            if (_score >= 100 * Level)
            {
                Level++;
                _score = 0;
                LvLChanged?.Invoke();
            }
        }
    }

    public static void ResetAllStats()
    {
        Level = 1;
        _score = 0;
        LvLChanged?.Invoke();
    }
}
