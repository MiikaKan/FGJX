using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI scoreText;

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


    private void Start()
    {
        Score = 100;
    }

    private void SetScoreText(float value)
    {
        scoreText.text = Mathf.RoundToInt(value).ToString() + " %";
    }
}
