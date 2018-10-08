﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour {

    private bool lightOn = false;
    public float lightValue = 0f;
    public float lightPerSecond = 0.3f;
    public UILight LightUICanvas;

    private void Update() {
        if(lightOn)
            ActivateLight();
    }
    private void ActivateLight() {
        if(lightValue < 100) {
            lightValue += lightPerSecond * Time.deltaTime;
            LightUICanvas.UpdateTexts((int)lightValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player")
            lightOn = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player")
            lightOn = false;
    }
}