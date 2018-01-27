﻿using System.Collections;
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
    private int _pointCapGlorious;
    private int _pointCapGodlike;

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
                SetSignalStrengthText(_signalStrength);
                SetSignalStrengthColor();
            }
        }
    }

    public float Points
    {
        get
        {
            return _points;
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

    private void SetSignalStrengthText(float value)
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

    private void SetSignalStrengthColor()
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
        print("POINTS");
        print(_points);
        print(_pointCapGlorious);
        print(_pointCapGodlike);
        if (_points < _pointCapGlorious)
        {
            print("MISERABLE");
            color = _signalColors[0];
            _ratingImage.sprite = _ratingSprites[0];
        }
        else if(_pointCapGlorious <= _points && _points < _pointCapGodlike)
        {
            print("FUK YE");
            color = _signalColors[1];
            _ratingImage.sprite = _ratingSprites[1];
        }
        else
        {
            print("GOOOD");
            color = _signalColors[2];
            _ratingImage.sprite = _ratingSprites[2];
        }

        _pointsAmount.color = color;
        _pointsLabel.color = color;
        _ratingImage.color = color;
        _wifiImage.color = color;
        _wifiImage.fillAmount = _points.Scale(0f, _pointCapGodlike, 0f, 1f);
    }

    public void SetLevelPointCaps(int capGlorious, int capGodlike)
    {
        _pointCapGlorious = capGlorious;
        _pointCapGodlike = capGodlike;
        print("capglio");
        print(capGlorious);
    }
}
