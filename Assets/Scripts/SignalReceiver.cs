using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalReceiver : MonoBehaviour {

    [SerializeField]
    private ScoreDisplay _scoreDisplay;
    private bool _receiving;
    private float _signalStrength;

    public bool ReceivingStatus { get; private set; }
    public UnityEvent OnReceiveStatusChanged = new UnityEvent();

	public void Receive(float signalStrength)
    {
        _signalStrength = signalStrength;
        _receiving = true;
    }

    private void Update()
    {
        if (_receiving)
        {
            //print("Receiving signal of strength: " + _signalStrength);
            _scoreDisplay.Score = _signalStrength;
        }
        else
        {
            _scoreDisplay.Score = 0f;
        }

        if(_receiving && ReceivingStatus == false)
        {
            ReceivingStatus = true;
            OnReceiveStatusChanged.Invoke();
        }else if(!_receiving && ReceivingStatus == true)
        {
            ReceivingStatus = false;
            OnReceiveStatusChanged.Invoke();
        }

        _receiving = false;
    }
}
