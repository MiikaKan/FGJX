using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    [SerializeField]
    private List<SignalReceiver> _requiredReceivers;
    [SerializeField]
    private DoneButton _doneButton;

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
