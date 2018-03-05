using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SignalBounceType
{
    POSITIVE,
    NEGATIVE,
    NEUTRAL,
    NOBOUNCE,
}

public class SignalBouncer : MonoBehaviour
{
    [SerializeField] private SignalBounceType _signalBounceType;

    public float BounceAmount
    {
        get
        {
            var bounceAmount = 1f;
            switch (_signalBounceType)
            {
                case SignalBounceType.POSITIVE:
                    bounceAmount = 1.33f;
                    break;
                case SignalBounceType.NEGATIVE:
                    bounceAmount = 0.75f;
                    break;
                case SignalBounceType.NOBOUNCE:
                    bounceAmount = 0f;
                    break;
            }
            return bounceAmount;
        }
    }
}
