using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _levelIdText;
    [SerializeField]
    private TextMeshProUGUI _levelTitleText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _signalStrengthText;
    [SerializeField]
    private Image _wifiImage;
    [SerializeField]
    private Color[] _signalColors; 

    private float _score;
    public float Score
    {
        get
        {
            return _score;
        }

        set
        {
            if (_score != value)
            {
                _score = value;
                SetScoreText(_score);
                SetScoreColor();
            }
        }
    }

    private void Start()
    {
        Score = 60;
    }

    private void SetScoreText(float value)
    {
        _scoreText.text = Mathf.RoundToInt(value).ToString() + " %";
    }

    public void SetLevelTitle(int id, string title)
    {
        _levelIdText.text = "LEVEL " + id + ":";
        _levelTitleText.text = title.ToUpperInvariant();
    }

    private void SetScoreColor()
    {
        Color color;
        if (_score < 1)
        {
            color = _signalColors[0];
        }
        else if(_score >= 1 && _score < 40)
        {
            color = Color.Lerp(_signalColors[1], _signalColors[2], 0.01f * _score);
        }
        else if (_score >= 40 && _score < 80)
        {
            color = Color.Lerp(_signalColors[2], _signalColors[3], 0.01f * _score);
        }

        else
        {
            color = _signalColors[3];
        }

        _signalStrengthText.color = color;
        _wifiImage.color = color;
        _wifiImage.fillAmount =  0.01f * _score;
    }
}
