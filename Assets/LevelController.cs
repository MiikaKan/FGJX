using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [System.Serializable]
    public struct levelData
    {
        public string sceneName;
        public string sceneTitle;
        public int pointCapGlorious;
        public int pointCapGodlike;
    }

    [SerializeField]
    private List<levelData> _levelDatas;

    private LevelCompleteScreen _levelCompleteScreen;
    private List<SignalReceiver> _requiredReceivers;
    private DoneButton _doneButton;

    private ScoreDisplay _scoreDisplay;

    private levelData _activeLevel;
    public int ActiveLevelId { get; private set; }

    public void CompleteLevel()
    {
        _scoreDisplay.gameObject.SetActive(false);
        _levelCompleteScreen.SignalStrength = _scoreDisplay.Score;
        _levelCompleteScreen.Bounces = _scoreDisplay.Bounces;
        _levelCompleteScreen.SetLevelPointCaps(_levelDatas[0].pointCapGlorious, _levelDatas[0].pointCapGodlike);
        _levelCompleteScreen.Points = Mathf.RoundToInt(_scoreDisplay.Score) * Mathf.RoundToInt(_scoreDisplay.Bounces);
        _levelCompleteScreen.Show();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _requiredReceivers = new List<SignalReceiver>();
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

    public void StartLevel(int levelId)
    {
        ActiveLevelId = levelId;
        _activeLevel = _levelDatas[levelId];
        SceneManager.LoadScene(_activeLevel.sceneName);
    }

    public void StartNextLevel()
    {
        StartLevel(ActiveLevelId + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_activeLevel.sceneName);
    }

    private void OnLevelWasLoaded(int level)
    {
        _scoreDisplay = FindObjectOfType<ScoreDisplay>();
        _doneButton = FindObjectOfType<DoneButton>();

        _requiredReceivers.Clear();
        _requiredReceivers.AddRange(FindObjectsOfType<SignalReceiver>());
        foreach (var receiver in _requiredReceivers)
        {
            receiver.OnReceiveStatusChanged.AddListener(CheckCompletion);
        }

        _levelCompleteScreen = FindObjectOfType<LevelCompleteScreen>();

        _scoreDisplay.SetLevelTitle(ActiveLevelId, _activeLevel.sceneTitle);

        _doneButton.Deactivate();
    }
}
