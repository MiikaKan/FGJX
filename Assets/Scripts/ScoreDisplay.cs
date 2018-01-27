using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _bouncesText;
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

    private int _bounces;
    public int Bounces
    {
        get
        {
            return _bounces;
        }

        set
        {
            if (_bounces != value)
            {
                _bounces = value;
                SetBounceText(_bounces);
            }
        }
    }


    private void Start()
    {
        Score = 60;
        Bounces = 0;
    }

    private void SetScoreText(float value)
    {
        _scoreText.text = Mathf.RoundToInt(value).ToString() + " %";
    }

    private void SetBounceText(int value)
    {
        _bouncesText.text = value.ToString();
    }

    private void SetScoreColor()
    {
        Color color;
        if (_score < 1)
        {
            color = _signalColors[0];
        }
        else
        {
            color = Color.Lerp(_signalColors[1], _signalColors[2], 0.01f * _score);
        }

        _signalStrengthText.color = color;
        _wifiImage.color = color;
        _wifiImage.fillAmount =  0.01f * _score;
    }
}
