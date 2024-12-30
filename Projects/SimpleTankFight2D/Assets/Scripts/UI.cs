using UnityEngine;
using UnityEngine.UI;

using System;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _scoreText;

    public void UpdateScoreAndLevel()
    {
        _levelText.text = $"LEVEL {Stats.Level}";
        _scoreText.text = $"SCORE: {Stats.Score:0000}";
    }

    public void UpdateHP(Int32 value)
    {
        _hpText.text = $"HP: {value}";
    }
}
