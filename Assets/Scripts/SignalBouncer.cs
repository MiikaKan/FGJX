using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBouncer : MonoBehaviour {

    [SerializeField]
    [Range(0f, 1f)]
    private float bounceAmount = 1f;

    public float BounceAmount
    {
        get
        {
            return bounceAmount;
        }
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    other.GetComponent<Renderer>().material.color = Color.red;
    //}
}
