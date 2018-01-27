using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceiver : MonoBehaviour {

	public void Receive(float signalStrength)
    {
        print("Receiving signal of strength: " + signalStrength);
    }
}
