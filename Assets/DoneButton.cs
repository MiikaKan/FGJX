using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneButton : MonoBehaviour {

    [SerializeField]
    private Image _fader;
    [SerializeField]
    private Button _button;

    public void Activate()
    {
        _fader.enabled = false;
    }

    public void Deactivate()
    {
        _fader.enabled = true;
    }
}
