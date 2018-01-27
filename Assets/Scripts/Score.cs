using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {

    private int score;

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        score = 99;
        SetScoreText();
    }

    public void SetScoreText()
    {
        scoreText.text = score.ToString() + " %";
    }


}
