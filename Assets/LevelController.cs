using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    [SerializeField]
    private List<SignalReceiver> _requiredReceivers;
    [SerializeField]
    private DoneButton _doneButton;

    [System.Serializable]
    public struct levelData
    {
        public string sceneName;
        public int pointCapGlorious;
        public int pointCapGodlike;
    }

    [SerializeField]
    private List<levelData> _levelDatas;
    [SerializeField]
    private LevelComplete _levelComplete;
    [SerializeField]
    private ScoreDisplay _gameScores;
    [SerializeField]
    private GameObject _scoreDisplay;
    [SerializeField]
    private GameObject _levelCompleteDisplay;
    [SerializeField]
    private GameObject _doneButtonDisplay;

    private levelData _activeLevel;

    public void CompleteLevel()
    {
        _scoreDisplay.SetActive(false);
        _doneButtonDisplay.SetActive(false);
        _levelCompleteDisplay.SetActive(true);
        _levelComplete.SignalStrenght = _gameScores.Score;
        _levelComplete.Bounces = _gameScores.Bounces;
        _levelComplete.SetLevelPointCaps(_levelDatas[0].pointCapGlorious, _levelDatas[0].pointCapGodlike);
        _levelComplete.Points = Mathf.RoundToInt(_gameScores.Score) * Mathf.RoundToInt(_gameScores.Bounces);
    }

    private void Start()
    {
        foreach (var receiver in _requiredReceivers)
        {
            receiver.OnReceiveStatusChanged.AddListener(CheckCompletion);
        }
    }

    private void CheckCompletion()
    {
        if (_requiredReceivers.TrueForAll(x => x.ReceivingStatus == true))
        {
            _doneButton.Activate();
        }
        else
        {
            _doneButton.Deactivate();
        }
    }

}
