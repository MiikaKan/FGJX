using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    [SerializeField]
    private AudioSource _introMusic;
    [SerializeField]
    private AudioSource _loopingMusic;

    void Start()
    {

        _introMusic.Play();

    }


    void Update()
    {

        if (!_introMusic.isPlaying && !_loopingMusic.isPlaying)
        {

            _loopingMusic.Play();

        }

    }
}
