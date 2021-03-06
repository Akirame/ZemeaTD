﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour {

    public WaveControl waveControl;
    public GameObject tutorial;
    private SpriteRenderer rend;
    public List<Sprite> colorSprites;
    private int hitConta = 0;
    private void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        RandomColor();
    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if(CheckColor(collision.GetComponent<ColorProyectile>().colorType))
            {
                hitConta++;
                RandomColor();
            }
            if (!GameManager.Get().tutorialDone && hitConta > 3)
            {
                tutorial.GetComponent<TutorialManager>().TutorialEnd();
            }
        }
    }

    private bool CheckColor(ColorAttribute.COLOR_TYPE element)
    {
        switch(element)
        {
            case ColorAttribute.COLOR_TYPE.GREEN:
                return (rend.sprite == colorSprites[0]);
            case ColorAttribute.COLOR_TYPE.MAGENTA:
                return (rend.sprite == colorSprites[1]);
            case ColorAttribute.COLOR_TYPE.ORANGE:
                return (rend.sprite == colorSprites[2]);
            case ColorAttribute.COLOR_TYPE.YELLOW:
                return (rend.sprite == colorSprites[3]);
        }
        return false;
    }

    private void RandomColor()
    {
        rend.sprite = colorSprites[UnityEngine.Random.Range(0, colorSprites.Count)];
    }
}
