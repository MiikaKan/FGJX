using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSound : MonoBehaviour, IPointerEnterHandler
{

    private AudioClip _hoverSound;

    private void Start()
    {
        _hoverSound = Resources.Load("ButtonHover") as AudioClip;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        AudioSource.PlayClipAtPoint(_hoverSound, Vector3.zero);
    }
}
