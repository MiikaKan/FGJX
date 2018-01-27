using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceiver : MonoBehaviour {

    [SerializeField]
    private ScoreDisplay _scoreDisplay;
    private bool _receiving;
    private float _signalStrength;

	public void Receive(float signalStrength)
    {
        _signalStrength = signalStrength;
        _receiving = true;
    }

    private void Update()
    {
        if (_receiving)
        {
            print("Receiving signal of strength: " + _signalStrength);
            _scoreDisplay.Score = _signalStrength;
        }
        else
        {
            _scoreDisplay.Score = 0f;
        }

        _receiving = false;
    }
}
