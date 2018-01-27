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

    private float _score;
    public float Score
    {
        get
        {
            return _score;
        }

        set
        {
            if(_score != value)
            {
                _score = value;
                SetScoreText(_score);
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
        Score = 100;
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
}
