using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneButton : MonoBehaviour {

    [SerializeField]
    private Image _fader;
    [SerializeField]
    private Button _button;

    private LevelController _levelController;

    private void Start()
    {
        _button = GetComponent<Button>();
        _levelController = FindObjectOfType<LevelController>();

        if (!_levelController)
        {
            Debug.LogError("Couldn'´t find LevelController!");
        }
        else {
            _button.onClick.AddListener(_levelController.CompleteLevel);
        }
    }

    private void OnDestroy()
    {
        if (_button)
        {
            _button.onClick.RemoveAllListeners();
        }
    }

    public void Activate()
    {
        _fader.enabled = false;
        _button.interactable = true;
    }

    public void Deactivate()
    {
        _fader.enabled = true;
        _button.interactable = false;
    }
}
