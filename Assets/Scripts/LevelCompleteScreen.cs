using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCompleteScreen : MonoBehaviour {

    [SerializeField]
    private RectTransform _contentsPanel;
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
    private Image _dudeImage;
    [SerializeField]
    private Color[] _signalColors;
    [SerializeField]
    private Sprite[] _ratingSprites;
    [SerializeField]
    private Sprite[] _dudeSprites;

    private float _signalStrength;
    private float _points;
    private int _pointCapGlorious;
    private int _pointCapGodlike;

    private AudioClip _mediocreSound;
    private AudioClip _gloriousSound;
    private AudioClip _godlikeSound;

    private LevelController _levelController;

    public float SignalStrength
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

    private void Start()
    {
        _levelController = FindObjectOfType<LevelController>();

        _mediocreSound = Resources.Load("Mediocre") as AudioClip;
        _gloriousSound = Resources.Load("Glorious") as AudioClip;
        _godlikeSound = Resources.Load("Godlike") as AudioClip;
    }

    public void Show()
    {
        _contentsPanel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _contentsPanel.gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        _levelController.StartNextLevel();
    }

    public void RestartLevel()
    {
        _levelController.RestartLevel();
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
        if (_points < _pointCapGlorious)
        {
            color = _signalColors[0];
            _ratingImage.sprite = _ratingSprites[0];
            _dudeImage.sprite = _dudeSprites[0];
            AudioSource.PlayClipAtPoint(_mediocreSound, Vector3.zero);
        }
        else if(_pointCapGlorious <= _points && _points < _pointCapGodlike)
        {
            color = _signalColors[1];
            _ratingImage.sprite = _ratingSprites[1];
            _dudeImage.sprite = _dudeSprites[1];
            AudioSource.PlayClipAtPoint(_gloriousSound, Vector3.zero);
        }
        else
        {
            color = _signalColors[2];
            _ratingImage.sprite = _ratingSprites[2];
            _dudeImage.sprite = _dudeSprites[2];
            AudioSource.PlayClipAtPoint(_godlikeSound, Vector3.zero);
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
