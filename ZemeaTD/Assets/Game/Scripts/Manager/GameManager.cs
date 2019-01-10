﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;
    private LoaderManager loader;
    public bool winGame = false;

    public static GameManager Get()
    {
        return instance;
    }
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public PlayerLevel player1Level;
    public PlayerLevel player2Level;
    public bool tutorialDone = false;
    private AudioSource aSource;

    private void Start()
    {
        loader = LoaderManager.Get();
        Tower.TowerDestroyed += GameOver;
        LightStand.LightFinished += GameWon;
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("ResetGameScene", ResetGame);
            DebugScreen.GetInstance().AddButton("Add Players Level", LevelUpPlayers);
        }
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddMusic(aSource);
    }

    private void ResetGame()
    {
        loader.LoadSceneQuick("SampleScene");
        winGame = false;
        tutorialDone = true;
    }

    private void GameWon(LightStand l)
    {
        winGame = true;
        loader.LoadScene("FinalScreen");
    }

    private void LevelUpPlayers()
    {
        player1Level.LevelUpPlayer();
        player2Level.LevelUpPlayer();
    }

    public void ToMainMenu()
    {
        loader.LoadSceneQuick("MainMenu");
    }

    private void GameOver(Tower t)
    {
        loader.LoadSceneQuick("FinalScreen");
        winGame = false;
    }
}
