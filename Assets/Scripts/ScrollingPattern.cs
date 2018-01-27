﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPattern : MonoBehaviour {

    public float scrollSpeed = 0.5F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("MainMenuUpperTile", new Vector2(offset, 0));
    }
}
