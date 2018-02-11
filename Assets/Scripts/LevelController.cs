using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [System.Serializable]
    public struct LevelData
    {
        public string SceneName;
        public string SceneTitle;
        public int PointCapFail;
        public int PointCapGood;
        public int PointCapGreat;
    }

    [SerializeField]
    private List<LevelData> _levelDatas;

    private LevelCompleteScreen _levelCompleteScreen;
    private List<SignalReceiver> _requiredReceivers;
    private DoneButton _doneButton;

    private ScoreDisplay _scoreDisplay;

    private LevelData _activeLevel;
    public int ActiveLevelId { get; private set; }
    public bool TutorialRead;

    public void CompleteLevel()
    {
        _scoreDisplay.gameObject.SetActive(false);
        _levelCompleteScreen.SignalStrength = _scoreDisplay.Score;
        _levelCompleteScreen.SetLevelTitle(ActiveLevelId);
        _levelCompleteScreen.SetPointCaps(_activeLevel.PointCapFail, _activeLevel.PointCapGood, _activeLevel.PointCapGreat);
        _levelCompleteScreen.Show();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _requiredReceivers = new List<SignalReceiver>();
        TutorialRead = false;
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


    public void SetTutorialRead()
    {
        TutorialRead = true;
    }

    public void StartLevel(int levelId)
    {
        ActiveLevelId = levelId;
        _activeLevel = _levelDatas[levelId];
        SceneManager.LoadScene(_activeLevel.SceneName);
    }

    public void StartNextLevel()
    {
        StartLevel(ActiveLevelId + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_activeLevel.SceneName);
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

        _scoreDisplay.SetLevelTitle(ActiveLevelId, _activeLevel.SceneTitle);

        _doneButton.Deactivate();
    }
}
