﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text waveText;
    public Text P1Level;
    public Text P2Level;
    public Image orbPlayer1;
    public Image orbPlayer2;
    public Image expBarP1;
    public Image expBarP2;
    public AttackBehaviour player1;
    public AttackBehaviour player2;
    public Sprite[] orbSprites; 
    private string waveTextAux;
    private PlayerLevel p1;
    private PlayerLevel p2;
    private int p1Level;
    private int p2Level;
    private int p1Exp;
    private int p2Exp;

    private void Start()
    {
        p1 = player1.gameObject.GetComponent<PlayerLevel>();
        p2 = player2.gameObject.GetComponent<PlayerLevel>();
        p1Level = p1.playerLevel;
        p2Level = p2.playerLevel;
        p1Exp = p1.playerExperience;
        p2Exp = p2.playerExperience;
        expBarP1.fillAmount = p1.playerExperience / (p1.expNeededPerLevel * p1.playerLevel);
        expBarP2.fillAmount = p2.playerExperience / (p2.expNeededPerLevel * p2.playerLevel);
    }

    private void Update()
    {
        if (waveTextAux != EnemyManager.GetInstance().wave.name) //delegate
        {
            GetComponent<Animator>().SetTrigger("wave");
            waveTextAux = EnemyManager.GetInstance().wave.name;            
        }
        ChangeOrbImage();


        if (p1Exp != p1.playerExperience)
        {
            p1Exp = p1.playerExperience;
            expBarP1.fillAmount = (float)p1.playerExperience/ (p1.expNeededPerLevel * p1.playerLevel);
        }

        if (p2Exp != p2.playerExperience)
        {
            p2Exp = p2.playerExperience;
            expBarP2.fillAmount = (float)p2.playerExperience / (p2.expNeededPerLevel * p2.playerLevel);
        }

        if (p1.playerLevel != p1Level)
        {
            p1Level = p1.playerLevel;
            P1Level.text = "Lvl " + p1.playerLevel.ToString();
        }
        if (p2.playerLevel != p2Level)
        {
            p2Level = p2.playerLevel;
            P2Level.text = "Lvl " + p2.playerLevel.ToString();
        }

    }
    
    private void ChangeOrbImage()//delegate
    {
        if (player1.currentElement)
        {
            orbPlayer1.color = new Color(1f, 1f, 1f, 1f);
            orbPlayer1.sprite = orbSprites[(int)player1.currentElement.elementType];
        }
        else
        {
            orbPlayer1.color = new Color(1f, 1f, 1f, 0f);            
        }
        if (player2.currentElement)
        {
            orbPlayer2.color = new Color(1f, 1f, 1f, 1f);
            orbPlayer2.sprite = orbSprites[(int)player2.currentElement.elementType];
        }
        else
        {
            orbPlayer2.color = new Color(1f, 1f, 1f, 0f);            
        }
    }

    private void UpdateText()
    {
        waveText.text = waveTextAux;
    }
}