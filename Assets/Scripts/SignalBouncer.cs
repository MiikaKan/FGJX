using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBouncer : MonoBehaviour {

    [SerializeField]
    private float bounceAmount = 1f;

    public float BounceAmount
    {
        get
        {
            return bounceAmount;
        }
    }
}
