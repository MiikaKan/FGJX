using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickSound : MonoBehaviour {

    [SerializeField]
    private AudioClip _onClickSound;

    public void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(_onClickSound, transform.position);
    }
}
