using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCompleteScreen : MonoBehaviour {

    [SerializeField]
    private RectTransform _contentsPanel;
    [SerializeField]
    private TextMeshProUGUI _titleLabel;
    [SerializeField]
    private TextMeshProUGUI _signalStrengthLabel;
    [SerializeField]
    private TextMeshProUGUI _signalStrengthText;
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

    private int _pointCapFail = 80;
    private int _pointCapGood = 150;
    private int _pointCapGreat = 250;

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
                SetPointsColorAndImages();
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
        _signalStrengthText.text = Mathf.RoundToInt(value).ToString() + " %";
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

        _signalStrengthText.color = color;
    }

    private void SetPointsColorAndImages()
    {
        Color color;
        if (_signalStrength > _pointCapFail && _signalStrength < _pointCapGood)
        {
            color = _signalColors[0];
            _ratingImage.sprite = _ratingSprites[0];
            _dudeImage.sprite = _dudeSprites[0];
            AudioSource.PlayClipAtPoint(_mediocreSound, Camera.main.transform.position, 0.2f);
        }
        else if(_signalStrength > _pointCapGood && _signalStrength < _pointCapGreat)
        {
            color = _signalColors[1];
            _ratingImage.sprite = _ratingSprites[1];
            _dudeImage.sprite = _dudeSprites[1];
            AudioSource.PlayClipAtPoint(_gloriousSound, Camera.main.transform.position, 0.2f);
        }
        else
        {
            color = _signalColors[2];
            _ratingImage.sprite = _ratingSprites[2];
            _dudeImage.sprite = _dudeSprites[2];
            AudioSource.PlayClipAtPoint(_godlikeSound, Camera.main.transform.position, 0.2f);
        }

        _signalStrengthText.color = color;
        _signalStrengthLabel.color = color;
        _ratingImage.color = color;
        _wifiImage.color = color;
        _wifiImage.fillAmount = _signalStrength.Scale(0f, _pointCapGood, 0f, 1f);
    }

    public void SetLevelTitle(int levelid)
    {
        _titleLabel.text = "LEVEL " + levelid + " COMPLETE";
    }

    public void SetPointCaps(int fail, int good, int great)
    {
        _pointCapFail = fail;
        _pointCapGood = good;
        _pointCapGreat = great;
    }
}
