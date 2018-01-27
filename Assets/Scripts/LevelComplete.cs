using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelComplete : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _pointsAmount;
    [SerializeField]
    private TextMeshProUGUI _pointsLabel;
    [SerializeField]
    private TextMeshProUGUI _bouncesAmount;
    [SerializeField]
    private TextMeshProUGUI _signalStrengthAmount;
    [SerializeField]
    private Image _wifiImage;
    [SerializeField]
    private Image _ratingImage;
    [SerializeField]
    private Color[] _signalColors;
    [SerializeField]
    private Sprite[] _ratingSprites;

    private float _signalStrength;
    private float _points;

    public float SignalStrenght
    {
        get
        {
            return _signalStrength;
        }

        set
        {
            if (_signalStrength != value)
            {
                _signalStrength = value;
                SetScoreText(_signalStrength);
                SetScoreColor();
            }
        }
    }

    public float Points
    {
        get
        {
            return _signalStrength * _bounces;
        }

        set
        {
            print(value);
            if (_points != value)
            {
                _points = value;
                SetPointsText(_points);
                SetPointsColorAndImages();
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
                SetBouncesColor();
            }
        }
    }


    private void Start()
    {
        SignalStrenght = 60;
        Bounces = 1;
        Points = 120;
    }

    private void SetScoreText(float value)
    {
        _signalStrengthAmount.text = Mathf.RoundToInt(value).ToString() + " %";
    }

    private void SetPointsText(float value)
    {
        _pointsAmount.text = Mathf.RoundToInt(value).ToString();
    }

    private void SetBounceText(int value)
    {
        _bouncesAmount.text = value.ToString();
    }

    private void SetScoreColor()
    {
        Color color;
        if (_signalStrength < 1)
        {
            color = _signalColors[0];
        }
        else
        {
            color = Color.Lerp(_signalColors[1], _signalColors[2], 0.01f * _signalStrength);
        }

        _signalStrengthAmount.color = color;
        _wifiImage.color = color;
        _wifiImage.fillAmount = 0.01f * _signalStrength;
    }

    private void SetBouncesColor()
    {
        Color color;
        if (_bounces < 3)
        {
            color = _signalColors[0];
        }
        else
        {
            color = _signalColors[1];
        }

        _bouncesAmount.color = color;
    }

    private void SetPointsColorAndImages()
    {
        Color color;
        if (_points < 100)
        {
            color = _signalColors[0];
            _ratingImage.sprite = _ratingSprites[0];
        }
        else
        {
            color = _signalColors[1];
            _ratingImage.sprite = _ratingSprites[1];
        }

        _pointsAmount.color = color;
        _pointsLabel.color = color;
        _wifiImage.color = color;
        _ratingImage.color = color;
        _wifiImage.fillAmount = 0.01f * _signalStrength;   
    }
}
